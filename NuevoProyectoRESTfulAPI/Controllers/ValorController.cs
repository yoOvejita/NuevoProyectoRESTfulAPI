using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuevoProyectoRESTfulAPI.Models;
using NuevoProyectoRESTfulAPI.Repos;
using System.Collections.Generic;

namespace NuevoProyectoRESTfulAPI.Controllers
{
    [Route("api/valor")]
    [ApiController]
    public class ValorController : ControllerBase
    {
        private readonly IValorRepository repo;
        public ValorController(IValorRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("getvalores")]
        public ActionResult<IEnumerable<Valor>> GetValores()
        {
            var valores = repo.GetValores();
            return Ok(valores);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Valor>> GetValorById(int id)
        {
            var valor = repo.GetValorById(id);
            return Ok(valor);
        }
    }
}
