using InmobiliariaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml.Linq;

namespace InmobiliariaAPI.Clases
{
    public class clsEmpleado
    {
        private DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public EMPLEADO empleado { get; set; }

        public EMPLEADO ConsultarPorUsuario(string username)
        {
            try
            {
                //Consulta el usuario por su nombre de usuario
                USUARIO usuario = DBInmobiliaria.USUARIOs.FirstOrDefault(u => u.Username == username);
                if (usuario == null)
                {
                    return null; //Retorna un mensaje de error si no se encuentra el usuario
                }
                //Consulta el empleado por su documento
                empleado = DBInmobiliaria.EMPLEADOes.FirstOrDefault(e => e.Codigo_Empleado == usuario.Documento_Empleado);
                if (empleado == null)
                {
                    return null; //Retorna un mensaje de error si no se encuentra el empleado
                }
                return empleado; //Retorna un mensaje de confirmación si se encuentra el empleado
            }
            catch 
            {
                return null; //Retorna un mensaje de error
            }
        }
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
        public IQueryable ConsultarTodo()
        {
            return DBInmobiliaria.Set<EMPLEADO>()
                .OrderBy(E => E.Apellido)
                .ThenBy(E => E.Nombre)
                .AsEnumerable() // 👈 Trae datos a memoria
                .Select(E => new
                {
                    Editar = "<img src =\"../Imagenes/Editar.png\" onclick=\"Editar() \"style=\"cursor:grab\"/>",
                    Codigo =  E.Codigo_Empleado,
                    E.Tipo_Doc,
                    E.Nro_Documento,
                    E.Activo,
                    Sede = E.Codigo_Sede,
                    E.Nombre,
                    E.Apellido,
                    Genero=E.Codigo_Genero,
                    E.Email,
                    E.Tipo_Telefono,
                    E.Telefono,
                    E.Fecha_Contratacion,
                    TipoEmpleado= E.Codigo_TipoEmpleado,
                    
                    
                    
                    
                })
                .AsQueryable(); // Si necesitas devolver IQueryable
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