using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InmobiliariaAPI.Models;
using System.Data.Entity.Migrations;

namespace InmobiliariaAPI.Clases
{
	public class clsTipoTransaccion
	{
        DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public TIPO_TRANSACCION tipoTransaccion { get; set; }

        public IQueryable ConsultarTodos()
        {
            return DBInmobiliaria.TIPO_TRANSACCION
                .Select(Tt => new
                {
                    Codigo = Tt.Codigo_TipoTransaccion,
                    Nombre = Tt.Descripcion
                });
        } 

       

    }
}