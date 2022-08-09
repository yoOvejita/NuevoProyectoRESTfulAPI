using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Campus.Models
{
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

        public int fci { get; set; }
        public ICollection<Perfil> perfiles { get; set; }

        public Estudiante()
        {

        }
    }
}

