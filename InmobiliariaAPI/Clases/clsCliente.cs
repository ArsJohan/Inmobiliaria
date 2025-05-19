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
        private DBINMOBILIARIAEntities DBinmovi = new DBINMOBILIARIAEntities();
        public CLIENTE cliente { get; set; }
        public string Insertar()
        {
            try
            {
                DBinmovi.CLIENTEs.Add(cliente); //Agrega un cliente a la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                DBinmovi.SaveChanges(); //Guarda los cambios en la base de datos
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
            return DBinmovi.CLIENTEs.FirstOrDefault(c => c.Nro_Documento == documento);
        }
        public List<CLIENTE> ConsultarTodo()
        {
            return DBinmovi.CLIENTEs
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
                DBinmovi.CLIENTEs.AddOrUpdate(cliente); //Actualiza el cliente en la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                DBinmovi.SaveChanges(); //Guarda los cambios en la base de datos
                return "Clinte actualizado correctamente"; //Retorna un mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al actualizar el Cliente: " + ex.Message; //Retorna un mensaje de error
            }
        }
        public string Eliminar()
        {
            try
            {
                CLIENTE cLIENTE = ConsultarXDocumento(cliente.Nro_Documento);
                if (cLIENTE == null)
                {
                    return "Cliente no existe"; //Retorna un mensaje de error
                }
                DBinmovi.CLIENTEs.Remove(cliente); //Elimina el cliente en la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                DBinmovi.SaveChanges(); //Guarda los cambios en la base de datos
                return "Clinte actualizado correctamente"; //Retorna un mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al Eliminar el Cliente: " + ex.Message; //Retorna un mensaje de error
            }

        }
    }
}