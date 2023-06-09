using System.Text.Json.Serialization;

namespace PearApi.Authentication
{
   public class JwtTokenConfig
   {
      [JsonPropertyName("secret")]
      public virtual string? Secret { get; set; }

      [JsonPropertyName("issuer")]
      public virtual string? Issuer { get; set; }

      [JsonPropertyName("audience")]
      public virtual string? Audience { get; set; }

      [JsonPropertyName("accessTokenExpiration")]
      public virtual int AccessTokenExpiration { get; set; }

      [JsonPropertyName("refreshTokenExpiration")]
      public virtual int RefreshTokenExpiration { get; set; }
   }
}