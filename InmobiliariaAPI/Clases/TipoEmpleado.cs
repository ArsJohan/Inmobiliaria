using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using InmobiliariaAPI.Models;

namespace InmobiliariaAPI.Clases
{
	public class TipoEmpleado
	{
        DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public TIPO_EMPLEADO tipoEmpleado { get; set; }

        public IQueryable ConsultarTodos()
        {
            return DBInmobiliaria.TIPO_EMPLEADO
                .Select(E => new
                {
                    Codigo = E.Codigo_TipoEmpleado,
                    Nombre = E.Descripcion
                });
        }
    }
}