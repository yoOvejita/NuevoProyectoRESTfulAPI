using NuevoProyectoRESTfulAPI.Models;
using System.Collections.Generic;

namespace NuevoProyectoRESTfulAPI.Repos
{
    public interface IEstudianteRepository
    {
        IEnumerable<Estudiante> GetEstudiantes();
        Estudiante GetEstudianteByCi(int ci);
        void AddEstudiante(Estudiante est);
        bool Guardar();
        void UpdateEstudiante(Estudiante est);
        void EliminarEstudiante(Estudiante est);
    }
}
