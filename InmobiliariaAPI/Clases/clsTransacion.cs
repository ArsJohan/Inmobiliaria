using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using InmobiliariaAPI.Models;

namespace InmobiliariaAPI.Clases
{
    public class clsTransacion
    {
        private DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public TRANSACCION transacion { get; set; }

        public string Insertar()
        {
            try
            {
                DBInmobiliaria.TRANSACCIONs.Add(transacion);
                DBInmobiliaria.SaveChanges();
                return "Transacción registrada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al registrar la transacción: " + ex.Message;
            }
        }

        public TRANSACCION ConsultarXCodigo (int codigo)
        {
            TRANSACCION tran = DBInmobiliaria.TRANSACCIONs.FirstOrDefault(t => t.Codigo_Transaccion == codigo);
            return tran;
        }
        public List<TRANSACCION> ConsultarTodo()
        {
            return DBInmobiliaria.TRANSACCIONs
                .ToList(); //Retorna una lista de empleados
        }
        
        public string Actualizar()
        {
            try
            {
                TRANSACCION tran = ConsultarXCodigo(transacion.Codigo_Transaccion);
                if (tran == null)
                {
                    return "Transacción no encontrada";
                }
                DBInmobiliaria.TRANSACCIONs.AddOrUpdate(transacion);
                DBInmobiliaria.SaveChanges();
                return "Transacción con código " + transacion.Codigo_Transaccion + " actualizada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al actualizar la transacción: " + ex.Message;
            }
        }
        public string Eliminar (int codigo)
        {
            try
            {
                TRANSACCION tran = ConsultarXCodigo(codigo);
                if (tran == null)
                {
                    return "Transacción no encontrada";
                }
                DBInmobiliaria.TRANSACCIONs.Remove(tran);
                DBInmobiliaria.SaveChanges();
                return "Transacción con código " + codigo + " eliminada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar la transacción: " + ex.Message;
            }
        }
    }
}