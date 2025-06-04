using InmobiliariaAPI.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InmobiliariaAPI.Controllers
{
    [RoutePrefix("api/TipoTelefono")]
    [AllowAnonymous]
    public class TipoTelefonoController : ApiController
    {
        
        // GET: api/TipoTelefono/Todos
        [HttpGet]
        [Route("Todos")]
        public IHttpActionResult ConsultarTodos()
        {
            var servicio = new clsTipoTelefono();
            return Ok(servicio.ConsultarTodos());
        }
    }
}