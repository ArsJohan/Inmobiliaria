using InmobiliariaAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Security.Principal;

namespace InmobiliariaAPI.Clases
{
    public class clsImagenInmueble
    {
       
        
        private DBINMOBILIARIAEntities DBInmobiliaria = new DBINMOBILIARIAEntities();
        private readonly string _baseUrl = "http://localhost:53901/wwwroot/uploads/images/";

        // Obtener solo las imágenes principales de cada inmueble
        public List<object> ConsultarTodos()
        {
            return DBInmobiliaria.IMAGEN_INMUEBLE
                .Where(img => img.Es_Principal == true)
                .Select(img => new
                {
                    id = img.Codigo_Imagen,
                    url = _baseUrl + img.Url_Imagen,
                    idInmueble = img.Codigo_Inmueble
                })
                .ToList<object>();
        }

        // Consultar una imagen por su id
        public object ConsultarPorId(int id)
        {
            var img = DBInmobiliaria.IMAGEN_INMUEBLE
                .Where(i => i.Codigo_Imagen == id)
                .Select(i => new
                {
                    id = i.Codigo_Imagen,
                    url = _baseUrl + i.Url_Imagen,
                    idInmueble = i.Codigo_Inmueble
                })
                .FirstOrDefault();

            return img;
        }

        public async Task<HttpResponseMessage> GrabarArchivo(HttpRequestMessage request, int codigoInmueble, bool esPrincipal)
        {
            if (!request.Content.IsMimeMultipartContent())
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se envió un archivo para procesar");
            }

            string root = HttpContext.Current.Server.MapPath("~/wwwroot/uploads/images");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await request.Content.ReadAsMultipartAsync(provider);

                if (provider.FileData.Count == 0)
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, "No se envió un archivo para procesar");
                foreach (var file in provider.FileData)
                {
                    string fileName = file.Headers.ContentDisposition.FileName?.Trim('"') ?? Path.GetFileName(file.LocalFileName);
                    if (fileName.Contains(@"/") || fileName.Contains(@"\\")) fileName = Path.GetFileName(fileName);

                    string fullPath = Path.Combine(root, fileName);

                    if (File.Exists(fullPath))
                        File.Delete(fullPath);

                    File.Move(file.LocalFileName, fullPath);

                    // Si la imagen será principal, desmarcar las demás
                    var imagenesInmueble = DBInmobiliaria.IMAGEN_INMUEBLE
                        .Where(i => i.Codigo_Inmueble == codigoInmueble && i.Es_Principal == true)
                        .ToList();

                    foreach (var img in imagenesInmueble)
                        img.Es_Principal = false;

                    if (esPrincipal)
                    {
                        // Desmarcar las demás imágenes principales
                        var imagenesPrincipales = DBInmobiliaria.IMAGEN_INMUEBLE
                            .Where(i => i.Codigo_Inmueble == codigoInmueble && i.Es_Principal == true)
                            .ToList();

                        foreach (var img in imagenesPrincipales)
                            img.Es_Principal = false;
                    }

                    // Buscar si ya existe el registro en la base de datos
                    var imagenExistente = DBInmobiliaria.IMAGEN_INMUEBLE
                        .FirstOrDefault(i => i.Url_Imagen == fileName && i.Codigo_Inmueble == codigoInmueble);

                    if (imagenExistente == null)
                    {
                        var imagen = new IMAGEN_INMUEBLE
                        {
                            Codigo_Inmueble = codigoInmueble,
                            Url_Imagen = fileName,
                            Es_Principal = esPrincipal,
                            Fecha_Subida = DateTime.Now
                        };
                        DBInmobiliaria.IMAGEN_INMUEBLE.Add(imagen);
                    }
                    else
                    {
                        imagenExistente.Fecha_Subida = DateTime.Now;
                        imagenExistente.Es_Principal = esPrincipal;
                    }
                }

                DBInmobiliaria.SaveChanges();
                return request.CreateResponse(HttpStatusCode.OK, "Se cargaron las imágenes correctamente.");
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        public HttpResponseMessage EliminarArchivo(HttpRequestMessage request, string archivo, int codigoInmueble)
        {
            try
            {
                string root = HttpContext.Current.Server.MapPath("~/wwwroot/uploads/images");
                string archivoCompleto = Path.Combine(root, archivo);

                if (File.Exists(archivoCompleto))
                {
                    File.Delete(archivoCompleto);

                    // Eliminar referencia en la base de datos
                    var imagen = DBInmobiliaria.IMAGEN_INMUEBLE
                        .FirstOrDefault(i => i.Url_Imagen == archivo && i.Codigo_Inmueble == codigoInmueble);

                    if (imagen != null)
                    {
                        DBInmobiliaria.IMAGEN_INMUEBLE.Remove(imagen);
                        DBInmobiliaria.SaveChanges();
                    }

                    return request.CreateResponse(HttpStatusCode.OK, "Se eliminó el archivo del servidor y la referencia en la base de datos.");
                }
                else
                {
                    return request.CreateErrorResponse(HttpStatusCode.NotFound, "El archivo no se encuentra en el servidor");
                }
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.InternalServerError, "No se pudo eliminar el archivo. " + ex.Message);
            }
        }
        
    }
}
