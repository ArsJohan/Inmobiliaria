using InmobiliariaAPI.Clases;
using InmobiliariaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InmobiliariaAPI.Controllers
{
    public class VentaController : ApiController
    {
        [RoutePrefix("api/Venta")]
        [Authorize]
        public class clienteController : ApiController
        {
            [HttpPost]
            [Route("Insertar")]
            public string Insertar([FromBody] VENTA dato)
            {
                clsVenta venta = new clsVenta();
                venta.venta = dato;
                return venta.Insertar();
            }
            [HttpGet]
            [Route("ConsultarTodos")]
            public List<VENTA> ConsultarTodos()
            {
                //Se crea un objeto de la clase clsEmpleado
                clsVenta venta = new clsVenta(); ;
                //Se llama al método ConsultarTodos de la clase clsEmpleado
                return venta.ConsultarTodo();
            }
            [HttpGet]
            [Route("Consultar")]
            public VENTA Consultar(int Documento)
            {
                clsVenta venta = new clsVenta();
                return venta.Consultar(Documento);
            }
            [HttpPut]
            [Route("Actualizar")]
            public string Actualizar([FromBody] VENTA venta)
            {
                clsVenta vent = new clsVenta();
                vent.venta = venta;
                return vent.Actualizar();
            }
            [HttpDelete]
            [Route("Eliminar")]
            public string Eliminar([FromBody] VENTA venta)
            {
                clsVenta vent = new clsVenta();
                vent.venta = venta;
                return vent.Eliminar();
            }
        }
    }
}