using InmobiliariaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace InmobiliariaAPI.Clases
{
    public class clsVenta
    {
        private DBINMOBILIARIAEntities DBinmovi = new DBINMOBILIARIAEntities();
        public VENTA venta { get; set; }
        public string Insertar()
        {
            try
            {
                DBinmovi.VENTAs.Add(venta); //Agrega un venta a la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                DBinmovi.SaveChanges(); //Guarda los cambios en la base de datos
                return "venta insertado correctamente"; //Retorna un mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al insertar el venta: " + ex.Message; //Retorna un mensaje de error
            }
        }
        public VENTA Consultar(int codigo)
        {
            //Expresiones lambda se convierte en objetos del tipo que se esta consultando
            //El método FirstOrDefault retorna el primer objeto que cumpla con la condición que se escribe en la consulta
            return DBinmovi.VENTAs.FirstOrDefault(v => v.Codigo_Transaccion == codigo);
        }
        public List<VENTA> ConsultarTodo()
        {
            return DBinmovi.VENTAs
                .ToList(); //Retorna una lista de venta
        }
        public string Actualizar()
        {
            try
            {
                //Antes de actualizar, se debería consultar si el dato ya existe para poder actualizarlo, de lo contrario se debería insertar o retornar un mensaje de error
                //Consultar el venta
                VENTA vENTA = Consultar(venta.Codigo_Transaccion);
                if (vENTA == null)
                {
                    return "venta no existe"; //Retorna un mensaje de error
                }
                DBinmovi.VENTAs.AddOrUpdate(venta); //Actualiza el venta en la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                DBinmovi.SaveChanges(); //Guarda los cambios en la base de datos
                return "venta actualizado correctamente"; //Retorna un mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al actualizar el Empleado: " + ex.Message; //Retorna un mensaje de error
            }
        }
        public string Eliminar()
        {
            try
            {
                //Antes de actualizar, se debería consultar si el dato ya existe para poder actualizarlo, de lo contrario se debería insertar o retornar un mensaje de error
                //Consultar el venta
                VENTA vENTA= Consultar(venta.Codigo_Transaccion);
                if (vENTA == null)
                {
                    return "venta no existe"; //Retorna un mensaje de error
                }
                DBinmovi.VENTAs.Remove(venta); //Actualiza el venta en la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                DBinmovi.SaveChanges(); //Guarda los cambios en la base de datos
                return "venta actualizado correctamente"; //Retorna un mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al actualizar el venta: " + ex.Message; //Retorna un mensaje de error
            }
        }
    }
}