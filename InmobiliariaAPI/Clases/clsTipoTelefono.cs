using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using InmobiliariaAPI.Models;

namespace InmobiliariaAPI.Clases
{
    public class clsTipoTelefono
    {
        private DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public TIPO_TELEFONO tipoTelefono { get; set; }

        public IQueryable ConsultarTodos()
        {
            return DBInmobiliaria.TIPO_TELEFONO
                .Select(Tf => new
                {
                    Codigo = Tf.Codigo_TipoTelefono,
                    Nombre = Tf.Descripcion
                });
        }
    }
}