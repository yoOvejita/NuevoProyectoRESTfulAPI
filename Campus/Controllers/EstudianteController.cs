using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using AutoMapper;
using Campus.Conexion;
using Campus.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Campus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IPerfilRepository repositorio;
        private readonly IMapper mapper;
        public EstudianteController(IPerfilRepository repo, IMapper mapper)
        {
            repositorio = repo;
            this.mapper = mapper;
        }
        // GET: api/values
        [HttpGet]
        public ActionResult<IEnumerable<EstudianteReadDTO>> Get()
        {
            var estudiantes = repositorio.GetEstudiantes();
            return Ok(mapper.Map<IEnumerable<EstudianteReadDTO>>(estudiantes));
        }
    }
}
