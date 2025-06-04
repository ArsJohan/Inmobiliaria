using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using InmobiliariaAPI.Models;

namespace InmobiliariaAPI.Clases
{
	public class clsVisita
	{
		private DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public VISITA visita { get; set; }

        public IQueryable<object> ConsultarTodos()
        {
            return from v in DBInmobiliaria.Set<VISITA>()
                   join c in DBInmobiliaria.Set<CLIENTE>()
                     on v.Codigo_Cliente equals c.Codigo_Cliente
                   select new
                   {
                       Eliminar = "<img src =\"../Imagenes/Eliminar.png\" onclick=\"Eliminar() \"style=\"cursor:grab\"/>",
                       v.Codigo_Cliente,
                       Nro_Documento = c.Nro_Documento,
                       Codigo_visita = v.Codigo_Visita,
                       v.Codigo_Inmueble,
                       Nombre = c.Nombre,
                       Apellido = c.Apellido,
                       Direccion = c.Direccion,
                       TipoTelefono = c.Tipo_Telefono,
                       Telefono = c.Telefono,
                       v.Fecha_Visita,
                       Email = c.Email,
                       v.Comentarios
                   };
        }

        public List<VISITA> ConsultarPorInmueble(int codigoInmueble)
        {
            return DBInmobiliaria.VISITAs.Where(v => v.Codigo_Inmueble == codigoInmueble).ToList();
        }

        public IQueryable<DateTime> FechasDisponibles(int codigoInmueble)
        {
            var visitas = DBInmobiliaria.VISITAs
                .Where(v => v.Codigo_Inmueble == codigoInmueble)
                .Select(v => v.Fecha_Visita)
                .ToList();
            var diasDisponibles = new List<DateTime>();
            DateTime fechaActual = DateTime.Now.Date;
            for (int i = 0; i < 30; i++)
            {
                DateTime dia = fechaActual.AddDays(i);
                if (!visitas.Any(v => v.Date == dia))
                {
                    diasDisponibles.Add(dia);
                }
            }
            return diasDisponibles.AsQueryable();
        }

        public string Insertar()
        {
            try
            {
                //Validar que la fecha de visita sea mayor o igual a la fecha actual
                if (visita.Fecha_Visita.Date < DateTime.Now.Date)
                {
                    return "La fecha de visita es incorrecta";
                }

                //Validar que visita no exita en la misma fecha
                var visitaExistente = DBInmobiliaria.VISITAs
                    .FirstOrDefault(v => v.Codigo_Inmueble == visita.Codigo_Inmueble && v.Fecha_Visita.Date == visita.Fecha_Visita.Date);
                if (visitaExistente != null) { return "La fecha de la visita no se encuentra disponible"; }
                visita.Comentarios = "Visita registrada correctamente";
                    DBInmobiliaria.VISITAs.Add(visita);
                DBInmobiliaria.SaveChanges();
                return "Visita registrada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al registrar la visita: " + ex.Message;
            }
        }

        public string Actualizar()
        {
            try
            {
                DBInmobiliaria.VISITAs.AddOrUpdate(visita);
                DBInmobiliaria.SaveChanges();
                return "Visita actualizada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al actualizar la visita: " + ex.Message;
            }
        }


        public string Eliminar(int codigoVisita)
        {
            try
            {
                var visitaExistente = DBInmobiliaria.VISITAs.FirstOrDefault(v => v.Codigo_Visita == codigoVisita);
                if (visitaExistente == null)
                {
                    return "Visita no encontrada";
                }
                DBInmobiliaria.VISITAs.Remove(visitaExistente);
                DBInmobiliaria.SaveChanges();
                return "Visita eliminada correctamente";
            }
            catch (Exception ex)
            {
                return "Error al eliminar la visita: " + ex.Message;
            }
        }
    }
}