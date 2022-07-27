using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevoProyectoRESTfulAPI.DTO;
using NuevoProyectoRESTfulAPI.Models;
using NuevoProyectoRESTfulAPI.Repos;
using System.Collections.Generic;

namespace NuevoProyectoRESTfulAPI.Controllers
{
    [Route("api/estudiantes")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteRepository estRepo;
        private readonly IMapper mapper;
        public EstudianteController(IEstudianteRepository estRepo, IMapper mapper)
        {
            this.estRepo = estRepo;
            this.mapper = mapper;
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
        public ActionResult<EstudianteReadDTO> setestudiante(EstudianteCreateDTO estCreateDTO)
        {
            Estudiante estudiante = mapper.Map<Estudiante>(estCreateDTO);
            estRepo.AddEstudiante(estudiante);
            estRepo.Guardar();
            EstudianteReadDTO estRetorno = mapper.Map<EstudianteReadDTO>(estudiante);
            return CreatedAtRoute(nameof(getestudiante), new { ci = estRetorno.ci}, estRetorno);
        }
    }
}
