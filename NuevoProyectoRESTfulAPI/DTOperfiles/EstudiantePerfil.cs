using AutoMapper;
using NuevoProyectoRESTfulAPI.DTO;
using NuevoProyectoRESTfulAPI.Models;

namespace NuevoProyectoRESTfulAPI.DTOperfiles
{
    public class EstudiantePerfil : Profile
    {
        public EstudiantePerfil()
        {
            CreateMap<Estudiante,EstudianteReadDTO>();
            CreateMap<EstudianteCreateDTO, Estudiante>();
        }
    }
}
