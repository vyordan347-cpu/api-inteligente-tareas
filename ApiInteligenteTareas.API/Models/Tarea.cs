using System.ComponentModel.DataAnnotations;

namespace ApiInteligenteTareas.API.Models
{
    public class Tarea
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        public string? Descripcion { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        public string Prioridad { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [Required]
        public DateTime FechaVencimiento { get; set; }
    }
}