using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InmobiliariaAPI.Models;
using System.Data.Entity.Migrations;

namespace InmobiliariaAPI.Clases
{
	public class clsEstadoPago
	{
        private DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public ESTADO_PAGO estadoPago { get; set; }


        public IQueryable consultarTodos()
        {
            return DBInmobiliaria.ESTADO_PAGO
                .Select(ep => new
                {
                    Codigo = ep.Codigo_EstadoPago,
                    Nombre = ep.Nombre_EstadoPago
                });
                
        }
    }
}