using InmobiliariaAPI.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InmobiliariaAPI.Controllers
{
    [RoutePrefix("api/TipoPago")]
    [AllowAnonymous]
    public class TipoPagoController : ApiController
    {
        
        // GET: api/TipoPago/Todos
        [HttpGet]
        [Route("Todos")]
        public IHttpActionResult ConsultarTodos()
        {
            var servicio = new clsTipoDePago();
            return Ok(servicio.ConsultarTodo());
        }
    }
}