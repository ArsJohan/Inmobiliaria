using InmobiliariaAPI.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InmobiliariaAPI.Controllers
{
    
    [RoutePrefix("api/Genero")]
    [AllowAnonymous]
    public class GeneroController : ApiController
    {
        // GET: api/Genero/Todos
        [HttpGet]
        [Route("Todos")]
        public IHttpActionResult ConsultarTodos()
        {
            var servicio = new clsGenero();
            return Ok(servicio.ConsultarTodos());
        }
    }
}