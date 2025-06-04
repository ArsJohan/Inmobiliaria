using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using InmobiliariaAPI.Models;

namespace InmobiliariaAPI.Clases
{
	public class clsGenero
	{
        DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public GENERO genero { get; set; }

        public IQueryable ConsultarTodos()
        {
            return DBInmobiliaria.GENEROes
                .Select(genero => new
                {
                    Codigo = genero.Codigo_Genero,
                    Nombre = genero.Descripcion
                }
                );
        }
    }
}