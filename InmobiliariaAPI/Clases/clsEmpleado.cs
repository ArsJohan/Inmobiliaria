using InmobiliariaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace InmobiliariaAPI.Clases
{
    public class clsEmpleado
    {
        private DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public EMPLEADO empleado { get; set; }
        public string Insertar()
        {
            try
            {
                DBInmobiliaria.EMPLEADOes.Add(empleado); //Agrega un empleado a la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                DBInmobiliaria.SaveChanges(); //Guarda los cambios en la base de datos
                return "Empleado insertado correctamente"; //Retorna un mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al insertar el Empleado: " + ex.Message; //Retorna un mensaje de error
            }
        }
        public EMPLEADO ConsultarXDocumento(string documento)
        {
            //Expresiones lambda se convierte en objetos del tipo que se esta consultando
            //El método FirstOrDefault retorna el primer objeto que cumpla con la condición que se escribe en la consulta
            return DBInmobiliaria.EMPLEADOes.FirstOrDefault(e => e.Nro_Documento == documento);
        }
        public List<EMPLEADO> ConsultarTodo()
        {
            return DBInmobiliaria.EMPLEADOes
                .ToList(); //Retorna una lista de empleados
        }
        public string Actualizar()
        {
            try
            {
                //Antes de actualizar, se debería consultar si el dato ya existe para poder actualizarlo, de lo contrario se debería insertar o retornar un mensaje de error
                //Consultar el Empleado
                EMPLEADO eMPLEADO = ConsultarXDocumento(empleado.Nro_Documento);
                if (eMPLEADO == null)
                {
                    return "Empleado no existe"; //Retorna un mensaje de error
                }
                DBInmobiliaria.EMPLEADOes.AddOrUpdate(empleado); //Actualiza el empleado en la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                DBInmobiliaria.SaveChanges(); //Guarda los cambios en la base de datos
                return "Empleado actualizado correctamente"; //Retorna un mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al actualizar el Empleado: " + ex.Message; //Retorna un mensaje de error
            }
        }
        public string Eliminar(string documento)
        {
            try
            {
                //Antes de actualizar, se debería consultar si el dato ya existe para poder actualizarlo, de lo contrario se debería insertar o retornar un mensaje de error
                //Consultar el Empleado
                EMPLEADO eMPLEADO = ConsultarXDocumento(documento);
                if (eMPLEADO == null)
                {
                    return "Empleado no existe"; //Retorna un mensaje de error
                }
                DBInmobiliaria.EMPLEADOes.Remove(eMPLEADO); //Actualiza el empleado en la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                DBInmobiliaria.SaveChanges(); //Guarda los cambios en la base de datos
                return "Empleado Eliminar correctamente"; //Retorna un mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al Eliminar el Empleado: " + ex.Message; //Retorna un mensaje de error
            }
        }
    }
}