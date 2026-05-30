using ApiInteligenteTareas.API.Data;
using ApiInteligenteTareas.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ApiInteligenteTareas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TareasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/tareas
       [HttpGet]
public async Task<ActionResult<IEnumerable<Tarea>>> GetTareas(
    string? estado,
    string? prioridad,
    DateTime? fechaInicio,
    DateTime? fechaFin)
{
    var query = _context.Tareas.AsQueryable();

    // Validación fechas
    if (fechaInicio.HasValue && fechaFin.HasValue)
    {
        if (fechaInicio > fechaFin)
        {
            return BadRequest("fechaInicio no puede ser mayor que fechaFin");
        }
    }

    // Filtro estado
    if (!string.IsNullOrEmpty(estado))
    {
        query = query.Where(t => t.Estado == estado);
    }

    // Filtro prioridad
    if (!string.IsNullOrEmpty(prioridad))
    {
        query = query.Where(t => t.Prioridad == prioridad);
    }

    // Filtro fecha inicio
    if (fechaInicio.HasValue)
    {
        query = query.Where(t => t.FechaCreacion >= fechaInicio.Value);
    }

    // Filtro fecha fin
    if (fechaFin.HasValue)
    {
        query = query.Where(t => t.FechaCreacion <= fechaFin.Value);
    }

    return await query.ToListAsync();
}

        // GET: api/tareas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            return tarea;
        }

        // POST: api/tareas
        [HttpPost]
        public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
        {
            var estadosValidos = new[]
            {
                "Pendiente",
                "EnProceso",
                "Completada"
            };

            var prioridadesValidas = new[]
            {
                "Baja",
                "Media",
                "Alta"
            };

            // Validación estado
            if (!estadosValidos.Contains(tarea.Estado))
            {
                return BadRequest("Estado inválido");
            }

            // Validación prioridad
            if (!prioridadesValidas.Contains(tarea.Prioridad))
            {
                return BadRequest("Prioridad inválida");
            }

            // Validación fecha
            if (tarea.FechaVencimiento.Date <
                DateTime.Now.Date)
            {
                return BadRequest(
                    "La fecha de vencimiento no puede ser menor a hoy");
            }

            _context.Tareas.Add(tarea);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTarea),
                new { id = tarea.Id },
                tarea);
        }

        // PUT: api/tareas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarea(
            int id,
            Tarea tarea)
        {
            if (id != tarea.Id)
            {
                return BadRequest();
            }

            _context.Entry(tarea).State =
                EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/tareas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            _context.Tareas.Remove(tarea);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}