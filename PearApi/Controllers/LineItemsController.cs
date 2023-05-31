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
   public class LineItemController : ControllerBase
   {
      private readonly PearContext _context;

      public LineItemController(PearContext context)
      {
         _context = context;
      }

      // GET: api/LineItem
      [HttpGet]
      public async Task<ActionResult<IEnumerable<LineItem>>> GetLineItem()
      {
         if (_context.LineItems == null)
         {
            return NotFound();
         }
         return await _context.LineItems.ToListAsync();
      }

      // GET: api/LineItem/5
      [HttpGet("{id}")]
      public async Task<ActionResult<LineItem>> GetLineItem(long id)
      {
         if (_context.LineItems == null)
         {
            return NotFound();
         }
         var lineItem = await _context.LineItems.FindAsync(id);

         if (lineItem == null)
         {
            return NotFound();
         }

         return lineItem;
      }

      // PUT: api/LineItem/5
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPut("{id}")]
      public async Task<IActionResult> PutLineItem(long id, LineItem lineItem)
      {
         if (id != lineItem.Id)
         {
            return BadRequest();
         }

         _context.Entry(lineItem).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!LineItemExists(id))
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

      // POST: api/LineItem
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPost]
      public async Task<ActionResult<LineItem>> PostLineItem(LineItem lineItem)
      {
         if (_context.LineItems == null)
         {
            return Problem("Entity set 'PearContext.LineItem'  is null.");
         }
         _context.LineItems.Add(lineItem);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetLineItem", new { id = lineItem.Id }, lineItem);
      }

      // DELETE: api/LineItem/5
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteLineItem(long id)
      {
         if (_context.LineItems == null)
         {
            return NotFound();
         }
         var lineItem = await _context.LineItems.FindAsync(id);
         if (lineItem == null)
         {
            return NotFound();
         }

         _context.LineItems.Remove(lineItem);
         await _context.SaveChangesAsync();

         return NoContent();
      }

      private bool LineItemExists(long id)
      {
         return (_context.LineItems?.Any(e => e.Id == id)).GetValueOrDefault();
      }
   }
}