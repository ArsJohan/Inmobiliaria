using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InmobiliariaAPI.Models;
using System.Data.Entity.Migrations;

namespace InmobiliariaAPI.Clases
{
    public class clsCliente
    {
        private DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public CLIENTE cliente { get; set; }
        public string Insertar()
        {
            try
            {
                DBInmobiliaria.CLIENTEs.Add(cliente); //Agrega un cliente a la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                DBInmobiliaria.SaveChanges(); //Guarda los cambios en la base de datos
                return "Cliente insertado correctamente"; //Retorna un mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al insertar el Cliente: " + ex.Message; //Retorna un mensaje de error
            }
        }
        public CLIENTE ConsultarXDocumento(string documento)
        {
            //Expresiones lambda se convierte en objetos del tipo que se esta consultando
            //El método FirstOrDefault retorna el primer objeto que cumpla con la condición que se escribe en la consulta
            return DBInmobiliaria.CLIENTEs.FirstOrDefault(c => c.Nro_Documento == documento);
        }
        public List<CLIENTE> ConsultarTodo()
        {
            return DBInmobiliaria.CLIENTEs
                .ToList(); //Retorna una lista de cliente
        }
        public string Actualizar()
        {
            try
            {
                //Antes de actualizar, se debería consultar si el dato ya existe para poder actualizarlo, de lo contrario se debería insertar o retornar un mensaje de error
                //Consultar el Cliente
                CLIENTE cLIENTE = ConsultarXDocumento(cliente.Nro_Documento);
                if (cLIENTE == null)
                {
                    return "Cliente no existe"; //Retorna un mensaje de error
                }
                DBInmobiliaria.CLIENTEs.AddOrUpdate(cliente); //Actualiza el cliente en la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                DBInmobiliaria.SaveChanges(); //Guarda los cambios en la base de datos
                return "Clinte actualizado correctamente"; //Retorna un mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al actualizar el Cliente: " + ex.Message; //Retorna un mensaje de error
            }
        }
        public string Eliminar(string documento)
        {
            try
            {
                CLIENTE cLIENTE = ConsultarXDocumento(documento);
                if (cLIENTE == null)
                {
                    return "Cliente no existe"; //Retorna un mensaje de error
                }
                DBInmobiliaria.CLIENTEs.Remove(cLIENTE); //Elimina el cliente en la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                DBInmobiliaria.SaveChanges(); //Guarda los cambios en la base de datos
                return "Clinte Eliminado correctamente"; //Retorna un mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al Eliminar el Cliente: " + ex.Message; //Retorna un mensaje de error
            }

        }
    }
}