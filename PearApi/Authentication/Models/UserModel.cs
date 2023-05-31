using PearApi.Services;

namespace PearApi.Models

{
   public class UserModel
   {
      public long Id { get; set; } = 0;

      public string Name { get; set; } = "";
      public string Email { get; set; } = "";
      public byte[]? PasswordSalt { get; set; }
      public byte[]? PasswordHash { get; set; }
      public string Adress { get; set; } = "";
      public string Phone { get; set; } = "";
      public string Role { get; set; } = UserRoles.Customer;

      public DateTime LastSignIn { get; set; } = DateTime.UtcNow;
      public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
      public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
      public string BannedUntil { get; set; } = "";
   }
}