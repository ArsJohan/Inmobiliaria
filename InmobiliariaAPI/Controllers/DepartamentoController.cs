using InmobiliariaAPI.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InmobiliariaAPI.Controllers
{
    [RoutePrefix("api/Departamento")]
    [AllowAnonymous]
    public class DepartamentoController : ApiController
    {
       
        // GET: api/Departamento/Todos
        [HttpGet]
        [Route("Todos")]
        public IHttpActionResult ConsultarTodos()
        {
            var servicio = new clsDepartamento();
            return Ok(servicio.ConsultarTodo());
        }

       
    }
}