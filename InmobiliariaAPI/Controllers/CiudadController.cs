using InmobiliariaAPI.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InmobiliariaAPI.Controllers
{
    [RoutePrefix("api/Ciudad")]
    [AllowAnonymous]
    public class CiudadController : ApiController
    {
         
        // GET: api/Ciudad/Todas
        [HttpGet]
        [Route("Todas")]
        public IHttpActionResult ConsultarPorDpto(int idDpto)
        {
            var servicio = new clsCiudad();
            return Ok(servicio.ConsultarPorDpto(idDpto));
        }
    }
}