using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InmobiliariaAPI.Dtos
{
	public class DtoLogin
	{
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string PaginaSolicitud { get; set; }
    }

    public class DtoLoginResponse
    {
        public string Usuario { get; set; }
        public string Perfil { get; set; }
        public string PaginaInicio { get; set; }
        public bool Autenticado { get; set; }
        public string Token { get; set; }
        public string Mensaje { get; set; }
    }
}