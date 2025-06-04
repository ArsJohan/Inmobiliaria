using InmobiliariaAPI.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InmobiliariaAPI.Controllers
{
    [RoutePrefix("api/Sedes")]
    public class SedesController : ApiController
    {
        // GET: api/Sedes/Todas
        [HttpGet]
        [Route("Todas")]
        public IHttpActionResult ConsultarTodas()
        {
            var servicio = new clsSede();
            return Ok(servicio.ConsultarTodo());
        }
    }
}