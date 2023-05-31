using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using PearApi.Utils;
using Microsoft.EntityFrameworkCore;

namespace PearApi.Authentication
{
   public interface IJwtAuthManager
   {
      Task<JwtAuthResult> GenerateTokens(long userId, Claim[] claims, DateTime now);
      Task<JwtAuthResult> RefreshAsync(string refreshToken, string accessToken, DateTime now);
      Task RemoveExpiredRefreshTokens(DateTime now);
      Task RemoveRefreshTokenByUserNameAsync(int userId);
      (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token);
   }

   public class JwtAuthManager : IJwtAuthManager
   {
      private readonly PearContext _context;  // can store in a database or a distributed cache
      private readonly JwtTokenConfig _jwtTokenConfig;
      private readonly byte[] _secret;


      public JwtAuthManager(JwtTokenConfig jwtTokenConfig, PearContext context)
      {
         _jwtTokenConfig = jwtTokenConfig;
         _context = context;
         _secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret!);
      }

      // optional: clean up expired refresh tokens
      public async Task RemoveExpiredRefreshTokens(DateTime now)
      {
         await _context.RefreshTokens.Where(x => x.ExpireAt < now).ExecuteDeleteAsync();
         await _context.SaveChangesAsync();

      }

      // can be more specific to ip, user agent, device name, etc.
      public async Task RemoveRefreshTokenByUserNameAsync(int userId)
      {
         await _context.RefreshTokens.Where(x => x.UserId == userId).ExecuteDeleteAsync();
         await _context.SaveChangesAsync();

      }

      public async Task<JwtAuthResult> GenerateTokens(long userId, Claim[] claims, DateTime now)
      {
         var id = (int)userId;
         var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
         var jwtToken = new JwtSecurityToken(
             _jwtTokenConfig.Issuer,
             shouldAddAudienceClaim ? _jwtTokenConfig.Audience : string.Empty,
             claims,
             expires: now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration).ToUniversalTime(),
             signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature));
         var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

         var refreshToken = new RefreshToken
         {
            UserId = id,
            TokenString = GenerateRefreshTokenString(),
            ExpireAt = now.AddMinutes(_jwtTokenConfig.RefreshTokenExpiration).ToUniversalTime()
         };
         await _context.RefreshTokens.AddAsync(refreshToken);
         await _context.SaveChangesAsync();

         return new JwtAuthResult
         {
            AccessToken = accessToken,
            RefreshToken = refreshToken
         };
      }

      public async Task<JwtAuthResult> RefreshAsync(string refreshToken, string accessToken, DateTime now)
      {
         var (principal, jwtToken) = DecodeJwtToken(accessToken);
         if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
         {
            throw new SecurityTokenException("Invalid token");
         }

         int userId = principal.GetLoggedInUserId<int>();
         var token = await _context.RefreshTokens.Where(x => x.TokenString == refreshToken).FirstOrDefaultAsync();
         if (token == null)
         {
            throw new SecurityTokenException("Invalid token");
         }
         if (token.UserId != userId || token.ExpireAt < now.ToUniversalTime())
         {
            throw new SecurityTokenException("Invalid token");
         }

         return await GenerateTokens(userId, principal.Claims.ToArray(), now); // need to recover the original claims
      }

      public (ClaimsPrincipal, JwtSecurityToken) DecodeJwtToken(string token)
      {
         if (string.IsNullOrWhiteSpace(token))
         {
            throw new SecurityTokenException("Invalid token");
         }
         var principal = new JwtSecurityTokenHandler()
             .ValidateToken(token,
                 new TokenValidationParameters
                 {
                    ValidateIssuer = true,
                    ValidIssuer = _jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(_secret),
                    ValidAudience = _jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1)
                 },
                 out var validatedToken);
         return (principal, validatedToken! as JwtSecurityToken)!;
      }

      private static string GenerateRefreshTokenString()
      {
         var randomNumber = new byte[32];
         using var randomNumberGenerator = RandomNumberGenerator.Create();
         randomNumberGenerator.GetBytes(randomNumber);
         return Convert.ToBase64String(randomNumber);
      }

   }

   public class JwtAuthResult
   {
      [JsonPropertyName("accessToken")]
      public string? AccessToken { get; set; }

      [JsonPropertyName("refreshToken")]
      public RefreshToken? RefreshToken { get; set; }
   }

   public class RefreshToken
   {
      [JsonPropertyName("id")]
      public long Id { get; set; }

      [JsonPropertyName("userId")]
      public long UserId { get; set; }    // can be used for usage tracking
                                          // can optionally include other metadata, such as user agent, ip address, device name, and so on

      [JsonPropertyName("tokenString")]
      public string? TokenString { get; set; }

      [JsonPropertyName("expireAt")]
      public DateTime ExpireAt { get; set; } = DateTime.UtcNow;


   }
}