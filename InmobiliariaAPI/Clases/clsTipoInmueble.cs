using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InmobiliariaAPI.Models;


namespace InmobiliariaAPI.Clases
{
	public class clsTipoInmueble
	{
        DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public TIPO_INMUEBLE tipoInmueble { get; set; }
        public IQueryable ConsultarTodo()
        {
            return DBInmobiliaria.TIPO_INMUEBLE
                .Select(ti => new
                {
                    Codigo = ti.Codigo_TipoInmueble,
                    Nombre = ti.Descripcion
                });
        }
    }
}