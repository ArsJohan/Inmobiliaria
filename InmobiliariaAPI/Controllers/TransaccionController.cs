using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InmobiliariaAPI.Clases;
using InmobiliariaAPI.Models;

namespace InmobiliariaAPI.Controllers
{
    [RoutePrefix("api/Transaccion")]
    [Authorize]
    public class TransaccionController : ApiController
    {
        [HttpGet]
        [Route("ConsultarXCodigo")]
        public TRANSACCION Consultar(int Codigo)
        {
            clsTransacion clsTransacion = new clsTransacion();
            return clsTransacion.ConsultarXCodigo(Codigo);
        }
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<TRANSACCION> ConsultarTodos()
        {
            //Se crea un objeto de la clase clsEmpleado
            clsTransacion transacion = new clsTransacion(); ;
            //Se llama al método ConsultarTodos de la clase clsEmpleado
            return transacion.ConsultarTodo();
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] TRANSACCION transacion)
        {
            clsTransacion clsTransacion = new clsTransacion();
            clsTransacion.transacion = transacion;
            return clsTransacion.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] TRANSACCION transacion)
        {
            clsTransacion clsTransacion = new clsTransacion();
            clsTransacion.transacion = transacion;
            return clsTransacion.Actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int codigo)
        {
            clsTransacion clsTransacion = new clsTransacion();
            return clsTransacion.Eliminar(codigo);
        }

    }
}