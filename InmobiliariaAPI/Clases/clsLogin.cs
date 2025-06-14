﻿using InmobiliariaAPI.Models;
using InmobiliariaAPI.Dtos;
using InmobiliariaAPI.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace InmobiliariaAPI.Clases
{
	public class clsLogin
	{
        public clsLogin()
        {
            loginRespuesta = new DtoLoginResponse();
        }
        public DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        public DtoLogin login { get; set; }
        public DtoLoginResponse loginRespuesta { get; set; }
        private bool ValidarUsuario()
        {
            try
            {
                //Se instancia un objeto de la clase Cypher
                clsCypher cifrar = new clsCypher();
                //Se consulta el usuario, sólo con el nombre, para obtener la información básica del usuario: Salt y clave encriptada
                USUARIO usuario = DBInmobiliaria.USUARIOs.FirstOrDefault(u => u.Username == login.Usuario);
                if (usuario == null)
                {
                    //El usuario no existe, se retorna un error
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "Usuario no existe";
                    return false;
                }
                //El usuario existe, se lee la información del Salt y se traduce a un arreglo de bytes y se cifra la clave que envió el usuario
                byte[] arrBytesSalt = Convert.FromBase64String(usuario.Salt);
                //login.clave tiene la clave plana
                string ClaveCifrada = cifrar.HashPassword(login.Clave, arrBytesSalt);
                //Se obtiene la clave cifrada
                login.Clave = ClaveCifrada;
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        private bool ValidarClave()
        {
            try
            {
                //Se consulta el usuario con la clave encriptada y el usuario para validar si existe
                USUARIO usuario = DBInmobiliaria.USUARIOs.FirstOrDefault(u => u.Username == login.Usuario && u.Clave == login.Clave);
                if (usuario == null)
                {
                    //Si no existe la clave es incorrecta
                    loginRespuesta.Autenticado = false;
                    loginRespuesta.Mensaje = "La clave no coincide";
                    return false;
                }
                //La clave y el usuario son correctos
                return true;
            }
            catch (Exception ex)
            {
                loginRespuesta.Autenticado = false;
                loginRespuesta.Mensaje = ex.Message;
                return false;
            }
        }
        public IQueryable<DtoLoginResponse> Ingresar()
        {
            //Si la validación es simple, en este punto se pone el código: if (user = "admin"){ token=...;}else{error;}
            if (ValidarUsuario() && ValidarClave())
            {
                //Si el usuario y la clave son correctas, se genera el token
                string token = TokenGenerator.GenerateTokenJwt(login.Usuario);
                //Consulta la información del usuario y el perfil
                return from U in DBInmobiliaria.Set<USUARIO>()
                       join UP in DBInmobiliaria.Set<PERFIL_USUARIO>()
                       on U.Codigo_Usuario equals UP.Codigo_Usuario
                       join P in DBInmobiliaria.Set<PERFIL>()
                       on UP.Codigo_Perfil equals P.Codigo_Perfil
                       where U.Username == login.Usuario &&
                               U.Clave == login.Clave
                       select new DtoLoginResponse
                       {
                           Usuario = U.Username,
                           Autenticado = true,
                           Perfil = P.Descripcion,
                           PaginaInicio = P.PaginaNavegar,
                           Token = token,
                           Mensaje = ""
                       };
            }
            else
            {
                List<DtoLoginResponse> List = new List<DtoLoginResponse>();
                List.Add(loginRespuesta);
                return List.AsQueryable();
            }
        }
    }
}