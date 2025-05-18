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

        private DBINMOBILIARIAEntities DBinmovi = new DBINMOBILIARIAEntities();
        public ARRIENDO arriendo { get; set; }

        public string Insertar()
        {
            try
            {
                DBinmovi.ARRIENDOes.Add(arriendo);
                DBinmovi.SaveChanges();
                return "Arriendo registrado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al registrar el arriendo: " + ex.Message;
            }
        }

        public ARRIENDO ConsultarXCodigo(int codigo)
        {
            ARRIENDO arri = DBinmovi.ARRIENDOes.FirstOrDefault(a => a.Codigo_Transaccion == codigo);
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
                DBinmovi.ARRIENDOes.AddOrUpdate(arriendo);
                DBinmovi.SaveChanges();
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
                DBinmovi.ARRIENDOes.Remove(arriendo);
                DBinmovi.SaveChanges();
                return "Arriendo con codigo " + codigo + " eliminado correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar el arriendo: " + ex.Message;
            }
        }
    }
}