using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InmobiliariaAPI.Models;
using InmobiliariaAPI.Clases;

namespace InmobiliariaAPI.Controllers
{
    [RoutePrefix("api/Empleado")]
    [Authorize]
    public class EmpleadoController : ApiController
    {
       
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
            public List<EMPLEADO> ConsultarTodos()
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
            public string Eliminar([FromBody] EMPLEADO empleado)
            {
                clsEmpleado emplead = new clsEmpleado();
                emplead.empleado = empleado;
                return emplead.Eliminar();
            }
        
    }
}