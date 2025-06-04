using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using InmobiliariaAPI.Models;

namespace InmobiliariaAPI.Clases
{
	public class clsCiudad
	{
        DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public CIUDAD ciudad { get; set; }

        public IQueryable ConsultarPorDpto(int dpto)
        {
            return DBInmobiliaria.CIUDADs
                .Where(c => c.Codigo_Departamento == dpto)
                .Select(c => new
                {
                    Codigo = c.Codigo_Ciudad,
                    Nombre= c.Nombre_Ciudad
                });
        }
    }
}