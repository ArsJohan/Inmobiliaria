using InmobiliariaAPI.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace InmobiliariaAPI.Controllers
{
    public class ImaganesInmuebleController : ApiController
    {
        [RoutePrefix("api/ImagenesInmueble")]
        [Authorize]
        public class ImagenesInmuebleController : ApiController
        {
            private readonly clsImagenInmueble _servicio = new clsImagenInmueble();

            // GET: api/ImagenesInmueble/ConsultarTodos
            [HttpGet]
            [Route("ConsultarTodos")]
            public IHttpActionResult ConsultarTodos()
            {
                var lista = _servicio.ConsultarTodos();
                return Ok(lista);
            }

            // GET: api/ImagenesInmueble/ConsultarPorId/5
            [HttpGet]
            [Route("ConsultarPorId/{id:int}")]
            public IHttpActionResult ConsultarPorId(int id)
            {
                var imagen = _servicio.ConsultarPorId(id);
                if (imagen == null)
                    return NotFound();
                return Ok(imagen);
            }

            // POST: api/ImagenesInmueble/CargarArchivo?codigoInmueble=5
            [HttpPost]
            [Route("CargarArchivo")]
            public async Task<HttpResponseMessage> CargarImagen(HttpRequestMessage request, int codigoInmueble, bool esPrincipal)
            {
                return await _servicio.GrabarArchivo(request, codigoInmueble, esPrincipal);
            }

            // PUT: api/ImagenesInmueble/ActualizarArchivo?codigoInmueble=5
            [HttpPut]
            [Route("ActualizarArchivo")]
            public async Task<HttpResponseMessage> ActualizarArchivo(HttpRequestMessage request, int codigoInmueble, bool esPrincipal)
            {
                // El método GrabarArchivo ya sobreescribe si existe, así que puedes usar el mismo método
                return await _servicio.GrabarArchivo(request, codigoInmueble, esPrincipal);
            }

            // DELETE: api/ImagenesInmueble/EliminarArchivo?archivo=nombre.jpg&codigoInmueble=5
            [HttpDelete]
            [Route("EliminarArchivo")]
            public HttpResponseMessage EliminarArchivo(HttpRequestMessage request, string archivo, int codigoInmueble)
            {
                return _servicio.EliminarArchivo(request, archivo, codigoInmueble);
            }
        }
    }
}
