using Campus.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Campus.Conexion
{
    public class ImplPerfilRepository : IPerfilRepository
    {
        private readonly CampusDbContext contexto;
        public ImplPerfilRepository(CampusDbContext contexto)
        {
            this.contexto = contexto;
        }

        //Para estudiantes
        public void CrearEstudiante(Estudiante est)
        {
            if (est == null)
                throw new ArgumentNullException(nameof(est));
            else
                contexto.Estudiantes.Add(est);
        }
        public bool ExisteEstudiante(int ci)
        {
            return contexto.Estudiantes.Where(es => es.ci == ci).First() != null;
        }

        public IEnumerable<Estudiante> GetEstudiantes()
        {
            return contexto.Estudiantes.ToList();
        }

        //Para perfiles
        public void CrearPerfil(int ci, Perfil perfil)
        {
            if (perfil == null)
                throw new ArgumentNullException(nameof(perfil));
            else
            {
                perfil.estudianteCI = ci;
                contexto.Perfiles.Add(perfil);
            }

        }


        public Perfil GetPerfil(int idperfil, int ci)
        {
            return contexto.Perfiles.Where(per => per.id == idperfil && per.estudianteCI == ci).FirstOrDefault();
        }

        public IEnumerable<Perfil> GetPerfilesDeEstudiante(int ci)
        {
            return contexto.Perfiles.Where(p => p.estudianteCI == ci).ToList();
        }
        //Neutral
        public bool Guardar()
        {
            return (contexto.SaveChanges() >= 0);
        }
    }
}
