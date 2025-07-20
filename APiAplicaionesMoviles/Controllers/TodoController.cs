using APiAplicaionesMoviles.Interfaces;
using APiAplicaionesMoviles.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APiAplicaionesMoviles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public readonly ITodo _todoService;
        public TodoController(ITodo todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            try
            {
                var todos = await _todoService.GetTodosAsync();
                return Ok(todos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving todos: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodo(Guid id)
        {
            try
            {
                var todo = await _todoService.GetTodoAsync(id);
                return Ok(todo);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Todo with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving todo: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTodo([FromBody] ToDo todo)
        {
            if (todo == null)
            {
                return BadRequest("Todo cannot be null.");
            }
            try
            {
                var createdTodo = await _todoService.AddTodoAsync(todo);
                return CreatedAtAction(nameof(GetTodo), new { id = createdTodo.Id }, createdTodo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding todo: {ex.Message}");
            }
        }

        [HttpPost("Complete/{id}")]
        public async Task<IActionResult> MarkTodoAsCompleted(Guid id)
        {
            try
            {
                var result = await _todoService.MarkTodoAsCompletedAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound($"Todo with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error marking todo as completed: {ex.Message}");
            }
        }

        [HttpPost("Pending/{id}")]
        public async Task<IActionResult> MarkTodoAsPending(Guid id)
        {
            try
            {
                var result = await _todoService.MarkTodoAsPendingAsync(id);
                if (result)
                {
                    return NoContent();
                }
                return NotFound($"Todo with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error marking todo as completed: {ex.Message}");
            }
        }
    }
}
