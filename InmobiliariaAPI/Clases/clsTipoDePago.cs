using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InmobiliariaAPI.Models;
using System.Data.Entity.Migrations;

namespace InmobiliariaAPI.Clases
{
	public class clsTipoDePago
	{
		DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public TIPO_PAGO tipoPago { get; set; }

        public IQueryable ConsultarTodo()
        {
            return DBInmobiliaria.TIPO_PAGO
                .Select(tp => new
                {
                    Codigo = tp.Codigo_TipoPago,
                    Nombre = tp.Descripcion
                });
        }
    }
}