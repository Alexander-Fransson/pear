using PearApi.Services;

namespace PearApi.Models;

public class UserRegisterCredentials
{
   public string Name { get; set; } = "";
   public string Email { get; set; } = "";
   public string Password { get; set; } = "";
   public string Adress { get; set; } = "";
   public string Phone { get; set; } = "";
   public string Role { get; set; } = UserRoles.Customer;
}