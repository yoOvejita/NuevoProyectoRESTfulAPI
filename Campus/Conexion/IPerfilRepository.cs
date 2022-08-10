using Campus.Models;
using System.Collections.Generic;

namespace Campus.Conexion
{
    public interface IPerfilRepository
    {
        //Para estudiantes
        IEnumerable<Estudiante> GetEstudiantes();
        void CrearEstudiante(Estudiante est);
        bool ExisteEstudiante(int ci);

        //Para perfiles
        Perfil GetPerfil(int idperfil, int ci);
        IEnumerable<Perfil> GetPerfilesDeEstudiante(int ci);
        void CrearPerfil(int ci, Perfil perfil);

        bool Guardar();
    }
}
