using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InmobiliariaAPI.Models;
using System.Data.Entity.Migrations;

namespace InmobiliariaAPI.Clases
{
	public class clsSede
	{
        DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public SEDE sede { get; set; }
        public IQueryable ConsultarTodo()
        {
            return DBInmobiliaria.SEDEs
                .Select(s => new
                {
                    Codigo = s.Codigo_Sede,
                    Nombre = s.Nombre_Sede
                });
        }
    }
}