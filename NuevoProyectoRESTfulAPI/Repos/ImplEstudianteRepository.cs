using NuevoProyectoRESTfulAPI.Conexion;
using NuevoProyectoRESTfulAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace NuevoProyectoRESTfulAPI.Repos
{
    public class ImplEstudianteRepository : IEstudianteRepository
    {
        private readonly InstitutoDbContext cont;
        public ImplEstudianteRepository(InstitutoDbContext contexto)
        {
            cont = contexto;
        }

        public Estudiante GetEstudianteByCi(int ci)
        {
            return cont.Estudiantes.FirstOrDefault(est => est.ci == ci);
        }

        public IEnumerable<Estudiante> GetEstudiantes()
        {
            return cont.Estudiantes.ToList();
        }
    }
}
