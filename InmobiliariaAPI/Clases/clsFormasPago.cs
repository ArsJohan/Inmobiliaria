using InmobiliariaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace InmobiliariaAPI.Clases
{
    public class clsFormasPago
    {
        private DBINMOBILIARIAEntities db = new DBINMOBILIARIAEntities();
        public TIPO_PAGO tipoPago { get; set; }

        // Consultar todas las formas de pago
        public List<TIPO_PAGO> ConsultarTodos()
        {
            return db.TIPO_PAGO.ToList();
        }

        // Consultar una forma de pago por ID
        public TIPO_PAGO ConsultarPorId(int id)
        {
            return db.TIPO_PAGO.FirstOrDefault(t => t.Codigo_TipoPago == id);
        }

        // Insertar una nueva forma de pago
        public string Insertar()
        {
            try
            {
                db.TIPO_PAGO.Add(tipoPago);
                db.SaveChanges();
                return "Forma de pago insertada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al insertar la forma de pago: " + ex.Message;
            }
        }

        // Actualizar una forma de pago existente
        public string Actualizar()
        {
            try
            {
                var existente = db.TIPO_PAGO.FirstOrDefault(t => t.Codigo_TipoPago == tipoPago.Codigo_TipoPago);
                if (existente == null)
                    return "La forma de pago no existe";

                db.TIPO_PAGO.AddOrUpdate(tipoPago);
                db.SaveChanges();
                return "Forma de pago actualizada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al actualizar la forma de pago: " + ex.Message;
            }
        }

        // Eliminar una forma de pago por ID
        public string Eliminar(int id)
        {
            try
            {
                var existente = db.TIPO_PAGO.FirstOrDefault(t => t.Codigo_TipoPago == id);
                if (existente == null)
                    return "La forma de pago no existe";

                db.TIPO_PAGO.Remove(existente);
                db.SaveChanges();
                return "Forma de pago eliminada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar la forma de pago: " + ex.Message;
            }
        }
    }
}
