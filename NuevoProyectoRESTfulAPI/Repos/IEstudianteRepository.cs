using NuevoProyectoRESTfulAPI.Models;
using System.Collections.Generic;

namespace NuevoProyectoRESTfulAPI.Repos
{
    public interface IEstudianteRepository
    {
        IEnumerable<Estudiante> GetEstudiantes();
        Estudiante GetEstudianteByCi(int ci);
    }
}
