using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using AutoMapper;
using Campus.Conexion;
using Campus.DTO;
using Campus.Models;
using Microsoft.AspNetCore.Mvc;

namespace Campus.Controllers
{
    [Route("api/[controller]/{estudianteci}")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilRepository repositorio;
        private readonly IMapper mapper;
        public PerfilController(IPerfilRepository repo, IMapper mapper)
        {
            repositorio = repo;
            this.mapper = mapper;
        }
        // GET: api/perfil/123
        [HttpGet]
        public ActionResult<IEnumerable<PerfilReadDTO>> Get(int estudianteci)
        {
            if (!repositorio.ExisteEstudiante(estudianteci))
                return NotFound();
            var perfiles = repositorio.GetPerfilesDeEstudiante(estudianteci);
            return Ok(mapper.Map<IEnumerable<PerfilReadDTO>>(perfiles));
        }

        // GET api/perfil/123/2
        [HttpGet("{perfilid}", Name = "GetPerfilEstudiante")]
        public ActionResult<PerfilReadDTO> GetPerfilEstudiante(int estudianteci, int perfilid)
        {
            if (!repositorio.ExisteEstudiante(estudianteci))
                return NotFound();
            var perfil = repositorio.GetPerfil(estudianteci, perfilid);
            if (perfil == null)
                return NotFound();
            return Ok(mapper.Map<PerfilReadDTO>(perfil));
        }

        // POST api/values
        [HttpPost]
        public ActionResult<PerfilReadDTO> Post(int estudianteci, PerfilCreateDTO value)
        {
            if (!repositorio.ExisteEstudiante(estudianteci))
                return NotFound();
            Perfil perfil = mapper.Map<Perfil>(value);
            repositorio.CrearPerfil(estudianteci, perfil);
            repositorio.Guardar();
            var perfilReadDTO = mapper.Map<PerfilReadDTO>(perfil);
            return CreatedAtRoute(nameof(GetPerfilEstudiante), new { estudianteci = estudianteci, perfilid = perfilReadDTO.id }, perfilReadDTO);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
