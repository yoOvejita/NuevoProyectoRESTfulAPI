using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevoProyectoRESTfulAPI.DTO;
using NuevoProyectoRESTfulAPI.Models;
using NuevoProyectoRESTfulAPI.Repos;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch;
using System;
using NuevoProyectoRESTfulAPI.ComunicacionSync.Http;
using System.Threading.Tasks;
using NuevoProyectoRESTfulAPI.ComunicacionAsync;

namespace NuevoProyectoRESTfulAPI.Controllers
{
    [Route("api/estudiantes")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteRepository estRepo;
        private readonly IMapper mapper;
        private readonly ICampusHistorialCliente campusHistorialCliente;
        private readonly IBusDeMensajesCliente busDeMensajesCliente;
        public EstudianteController(IEstudianteRepository estRepo, IMapper mapper,
            ICampusHistorialCliente campusHistorialCliente, IBusDeMensajesCliente busDeMensajesCliente)
        {
            this.estRepo = estRepo;
            this.mapper = mapper;
            this.campusHistorialCliente = campusHistorialCliente;
            this.busDeMensajesCliente = busDeMensajesCliente;
        }
        [HttpGet]
        public ActionResult<IEnumerable<EstudianteReadDTO>> getestudiantes()
        {
            var estudiantes = estRepo.GetEstudiantes();
            return Ok(mapper.Map<IEnumerable<EstudianteReadDTO>>(estudiantes));
        }

        [HttpGet("{ci}", Name = "getestudiante")]
        public ActionResult<EstudianteReadDTO> getestudiante(int ci)
        {
            Estudiante est = estRepo.GetEstudianteByCi(ci);
            return Ok(mapper.Map<EstudianteReadDTO>(est));
        }

        [HttpPost]
        public async Task<ActionResult<EstudianteReadDTO>> setestudiante(EstudianteCreateDTO estCreateDTO)
        {
            Estudiante estudiante = mapper.Map<Estudiante>(estCreateDTO);
            estRepo.AddEstudiante(estudiante);
            estRepo.Guardar();
            EstudianteReadDTO estRetorno = mapper.Map<EstudianteReadDTO>(estudiante);
            try
            {
                await campusHistorialCliente.ComunicarseConCampus(estRetorno);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocurrió un error al comunicarse con Campus de forma sincronizada: {e.Message}");
            }

            //Usando RabbitMQ
            try
            {
                var estudiantePublisherDTO = mapper.Map<EstudiantePublisherDTO>(estRetorno);
                estudiantePublisherDTO.tipoEvento = "estudiante_publicado";
                busDeMensajesCliente.PublicarNuevoEstudiante(estudiantePublisherDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ocurrió un error al tratar de publicar: { e.Message}");
            }
            return CreatedAtRoute(nameof(getestudiante), new { ci = estRetorno.ci }, estRetorno);
        }

        [HttpPut("{ci}")]
        public ActionResult updateestudiante(int ci, EstudianteUpdateDTO estUpdateDTO)
        {
            Estudiante est = estRepo.GetEstudianteByCi(ci);
            if (est == null)
                return NotFound();
            mapper.Map(estUpdateDTO, est);
            estRepo.UpdateEstudiante(est);
            estRepo.Guardar();
            return NoContent();
        }

        /*
        [
            {
                "op":"replace",
                "path":"/nombre_de_atributo",
                "value": "valor nuevo"
            }
        ]
        */
        [HttpPatch("{ci}")]
        public ActionResult updatepatchestudiante(int ci, JsonPatchDocument<EstudianteUpdateDTO> estPatch)
        {
            Estudiante est = estRepo.GetEstudianteByCi(ci);
            if (est == null)
                return NotFound();
            EstudianteUpdateDTO estParaPatch = mapper.Map<EstudianteUpdateDTO>(est);
            estPatch.ApplyTo(estParaPatch, ModelState);
            if (!TryValidateModel(estParaPatch))
                return ValidationProblem(ModelState);
            mapper.Map(estParaPatch, est);
            estRepo.UpdateEstudiante(est);
            estRepo.Guardar();
            return NoContent();
        }

        [HttpDelete("{ci}")]
        public ActionResult eliminarstudiante(int ci)
        {
            Estudiante est = estRepo.GetEstudianteByCi(ci);
            if (est == null)
                return NotFound();
            
            estRepo.EliminarEstudiante(est);
            estRepo.Guardar();
            return NoContent();
        }
    }
}

