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

        public List<VISITA> ConsultarTodos()
        {
            return DBInmobiliaria.VISITAs.ToList();
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