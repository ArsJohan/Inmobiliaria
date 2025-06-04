using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InmobiliariaAPI.Models;
using System.Data.Entity.Migrations;

namespace InmobiliariaAPI.Clases
{
	public class clsTipoDocumento
	{
        DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public TIPO_DOCUMENTO tipoDocumento { get; set; }

        public IQueryable ConsultarTodo()
        {
            return DBInmobiliaria.TIPO_DOCUMENTO
                .Select(td => new
                {
                    Codigo = td.Codigo_doc,
                    Nombre = td.Descripcion
                });
        }

    }
}