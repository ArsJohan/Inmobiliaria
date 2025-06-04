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
    [RoutePrefix("api/Arriendo")]
    [Authorize]
    public class ArriendoController : ApiController
    {
        [HttpGet]
        [Route("ConsultarXCodigo")]
        public ARRIENDO Consultar(int Codigo_Transaccion)
        {
            clsArriendo arriendo = new clsArriendo();
            return arriendo.ConsultarXCodigo(Codigo_Transaccion);
        }
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<ARRIENDO> ConsultarTodos()
        {
            //Se crea un objeto de la clase clsEmpleado
            clsArriendo arriendo = new clsArriendo(); ;
            //Se llama al método ConsultarTodos de la clase clsEmpleado
            return arriendo.ConsultarTodo();
        }

        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] ARRIENDO arriendo)
        {
            clsArriendo clsArriendo = new clsArriendo();
            clsArriendo.arriendo = arriendo;
            return clsArriendo.Insertar();
        }

        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] ARRIENDO arriendo)
        {
            clsArriendo clsArriendo = new clsArriendo();
            clsArriendo.arriendo = arriendo;
            return clsArriendo.Actualizar();
        }

        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(int Codigo_Transaccion)
        {
            clsArriendo clsArriendo = new clsArriendo();
            return clsArriendo.Eliminar(Codigo_Transaccion);
        }
    }
}