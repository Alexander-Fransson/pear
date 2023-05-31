using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PearApi;
using PearApi.Models;

namespace PearApi.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class OptionController : ControllerBase
   {
      private readonly PearContext _context;
      public OptionController(PearContext context)
      {
         _context = context;
      }

      // GET: api/Option

      /*
      Är av typen 
      * Task<T> = Asyncron funktion
      * ActionResult<T> = Att det är ett resultat från en controller metod som följer reglerna
         * public = andra classer kan komma åt metoden.
         * not overloadable = inga andra metoder i klassen eller dess barn får ha samma namn.
         * not static = att alla metoder i olika instanser av klassen har unik data som inte delas med dess bröder.
      * IEnumerable<T> = En interface som specifierar att resultatet är en iterable, att objekten har funktioner som säger 
         om det finns fler och pekar på var de är t.ex listor.
      * Option = Att objekten följer specifikationerna i Models.Object classen 
      */

      [HttpGet]
      public async Task<ActionResult<IEnumerable<Option>>> GetOptions()
      {
         if (_context.Options == null)
         {
            return NotFound();
         }
         return await _context.Options.ToListAsync();
      }

      // GET: api/Option/5
      [HttpGet("{id}")]
      public async Task<ActionResult<Option>> GetOption(long id)
      {
         if (_context.Options == null)
         {
            return NotFound();
         }
         var option = await _context.Options.FindAsync(id);

         if (option == null)
         {
            return NotFound();
         }

         return option;
      }

      // PUT: api/Option/5
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPut("{id}")]
      public async Task<IActionResult> PutOption(long id, Option option)
      {
         if (id != option.Id)
         {
            return BadRequest();
         }

         _context.Entry(option).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!OptionExists(id))
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

      // POST: api/Option
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPost]
      public async Task<ActionResult<Option>> PostOption(Option option)
      {
         if (_context.Options == null)
         {
            return Problem("Entity set 'PearContext.Options'  is null.");
         }
         _context.Options.Add(option);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetOption", new { id = option.Id }, option);
      }

      // DELETE: api/Option/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteOption(long id)
      {
         if (_context.Options == null)
         {
            return NotFound();
         }
         var option = await _context.Options.FindAsync(id);
         if (option == null)
         {
            return NotFound();
         }

         _context.Options.Remove(option);
         await _context.SaveChangesAsync();

         return NoContent();
      }

      private bool OptionExists(long id)
      {
         return (_context.Options?.Any(e => e.Id == id)).GetValueOrDefault();
      }
   }
}