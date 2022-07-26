using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NuevoProyectoRESTfulAPI.Models
{
    [Table("Estudiante")]
    public class Estudiante
    {
        [Key]
        public int ci { get; set; }
        [Required]
        [MaxLength(50)]
        public string nombre { get; set; }
        [MaxLength(50)]
        [Required]
        public string apellido { get; set; }
        [Required]
        public DateTime fecha_nac { get; set; }
        [MaxLength(100)]
        public string email { get; set; }
        [MaxLength(60)]
        public string direccion { get; set; }
    }
}
