using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoApp.Data;
using todoApp.models;

namespace todoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetItem(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] TodoItem updatedItem)
        {
            {
                var item = await _context.TodoItems.FindAsync(id);
                if (item == null) return NotFound(new {message = "Todo not found!"});

                //else
                item.Title = updatedItem.Title;
                item.Description = updatedItem.Description;
                item.IsCompleted = updatedItem.IsCompleted;

                await _context.SaveChangesAsync();
                return Ok(new {message = "Item updated successfully!"});
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null) return NotFound(new {message = "Todo not found!"});
            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
            return Ok(new {message = "Todo Deleted successfully!"});
        }

    }
}
