using InmobiliariaAPI.Clases;
using InmobiliariaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InmobiliariaAPI.Controllers
{
    [RoutePrefix("api/Usuarios")]
    [AllowAnonymous]
    public class UsuariosController : ApiController
    {
        [HttpPost]
        [Route("CrearUsuarios")]
        
        public string CrearUsuarios([FromBody] USUARIO usuario, int idPerfil)
        {
            clsUsuario _usuario = new clsUsuario();
            _usuario.usuario = usuario;
            return _usuario.CrearUsuario(idPerfil);
        }
    }
}