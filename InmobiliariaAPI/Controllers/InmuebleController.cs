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

        // GET: api/Inmueble/Buscar
        [HttpGet]
        [Route("Buscar")]
        public IHttpActionResult BuscarInmuebles(int tipo = 0, decimal? precioMin = null, decimal? precioMax = null, int pagina = 1, int tamanioPagina = 6)
        {
            var servicio = new clsInmueble();

            var consulta = servicio.ConsultarInmuebles(tipo, precioMin, precioMax);

            // Si 'consulta' no es IQueryable<T>, intenta convertir:
            var resultado = consulta.AsQueryable()
                                   .Skip((pagina - 1) * tamanioPagina)
                                   .Take(tamanioPagina)
                                   .ToList();

            return Ok(resultado);
        }

        [HttpGet]
        [Route("ConsultarPorCodigo/{codigo:int}")]
        public IHttpActionResult ConsultarPorCodigo(int codigo)
        {
            var servicio = new clsInmueble();
            var inmueble = servicio.ConsultarInmueblesId(codigo);
            if (inmueble == null)
                return NotFound();
            return Ok(inmueble);
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