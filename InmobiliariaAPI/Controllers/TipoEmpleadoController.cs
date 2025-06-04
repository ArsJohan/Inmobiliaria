using InmobiliariaAPI.Clases;
using InmobiliariaAPI.Models;
using System.Net;
using System.Web.Http;

namespace InmobiliariaAPI.Controllers
{
    [RoutePrefix("api/TipoEmpleado")]
    [AllowAnonymous]
    public class TipoEmpleadoController : ApiController
    {
        
        // GET: api/TipoEmpleado/Todos
        [HttpGet]
        [Route("Todos")]
        public IHttpActionResult ConsultarTodos()
        {
            var servicio = new clsTipoEmpleado();
            return Ok(servicio.ConsultarTodos());
        }

        
    }
}
