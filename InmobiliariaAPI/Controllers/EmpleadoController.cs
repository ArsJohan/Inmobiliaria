using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InmobiliariaAPI.Models;
using InmobiliariaAPI.Clases;
using System.Web.Http.Cors;

namespace InmobiliariaAPI.Controllers
{
    [RoutePrefix("api/Empleado")]
    [AllowAnonymous]
    public class EmpleadoController : ApiController
    {


            // GET: api/Empleado/Consultar
            [HttpGet]
            [Route("ConsultarPorUsuario")]
            public IHttpActionResult ConsultarPorUsuario(string usuario)
            {
                clsEmpleado empleado = new clsEmpleado();
                var resultado = empleado.ConsultarPorUsuario(usuario);
                if (resultado != null)
                {
                    return Ok(resultado);
                }
                return NotFound();
            }

            [HttpPost]
            [Route("Insertar")]
            public string Insertar([FromBody] EMPLEADO dato)
            {
                clsEmpleado empleado = new clsEmpleado();
                empleado.empleado = dato;
                return empleado.Insertar();
            }
            [HttpGet]
            [Route("ConsultarTodos")]
            public IQueryable ConsultarTodos()
            {
                //Se crea un objeto de la clase clsEmpleado
                clsEmpleado empleado = new clsEmpleado();
                //Se llama al método ConsultarTodos de la clase clsEmpleado
                return empleado.ConsultarTodo();
            }
            [HttpGet]
            [Route("Consultar")]
            public EMPLEADO Consultar(string Documento)
            {
                clsEmpleado empleado = new clsEmpleado();
                return empleado.ConsultarXDocumento(Documento);
            }
            [HttpPut]
            [Route("Actualizar")]
            public string Actualizar([FromBody] EMPLEADO empleado)
            {
                clsEmpleado emplead = new clsEmpleado();
                emplead.empleado = empleado;
                return emplead.Actualizar();
            }
            [HttpDelete]
            [Route("Eliminar")]
            public string Eliminar(string documento)
            {
                clsEmpleado emplead = new clsEmpleado();
                return emplead.Eliminar(documento);
            }
        
    }
}