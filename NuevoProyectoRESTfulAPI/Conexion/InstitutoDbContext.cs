using Microsoft.EntityFrameworkCore;
using NuevoProyectoRESTfulAPI.Models;

namespace NuevoProyectoRESTfulAPI.Conexion
{
    public class InstitutoDbContext : DbContext
    {
        public DbSet<Estudiante> Estudiantes { get; set; }

        public InstitutoDbContext(DbContextOptions<InstitutoDbContext> opciones) : base(opciones)
        {

        }
    }
}
