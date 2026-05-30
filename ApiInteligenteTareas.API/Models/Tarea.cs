using System.ComponentModel.DataAnnotations;

namespace ApiInteligenteTareas.API.Models
{
    public class Tarea
    {
        [Key]
        public int Id { get; set; }

        public required string Titulo { get; set; }

        public string? Descripcion { get; set; }

        public required string Estado { get; set; }

        public required string Prioridad { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime FechaVencimiento { get; set; }
    }
}