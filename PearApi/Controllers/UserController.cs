using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PearApi.Models;

namespace PearApi.Controllers;

[Route("api/[controller]")]
[ApiController]


public class UserController : ControllerBase
{
   private readonly PearContext _context;

   public UserController(PearContext context)
   {
      _context = context;
   }

   // GET: api/Users
   [HttpGet]
   public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
   {
      if (_context.Users == null)
      {
         return NotFound();
      }
      return await _context.Users.ToListAsync();
   }

   // GET: api/User/5
   [HttpGet("{id}")]
   public async Task<ActionResult<UserModel>> GetUser(long id)
   {
      if (_context.Users == null)
      {
         return NotFound();
      }
      var User = await _context.Users.FindAsync(id);

      if (User == null)
      {
         return NotFound();
      }

      return User;
   }

   // PUT: api/User/5
   // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
   [HttpPut("{id}")]
   public async Task<IActionResult> PutUser(long id, UserModel User)
   {
      if (id != User.Id)
      {
         return BadRequest();
      }

      _context.Entry(User).State = EntityState.Modified;

      try
      {
         await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
         if (!UserExists(id))
         {
            return NotFound();
         }
         else
         {
            throw;
         }
      }

      return NoContent();
   }

   // POST: api/User
   // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
   [HttpPost]
   public async Task<ActionResult<UserModel>> PostUser(UserModel User)
   {
      if (_context.Users == null)
      {
         return Problem("Entity set 'UserContext.Users' is null.");
      }
      _context.Users.Add(User);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetUser", new { id = User.Id }, User);
   }

   // DELETE: api/User/5
   [HttpDelete("{id}")]
   public async Task<IActionResult> DeleteUser(long id)
   {
      if (_context.Users == null)
      {
         return NotFound();
      }
      var User = await _context.Users.FindAsync(id);
      if (User == null)
      {
         return NotFound();
      }

      _context.Users.Remove(User);
      await _context.SaveChangesAsync();

      return NoContent();
   }

   private bool UserExists(long id)
   {
      return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
   }
}