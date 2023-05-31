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
   public class OptionGroupController : ControllerBase
   {
      private readonly PearContext _context;

      public OptionGroupController(PearContext context)
      {
         _context = context;
      }

      // GET: api/OptionGroup
      [HttpGet]
      public async Task<ActionResult<IEnumerable<OptionGroup>>> GetOptionGroups()
      {
         if (_context.OptionGroups == null)
         {
            return NotFound();
         }
         return await _context.OptionGroups.ToListAsync();
      }

      // GET: api/OptionGroup/5
      [HttpGet("{id}")]
      public async Task<ActionResult<OptionGroup>> GetOptionGroup(long id)
      {
         if (_context.OptionGroups == null)
         {
            return NotFound();
         }
         var optionGroup = await _context.OptionGroups.
            Include(og => og.OptionRelations)!.
            ThenInclude(og => og.Option).
            FirstOrDefaultAsync(o => o.Id == id);

         if (optionGroup == null)
         {
            return NotFound();
         }

         return optionGroup;
      }

      // PUT: api/OptionGroup/5
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPut("{id}")]
      public async Task<IActionResult> PutOptionGroup(long id, OptionGroup optionGroup)
      {
         if (id != optionGroup.Id)
         {
            return BadRequest();
         }

         _context.Entry(optionGroup).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!OptionGroupExists(id))
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

      // POST: api/OptionGroup
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPost]
      public async Task<ActionResult<OptionGroup>> PostOptionGroup(OptionGroup optionGroup)
      {
         if (_context.OptionGroups == null)
         {
            return Problem("Entity set 'PearContext.OptionGroups'  is null.");
         }
         _context.OptionGroups.Add(optionGroup);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetOptionGroup", new { id = optionGroup.Id }, optionGroup);
      }

      // DELETE: api/OptionGroup/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteOptionGroup(long id)
      {
         if (_context.OptionGroups == null)
         {
            return NotFound();
         }
         var optionGroup = await _context.OptionGroups.FindAsync(id);
         if (optionGroup == null)
         {
            return NotFound();
         }

         _context.OptionGroups.Remove(optionGroup);
         await _context.SaveChangesAsync();

         return NoContent();
      }

      private bool OptionGroupExists(long id)
      {
         return (_context.OptionGroups?.Any(e => e.Id == id)).GetValueOrDefault();
      }
   }
}