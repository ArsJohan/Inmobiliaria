using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InmobiliariaAPI.Clases;

namespace InmobiliariaAPI.Controllers
{
    [RoutePrefix("api/tipoinmueble")]
    public class TipoInmuebleController : ApiController
    {
        [HttpGet]
        [Route("ConsultarTodos")]
        public IHttpActionResult ConsultarTodos()
        {
            try
            {
                var servicio = new clsTipoInmueble();
                var lista = servicio.ConsultarTodo();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}