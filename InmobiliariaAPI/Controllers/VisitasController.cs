using InmobiliariaAPI.Clases;
using InmobiliariaAPI.Models;
using System;
using System.Net;
using System.Web.Http;

namespace InmobiliariaAPI.Controllers
{
    [RoutePrefix("api/Visitas")]
    public class VisitasController : ApiController
    {
        [Authorize]
        // GET: api/Visitas/Todas
        [HttpGet]
        [Route("Todas")]
        public IHttpActionResult ConsultarTodas()
        {
            var servicio = new clsVisita();
            return Ok(servicio.ConsultarTodos());
        }

        [Authorize]
        // GET: api/Visitas/PorInmueble/5
        [HttpGet]
        [Route("PorInmueble/{codigoInmueble:int}")]
        public IHttpActionResult ConsultarPorInmueble(int codigoInmueble)
        {
            var servicio = new clsVisita();
            return Ok(servicio.ConsultarPorInmueble(codigoInmueble));
        }

        [AllowAnonymous]
        // GET: api/Visitas/FechasDisponibles/5
        [HttpGet]
        [Route("FechasDisponibles/{codigoInmueble:int}")]
        public IHttpActionResult FechasDisponibles(int codigoInmueble)
        {
            var servicio = new clsVisita();
            return Ok(servicio.FechasDisponibles(codigoInmueble));
        }

        [AllowAnonymous]
        // POST: api/Visitas/Insertar
        [HttpPost]
        [Route("Insertar")]
        public IHttpActionResult Insertar([FromBody] VISITA visita)
        {
            if (visita == null)
                return BadRequest("Datos inválidos");

            var servicio = new clsVisita { visita = visita };
            var mensaje = servicio.Insertar();
            if (mensaje.Contains("correctamente"))
                return Ok(mensaje);
            return Content(HttpStatusCode.BadRequest, mensaje);
        }

        [Authorize]
        // PUT: api/Visitas/Actualizar
        [HttpPut]
        [Route("Actualizar")]
        public IHttpActionResult Actualizar([FromBody] VISITA visita)
        {
            if (visita == null)
                return BadRequest("Datos inválidos");

            var servicio = new clsVisita { visita = visita };
            var mensaje = servicio.Actualizar();
            if (mensaje.Contains("correctamente"))
                return Ok(mensaje);
            return Content(HttpStatusCode.BadRequest, mensaje);
        }

        [Authorize]
        // DELETE: api/Visitas/Eliminar/5
        [HttpDelete]
        [Route("Eliminar/{codigoVisita:int}")]
        public IHttpActionResult Eliminar(int codigoVisita)
        {
            var servicio = new clsVisita();
            var mensaje = servicio.Eliminar(codigoVisita);
            if (mensaje.Contains("correctamente"))
                return Ok(mensaje);
            return Content(HttpStatusCode.BadRequest, mensaje);
        }
    }
}
