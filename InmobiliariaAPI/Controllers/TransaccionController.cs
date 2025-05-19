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
    [RoutePrefix("api/Transacion")]
    [Authorize]
    public class TransaccionController : ApiController
    {
        [HttpGet]
        [Route("ConsultarXCodigo")]
        public TRANSACCION Consultar(int Condigo_transacion)
        {
            clsTransacion clsTransacion = new clsTransacion();
            return clsTransacion.ConsultarXCodigo(Condigo_transacion);
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
        public string Eliminar(int Condigo_transacion)
        {
            clsTransacion clsTransacion = new clsTransacion();
            return clsTransacion.Eliminar(Condigo_transacion);
        }

    }
}