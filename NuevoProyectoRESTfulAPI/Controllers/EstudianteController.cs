using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public EstudianteController(IEstudianteRepository estRepo)
        {
            this.estRepo = estRepo;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Estudiante>> getestudiantes()
        {
            var estudiantes = estRepo.GetEstudiantes();
            return Ok(estudiantes);
        }

        [HttpGet("{ci}")]
        public ActionResult<Estudiante> getestudiante(int ci)
        {
            Estudiante est = estRepo.GetEstudianteByCi(ci);
            return Ok(est);
        }
    }
}
