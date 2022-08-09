using System;
using System.Threading.Tasks;
using NuevoProyectoRESTfulAPI.DTO;

namespace NuevoProyectoRESTfulAPI.ComunicacionSync.Http
{
    public interface ICampusHistorialCliente
    {
        Task ComunicarseConCampus(EstudianteReadDTO est);
    }
}

