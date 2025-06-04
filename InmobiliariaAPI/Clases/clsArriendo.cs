using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using InmobiliariaAPI.Models;

namespace InmobiliariaAPI.Clases
{
    public class clsArriendo
    {

        private DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public ARRIENDO arriendo { get; set; }

        public string Insertar()
        {
            try
            {
                DBInmobiliaria.ARRIENDOes.Add(arriendo);
                DBInmobiliaria.SaveChanges();
                return "Arriendo registrado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al registrar el arriendo: " + ex.Message;
            }
        }
        public List<ARRIENDO> ConsultarTodo()
        {
            return DBInmobiliaria.ARRIENDOes
                .ToList(); //Retorna una lista de empleados
        }
        public ARRIENDO ConsultarXCodigo(int codigo)
        {
            ARRIENDO arri = DBInmobiliaria.ARRIENDOes.FirstOrDefault(a => a.Codigo_Transaccion == codigo);
            return arri;
        }

        public string Actualizar()
        {
            try
            {
                ARRIENDO arri = ConsultarXCodigo(arriendo.Codigo_Transaccion);
                if (arri == null) 
                {
                    return "Arriendo no encontrado";
                }
                DBInmobiliaria.ARRIENDOes.AddOrUpdate(arriendo);
                DBInmobiliaria.SaveChanges();
                return "Arriendo con codigo" + arriendo.Codigo_Transaccion + " actualizado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al actualizar el arriendo: " + ex.Message;
            }
        }

        public string Eliminar(int codigo)
        {
            try
            {
                ARRIENDO arri = ConsultarXCodigo(codigo);
                if (arri == null)
                {
                    return "Arriendo no encontrado";
                }
                DBInmobiliaria.ARRIENDOes.Remove(arriendo);
                DBInmobiliaria.SaveChanges();
                return "Arriendo con codigo " + codigo + " eliminado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el arriendo: " + ex.Message;
            }
        }
    }
}