using Microsoft.AspNetCore.Mvc;
using ApiInteligenteTareas.API.DTOs;

namespace ApiInteligenteTareas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public TodosController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodos()
        {
            var todos = await _httpClient.GetFromJsonAsync<List<TodoDto>>(
                "https://jsonplaceholder.typicode.com/todos"
            );

            if (todos == null)
            {
                return NotFound();
            }

            return Ok(todos);
        }
    }
}