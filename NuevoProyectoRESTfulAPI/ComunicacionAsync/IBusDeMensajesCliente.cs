using NuevoProyectoRESTfulAPI.DTO;

namespace NuevoProyectoRESTfulAPI.ComunicacionAsync
{
    public interface IBusDeMensajesCliente
    {
        void PublicarNuevoEstudiante(EstudiantePublisherDTO estudiantePublisherDTO);
    }
}
