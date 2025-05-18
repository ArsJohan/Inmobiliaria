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
        private DBINMOBILIARIAEntities DBinmovi = new DBINMOBILIARIAEntities();
        public TRANSACCION transacion { get; set; }

        public string Insertar()
        {
            try
            {
                DBinmovi.TRANSACCIONs.Add(transacion);
                DBinmovi.SaveChanges();
                return "Transacción registrada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al registrar la transacción: " + ex.Message;
            }
        }

        public TRANSACCION ConsultarXCodigo (int codigo)
        {
            TRANSACCION tran = DBinmovi.TRANSACCIONs.FirstOrDefault(t => t.Codigo_Transaccion == codigo);
            return tran;
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
                DBinmovi.TRANSACCIONs.AddOrUpdate(transacion);
                DBinmovi.SaveChanges();
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
                DBinmovi.TRANSACCIONs.Remove(tran);
                DBinmovi.SaveChanges();
                return "Transacción con código " + codigo + " eliminada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar la transacción: " + ex.Message;
            }
        }
    }
}