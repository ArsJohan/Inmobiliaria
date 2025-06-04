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
    [RoutePrefix("api/Cliente")]
    public class ClienteController : ApiController
    {
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] CLIENTE dato)
        {
            clsCliente cliente = new clsCliente();
            cliente.cliente = dato;
            return cliente.Insertar();
        }
        [HttpGet]
        [Route("ConsultarTodos")]
        public List<CLIENTE> ConsultarTodos()
        {
            //Se crea un objeto de la clase clsEmpleado
            clsCliente cliente = new clsCliente();
            //Se llama al método ConsultarTodos de la clase clsEmpleado
            return cliente.ConsultarTodo();
        }
        [HttpGet]
        [Route("Consultar")]
        public CLIENTE Consultar(string documento)
        {
            clsCliente cliente = new clsCliente();
            return cliente.ConsultarXDocumento(documento);
        }
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] CLIENTE cliente)
        {
            clsCliente client = new clsCliente();
            client.cliente = cliente;
            return client.Actualizar();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(string documento)
        {
            clsCliente client = new clsCliente();
          
            return client.Eliminar(documento);
        }
    }
}