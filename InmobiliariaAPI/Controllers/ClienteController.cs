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
    [Authorize]
    public class clienteController : ApiController
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
        public CLIENTE Consultar(int Documento)
        {
            clsCliente cliente = new clsCliente();
            return cliente.Consultar(Documento);
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
        public string Eliminar([FromBody] CLIENTE cliente)
        {
            clsCliente client = new clsCliente();
            client.cliente = cliente;
            return client.Eliminar();
        }
    }
}