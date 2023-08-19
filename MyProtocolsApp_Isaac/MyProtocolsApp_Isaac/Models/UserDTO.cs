using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net;

namespace MyProtocolsApp_Isaac.Models
{
    public class UserDTO
    {
        [JsonIgnore]
        public RestRequest Request { get; set; }


        public int IDUsuario { get; set; }
        public string Correo { get; set; } = null!;
        public string Contrasennia { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string CorreoRespaldo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? Direccion { get; set; }
        public bool? Activo { get; set; }
        public bool? EstaBloqueado { get; set; }
        public int IDRol { get; set; }

        public string DescripcionRol { get; set; } = null;

        // funciones
        public async Task<UserDTO> GetUserInfo(string PEmail)
        {
            try
            {

                string RouteSufix = string.Format("Users/GetUserInfoByEmail?Pemail={0}", PEmail);
                //armamos la ruta completa al endpoint en el api

                string URL = Services.ApiConection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                //agregamos el mecanismo de seguridad, en este caso api key 
                Request.AddHeader(Services.ApiConection.ApiKeyName, Services.ApiConection.ApiKeyValue);

                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);
                //ejecutamos la llamada al api
                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien
                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    //en el api diseñamos el end point de forma que retorne un list<UIserDTO>
                    //peroesta funcion retorna solo un objeto de userDTO por lo tanto
                    //debemos seleccionar de la lista el item con el index 0

                    //esto puede servir para multitud de propocitos donde un api retorna uno o muchos registros
                    //pero nesecitamos solo uno de ellos

                    var list = JsonConvert.DeserializeObject<List<UserDTO>>(response.Content);

                    var item = list[0];
                    return item;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }

        }

           public async Task<bool> UPdateUserAsync()
        {
            try
            {
              
                string RouteSufix = string.Format("Users/{0}", this.IDUsuario);
                //armamos la ruta completa al endpoint en el api

                string URL = Services.ApiConection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Put);

                //agregamos el mecanismo de seguridad, en este caso api key 
                Request.AddHeader(Services.ApiConection.ApiKeyName, Services.ApiConection.ApiKeyValue);

                //en el caso de una operacion post debemos serializar el objeto para pasarlo como
                //json al api

                string SerealizedModelObject = JsonConvert.SerializeObject(this);
                //Agregamos el objeto serializado en el cuerpo del request
                Request.AddBody(SerealizedModelObject, GlobalObjects.MimeType);


                //ejecutamos la llamada al api
                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien
                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.Created)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw;
            }
        }
       
            
    }
}
