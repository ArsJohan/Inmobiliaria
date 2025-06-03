using InmobiliariaAPI.Clases;
using InmobiliariaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InmobiliariaAPI.Controllers
{
    [RoutePrefix("api/Inmueble")]
    public class InmuebleController : ApiController
    {
       
        
        // GET: api/Inmueble/Todos
        [HttpGet]
        [Route("Todos")]
        public IHttpActionResult ConsultarTodos()
        {
            var servicio = new clsInmueble();
            return Ok(servicio.ConsultarTodo());
        }

        // GET: api/Inmueble/PorCodigo/5
        [HttpGet]
        [Route("PorCodigo/{codigo:int}")]
        public IHttpActionResult ConsultarPorCodigo(int codigo)
        {
            var servicio = new clsInmueble();
            var result = servicio.ConsultarInmueble(codigo);
            return Ok(result);
        }

        // GET: api/Inmueble/PorTipo/Casa
        [HttpGet]
        [Route("PorTipo/{tipo}")]
        public IHttpActionResult ConsultarPorTipo(string tipo)
        {
            var servicio = new clsInmueble();
            var result = servicio.ConsultarPorTipo(tipo);
            return Ok(result);
        }

        // GET: api/Inmueble/PorPrecios?min=100000&max=500000
        [HttpGet]
        [Route("PorPrecios")]
        public IHttpActionResult ConsultarPorPrecios(decimal min, decimal max)
        {
            var servicio = new clsInmueble();
            var result = servicio.ConsultarPorPrecios(max, min);
            return Ok(result);
        }

        // POST: api/Inmueble/Insertar
        [HttpPost]
        [Route("Insertar")]
        public IHttpActionResult Insertar([FromBody] INMUEBLE inmueble)
        {
            if (inmueble == null)
                return BadRequest("Datos inválidos");

            var servicio = new clsInmueble { inmueble = inmueble };
            var mensaje = servicio.Insertar();
            if (mensaje.Contains("correctamente"))
                return Ok(mensaje);
            return Content(HttpStatusCode.BadRequest, mensaje);
        }

        // PUT: api/Inmueble/Actualizar
        [HttpPut]
        [Route("Actualizar")]
        public IHttpActionResult Actualizar([FromBody] INMUEBLE inmueble)
        {
            if (inmueble == null)
                return BadRequest("Datos inválidos");

            var servicio = new clsInmueble { inmueble = inmueble };
            var mensaje = servicio.Actualizar();
            if (mensaje.Contains("correctamente"))
                return Ok(mensaje);
            return Content(HttpStatusCode.BadRequest, mensaje);
        }

        // DELETE: api/Inmueble/Eliminar/5
        [HttpDelete]
        [Route("Eliminar/{codigo:int}")]
        public IHttpActionResult Eliminar(int codigo)
        {
            var servicio = new clsInmueble();
            var mensaje = servicio.Eliminar(codigo);
            if (mensaje.Contains("correctamente"))
                return Ok(mensaje);
            return Content(HttpStatusCode.BadRequest, mensaje);
        }
        
    }
}