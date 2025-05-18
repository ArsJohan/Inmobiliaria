using InmobiliariaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InmobiliariaAPI.Clases
{
	public class clsUsuario
	{
        private DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public USUARIO usuario { get; set; }
        public string CrearUsuario(int idPerfil)
        {
            try
            {
                clsCypher cypher = new clsCypher();
                cypher.Password = usuario.Clave;
                if (cypher.CifrarClave())
                {
                    //Graba el usuario
                    usuario.Clave = cypher.PasswordCifrado;
                    usuario.Salt = cypher.Salt;
                    DBInmobiliaria.USUARIOs.Add(usuario);
                    DBInmobiliaria.SaveChanges();
                    //Grabar el perfil del usuario
                    PERFIL_USUARIO usuarioPerfil = new PERFIL_USUARIO();
                    usuarioPerfil.Codigo_Perfil = idPerfil;
                    usuarioPerfil.Activo = true;
                    usuarioPerfil.Codigo_Usuario = usuario.Codigo_Usuario;
                    DBInmobiliaria.PERFIL_USUARIO.Add(usuarioPerfil);
                    DBInmobiliaria.SaveChanges();
                    return "Se creó el usuario exitosamente";
                }
                else
                {
                    return "No se pudo cifrar la clave";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
    
}