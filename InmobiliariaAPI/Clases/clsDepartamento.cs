using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using InmobiliariaAPI.Models;

namespace InmobiliariaAPI.Clases
{
	public class clsDepartamento
	{
        private DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public DEPARTAMENTO departamento { get; set; }

        public IQueryable ConsultarTodo()
        {
            return DBInmobiliaria.DEPARTAMENTOes
                .Select(d => new
                {
                    Codigo = d.Codigo_Departamento,
                    Nombre = d.Nombre
                });
                
        }
        public string Insertar()
        {
            try
            {
                DBInmobiliaria.DEPARTAMENTOes.Add(departamento);
                DBInmobiliaria.SaveChanges();
                return "Departamento insertado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar el departamento: " + ex.Message;
            }
        }
        public string Actualizar()
        {
            try
            {
                DBInmobiliaria.DEPARTAMENTOes.AddOrUpdate(departamento);
                DBInmobiliaria.SaveChanges();
                return "Departamento actualizado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el departamento: " + ex.Message;
            }
        }
        public string Eliminar(int codigoDepartamento)
        {
            try
            {
                var dep = DBInmobiliaria.DEPARTAMENTOes.FirstOrDefault(d => d.Codigo_Departamento == codigoDepartamento);
                if (dep == null)
                {
                    return "Departamento no encontrado";
                }
                DBInmobiliaria.DEPARTAMENTOes.Remove(dep);
                DBInmobiliaria.SaveChanges();
                return "Departamento eliminado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el departamento: " + ex.Message;
            }
        }
    }
}