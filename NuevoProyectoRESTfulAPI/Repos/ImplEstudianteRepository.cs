using NuevoProyectoRESTfulAPI.Conexion;
using NuevoProyectoRESTfulAPI.Models;
using System;
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

        public void AddEstudiante(Estudiante est)
        {
            if(est == null)
                throw new ArgumentNullException(nameof(est));
            cont.Estudiantes.Add(est);
        }

        public Estudiante GetEstudianteByCi(int ci)
        {
            return cont.Estudiantes.FirstOrDefault(est => est.ci == ci);
        }

        public IEnumerable<Estudiante> GetEstudiantes()
        {
            return cont.Estudiantes.ToList();
        }

        public bool Guardar()
        {
            return (cont.SaveChanges() > -1);
        }

        public void UpdateEstudiante(Estudiante est)
        {
            //throw new NotImplementedException();
        }
        public void EliminarEstudiante(Estudiante est)
        {
            if(est == null)
                throw new ArgumentNullException();
            cont.Estudiantes.Remove(est);
        }
    }
}
