
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Campus.Models
{
    public class Perfil
    {
        [Key]
        [Required]
        public int id { get; set; }
        [Required]
        public string nick { get; set; }
        [Required]
        public string descripcion { get; set; }
        public string[] lenguajes { get; set; }

        [Required]
        public int estudianteCI { get; set; }
        public Estudiante estudiante { get; set; }
        public Perfil()
        {
        }
    }
}

