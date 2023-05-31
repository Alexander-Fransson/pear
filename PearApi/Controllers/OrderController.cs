using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PearApi.Models;

namespace PearApi.Controllers
{
   [Route("api/orders")]
   [ApiController]
   public class OrderController : ControllerBase
   {
      private readonly PearContext _context;

      public OrderController(PearContext context)
      {
         _context = context;
      }

      // GET: api/orders
      [HttpGet]
      public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
      {
         if (_context.OrderItems == null)
         {
            return NotFound();
         }

         //return (JsonSerializer.Serialize(HttpContext.User));

         return await _context.OrderItems.ToListAsync();
      }

      // GET: api/Order/5
      [HttpGet("{id}")]
      public async Task<ActionResult<OrderItem>> GetOrderItem(long id)
      {
         if (_context.OrderItems == null)
         {
            return NotFound();
         }
         var orderItem = await _context.OrderItems.FindAsync(id);

         if (orderItem == null)
         {
            return NotFound();
         }

         return orderItem;
      }

      // PUT: api/Order/5
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPut("{id}")]
      public async Task<IActionResult> PutOrderItem(long id, OrderItem orderItem)
      {
         if (id != orderItem.Id)
         {
            return BadRequest();
         }

         _context.Entry(orderItem).State = EntityState.Modified;

         try
         {
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!OrderItemExists(id))
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

      // POST: api/Order
      // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
      [HttpPost]
      public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem orderItem)
      {
         if (_context.OrderItems == null)
         {
            return Problem("Entity set 'PearContext.OrderItems'  is null.");
         }
         _context.OrderItems.Add(orderItem);
         await _context.SaveChangesAsync();

         return CreatedAtAction("GetOrderItem", new { id = orderItem.Id }, orderItem);
      }

      // DELETE: api/Order/5
      [HttpDelete("{id}")]
      [Authorize(Roles = "Admin")]
      public async Task<IActionResult> DeleteOrderItem(long id)
      {
         if (_context.OrderItems == null)
         {
            return NotFound();
         }
         var orderItem = await _context.OrderItems.FindAsync(id);
         if (orderItem == null)
         {
            return NotFound();
         }

         _context.OrderItems.Remove(orderItem);
         await _context.SaveChangesAsync();

         return NoContent();
      }

      private bool OrderItemExists(long id)
      {
         return (_context.OrderItems?.Any(e => e.Id == id)).GetValueOrDefault();
      }
   }
}