using InmobiliariaAPI.Clases;
using InmobiliariaAPI.Models;
using System.Net;
using System.Web.Http;

namespace InmobiliariaAPI.Controllers
{
    [RoutePrefix("api/TipoTransaccion")]
    [AllowAnonymous]
    public class TipoTransaccionController : ApiController
    {
       
        // GET: api/TipoTransaccion/Todos
        [HttpGet]
        [Route("Todos")]
        public IHttpActionResult ConsultarTodos()
        {
            var servicio = new clsTipoTransaccion();
            return Ok(servicio.ConsultarTodos());
        }
    }
}
