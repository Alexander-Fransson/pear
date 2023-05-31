using System.Security.Claims;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PearApi.Services;
using PearApi.Authentication;
using PearApi.Models;
using PearApi.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace AuthTest.Controllers;

[ApiController]
[Route("api/auth")]
[Authorize]
public class AuthController : ControllerBase
{
   private readonly ILogger<AuthController> _logger;
   private readonly IAuthService _auth;
   private readonly IJwtAuthManager _jwt;

   public AuthController(ILogger<AuthController> logger, IAuthService auth, IJwtAuthManager jwtAuthManager)
   {
      _logger = logger;
      _auth = auth;
      _jwt = jwtAuthManager;
   }

   [HttpPost("login")]
   [AllowAnonymous]
   public async Task<ActionResult> Login([FromBody] UserLoginCredentials credentials)
   {
      if (_auth == null)
      {
         return new BadRequestResult();
      }

      var user = await _auth.IsValidUserCredentials(credentials.Email, credentials.Password);
      if (user == null)
      {
         return Unauthorized();
      }

      var claims = new[]
              {
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("role", user.Role)
            };

      var jwtResult = await _jwt.GenerateTokens(user.Id, claims, DateTime.Now);
      await _auth.UpdateLatestSignIn(user.Id);
      _logger.LogInformation($"User [{credentials.Email}] logged in the system.");
      return Ok(new LoginResult
      {
         Id = user.Id,
         Role = user.Role,
         AccessToken = jwtResult.AccessToken,
         RefreshToken = jwtResult.RefreshToken!.TokenString
      });
   }

   [HttpPost("register")]
   [AllowAnonymous]
   public async Task<ActionResult> Register([FromBody] UserRegisterCredentials credentials)
   {
      if (_auth == null)
      {
         return new BadRequestResult();
      }

      var IsExistingUser = await _auth.IsAnExistingUser(credentials.Email);
      if (IsExistingUser)
      {
         return new ConflictResult();
      }

      var user = await _auth.CreateUser(credentials);
      _logger.LogInformation($"\n created user: {user.Id}");
      var claims = new[]
      {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("role", user.Role)
            };

      var jwtResult = await _jwt.GenerateTokens(user.Id, claims, DateTime.Now);
      _logger.LogInformation($"User [{credentials.Email}] logged in the system.");
      return Ok(new LoginResult
      {
         Id = user.Id,
         Email = user.Email,
         Role = user.Role,
         AccessToken = jwtResult.AccessToken,
         RefreshToken = jwtResult.RefreshToken!.TokenString
      });
   }

   [HttpPost("refresh-token")]
   [Authorize]
   public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
   {
      try
      {
         var userName = User.GetLoggedInUserEmail();
         var userId = User.GetLoggedInUserId<long>();
         _logger.LogInformation($"User [{userName}] is trying to refresh JWT token.");

         if (string.IsNullOrWhiteSpace(request.RefreshToken))
         {
            return Unauthorized();
         }

         var accessToken = await HttpContext.GetTokenAsync("Bearer", "access_token");
         var jwtResult = await _jwt.RefreshAsync(request.RefreshToken, accessToken!, DateTime.Now);

         _logger.LogInformation($"User [{userName}] has refreshed JWT token.");
         return Ok(new LoginResult
         {
            Id = userId,
            Email = userName,
            Role = User.GetLoggedInUserRole(),
            AccessToken = jwtResult.AccessToken,
            RefreshToken = jwtResult.RefreshToken!.TokenString
         });
      }
      catch (SecurityTokenException e)
      {
         return Unauthorized(e.Message); // return 401 so that the client side can redirect the user to login page
      }
   }

   [HttpGet("whoami")]
   [Authorize]
   public ActionResult GetCurrentUser()
   {

      return Ok(new LoginResult
      {
         Id = User.GetLoggedInUserId<int>(),
         Email = User.GetLoggedInUserEmail(),
         Role = User.GetLoggedInUserRole(),
      });
   }

   public class LoginResult
   {
      [JsonPropertyName("id")]
      public long Id { get; set; }
      [JsonPropertyName("email")]
      public string? Email { get; set; }

      [JsonPropertyName("role")]
      public string? Role { get; set; }

      [JsonPropertyName("accessToken")]
      public string? AccessToken { get; set; }

      [JsonPropertyName("refreshToken")]
      public string? RefreshToken { get; set; }
   }

   public class RefreshTokenRequest
   {
      [JsonPropertyName("refreshToken")]
      public string? RefreshToken { get; set; }
   }

}