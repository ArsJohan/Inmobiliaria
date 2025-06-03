using InmobiliariaAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace InmobiliariaAPI.Clases
{
	public class clsInmueble
	{
        private DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        private readonly string _baseUrl = "http://inmobiliaria-ysja.runasp.net/wwwroot/uploads/images/";
        public INMUEBLE inmueble { get; set; }


        public string Insertar()
        {
            try
            {
                DBInmobiliaria.INMUEBLEs.Add(inmueble); //Agrega un inmueble a la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                DBInmobiliaria.SaveChanges(); //Guarda los cambios en la base de datos
                return "Inmueble insertado correctamente"; //Retorna un mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al insertar el Inmueble: " + ex.Message; //Retorna un mensaje de error
            }
        }

        public INMUEBLE ConsultarXCodigo(int codigo)
        {
            //Expresiones lambda se convierte en objetos del tipo que se esta consultando
            //El método FirstOrDefault retorna el primer objeto que cumpla con la condición que se escribe en la consulta
            return DBInmobiliaria.INMUEBLEs.FirstOrDefault(i => i.Codigo_Inmueble == codigo);
        }
        public string Actualizar() {

            try
            {
                //Antes de actualizar, se debería consultar si el dato ya existe para poder actualizarlo, de lo contrario se debería insertar o retornar un mensaje de error
                //Consultar el Inmueble
                INMUEBLE iNMUEBLE = ConsultarXCodigo(inmueble.Codigo_Inmueble);
                if (iNMUEBLE == null)
                {
                    return "Inmueble no existe"; //Retorna un mensaje de error
                }
                DBInmobiliaria.INMUEBLEs.AddOrUpdate(inmueble); //Actualiza el inmueble en la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                DBInmobiliaria.SaveChanges(); //Guarda los cambios en la base de datos
                return "Inmueble actualizado correctamente"; //Retorna un mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al actualizar el Inmueble: " + ex.Message; //Retorna un mensaje de error
            }

        }

        public IEnumerable<object> ConsultarInmuebles(string tipo = null, decimal? precioMin = null, decimal? precioMax = null)
        {
            var query = from I in DBInmobiliaria.Set<INMUEBLE>()
                        join Est in  DBInmobiliaria.Set<ESTADO>() on I.Codigo_Estado equals Est.Codigo_Estado
                        join TI in DBInmobiliaria.Set<TIPO_INMUEBLE>() on I.Codigo_TipoInmueble equals TI.Codigo_TipoInmueble
                        join C in DBInmobiliaria.Set<CIUDAD>() on I.Codigo_Ciudad equals C.Codigo_Ciudad
                        join D in DBInmobiliaria.Set<DEPARTAMENTO>() on C.Codigo_Departamento equals D.Codigo_Departamento
                        join Img in DBInmobiliaria.Set<IMAGEN_INMUEBLE>() on I.Codigo_Inmueble equals Img.Codigo_Inmueble
                        where Img.Es_Principal == true && Est.Nombre_Estado == "Disponible"
                        select new
                        {
                            Codigo = I.Codigo_Inmueble,
                            Direccion = I.Direccion,
                            Es_Nuevo = I.Es_Nuevo,
                            Estrato = I.Estrato,
                            Anio = I.Anio_Construccion,
                            Precio_Venta = I.Precio_Venta,
                            Arriendo = I.Canon_Mensual,
                            Departamento = D.Nombre,
                            Ciudad = C.Nombre_Ciudad,
                            Tipo = TI.Descripcion,
                            Estado = Est.Nombre_Estado,
                            url = _baseUrl + Img.Url_Imagen,
                        };

            if (!string.IsNullOrEmpty(tipo))
                query = query.Where(x => x.Tipo == tipo);

            if (precioMin.HasValue)
                query = query.Where(x => (x.Precio_Venta ?? x.Arriendo) >= precioMin.Value);

            if (precioMax.HasValue)
                query = query.Where(x => (x.Precio_Venta ?? x.Arriendo) <= precioMax.Value);

            return query.ToList();
        }
        public string Eliminar(int codigo)
        {
            try
            {
                INMUEBLE inmueble = ConsultarXCodigo(codigo);
                if (inmueble == null)
                {
                    return "Inmueble no existe"; //Retorna un mensaje de error
                }
                DBInmobiliaria.INMUEBLEs.Remove(inmueble); //Elimina el inmueble de la lista del entity framework, se debe invocar el metodo SaveChanges para guardar los cambios en la base de datos
                DBInmobiliaria.SaveChanges(); //Guarda los cambios en la base de datos
                return "Inmueble eliminado correctamente"; //Retorna un mensaje de confirmación
            }
            catch (Exception ex)
            {
                return "Error al eliminar el Inmueble: " + ex.Message; //Retorna un mensaje de error
            }
        }
    }
}