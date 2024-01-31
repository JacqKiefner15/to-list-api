using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using ToDoWebApi.Models;

namespace ToDoWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {

        private readonly ILogger<ToDoController> _logger;
        private readonly ToDoContext _context;

        public ToDoController(ILogger<ToDoController> logger, ToDoContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetTodos")]
       public ActionResult<IEnumerable<ToDoItem>> GetTodoItems()
        {
            var toDoItems = _context.ToDos.ToList();

            return Ok(toDoItems);
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItem>> CreateToDoItem([FromBody] ToDoItem toDoItem)
        {
            if (toDoItem == null)
            {
                return BadRequest("Invalid Data");

            }

            try
            {
                _context.ToDos.Add(toDoItem);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, toDoItem);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("(id)")]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(int id)
        {
            var toDoItem = await _context.ToDos.FindAsync(id);

            if (toDoItem == null)
            {
                return NotFound();

            }
            return Ok(toDoItem);
        }

        [HttpPut("(id)")]
        public async Task<ActionResult<ToDoItem>> UpdateToDoItem(int id, [FromBody] ToDoItem updatedToDoItem)
        {
            if (updatedToDoItem == null || id != updatedToDoItem.Id)
            {
                return BadRequest("Invalid Data");
            }

            var existingToDoItem = await _context.ToDos.FindAsync(id);

            if (existingToDoItem == null)
            {
                return NotFound();
            }

            try
            {

                existingToDoItem.Name = updatedToDoItem.Name;
                existingToDoItem.Description = updatedToDoItem.Description;
                existingToDoItem.Status = updatedToDoItem.Status;

                await _context.SaveChangesAsync();

                return Ok(existingToDoItem);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server error: {ex.Message}");
            }
        }
    }
}
