using InmobiliariaAPI.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InmobiliariaAPI.Controllers
{
    [RoutePrefix("api/TipoDocumento")]
    [AllowAnonymous]
    public class TipoDocumentoController : ApiController
    {
       
        // GET: api/TipoDocumento/Todos
        [HttpGet]
        [Route("Todos")]
        public IHttpActionResult ConsultarTodos()
        {
            var servicio = new clsTipoDocumento();
            return Ok(servicio.ConsultarTodo());
        }

    }
}