using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InmobiliariaAPI.Models;
using System.Data.Entity.Migrations;

namespace InmobiliariaAPI.Clases
{
	public class clsEstadoInmueble
	{
		private DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public ESTADO estadoInmueble { get; set; }

		public IQueryable ConsultarTodo()
		{
            return DBInmobiliaria.ESTADOes
                .Select(e => new
                {
                    Codigo = e.Codigo_Estado,
                    Nombre = e.Nombre_Estado
                });
        }
    }
}