using System.Security.Cryptography;
using System.Text;
using PearApi.Models;
using Microsoft.EntityFrameworkCore;

namespace PearApi.Services
{
   public interface IAuthService
   {
      Task<bool> IsAnExistingUser(string userName);
      Task<UserModel?> IsValidUserCredentials(string userName, string password);
      Task<UserModel?> GetUser(int userId);
      Task<UserModel?> GetUser(string userEmail);
      Task<UserModel> CreateUser(UserRegisterCredentials user);
      Task UpdateLatestSignIn(long UserId);
   }

   public class AuthService : IAuthService
   {
      private readonly ILogger<AuthService> _logger;
      private readonly PearContext _context;


      private readonly List<UserModel> _users = new List<UserModel> { };
      // inject your database here for user validation
      public AuthService(ILogger<AuthService> logger, PearContext context)
      {
         _logger = logger;
         _context = context;
      }

      public async Task<UserModel?> IsValidUserCredentials(string email, string password)
      {
         var user = await _context.Users.Where(x => x.Email == email).SingleOrDefaultAsync();
         if (user == null)
         {
            return null;
         }

         _logger.LogInformation($"Validating user [{user.Email}]");

         if (string.IsNullOrWhiteSpace(password))
         {
            return null;
         }

         var hmac = new HMACSHA512(user.PasswordSalt!);
         var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
         for (int i = 0; i < computedHash.Length; i++)
         {
            if (computedHash[i] != user.PasswordHash![i]) return null;
         }

         return user;
      }

      public async Task<bool> IsAnExistingUser(string email)
      {
         var count = await _context.Users.CountAsync(x => x.Email == email);
         return count >= 1;
      }

      public async Task<UserModel?> GetUser(string email)
      {
         var user = await _context.Users.Where(x => x.Email == email).SingleOrDefaultAsync();
         return user;
      }

      public async Task<UserModel?> GetUser(int userId)
      {
         var user = await _context.Users.FindAsync(userId);
         return user;
      }

      public async Task<UserModel> CreateUser(UserRegisterCredentials user)
      {

         var hmac = new HMACSHA512();

         UserModel newUser = new UserModel
         {
            Name = user.Name,
            Email = user.Email,
            PasswordSalt = hmac.Key,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
            Adress = user.Adress,
            Phone = user.Phone,
            Role = user.Role ?? UserRoles.Customer,
         };

         var result = await _context.Users.AddAsync(newUser);
         await _context.SaveChangesAsync();
         return result.Entity;
      }

      public async Task UpdateLatestSignIn(long UserId)
      {
         await _context.Users.Where(x => x.Id == UserId).ExecuteUpdateAsync(x => x.SetProperty(b => b.LastSignIn, (_) => DateTime.UtcNow));
         await _context.SaveChangesAsync();
      }
   }

   public static class UserRoles
   {
      public const string Admin = nameof(Admin);
      public const string Customer = nameof(Customer);
   }
}