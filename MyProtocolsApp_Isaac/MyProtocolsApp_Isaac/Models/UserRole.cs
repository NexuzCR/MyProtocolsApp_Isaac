using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyProtocolsApp_Isaac.Models
{
    public class UserRole
    {
        public RestRequest Request { get; set; }   

        
        public int UserRoleId { get; set; }
        public string Description { get; set; } = null!;


        //funciones
        public async Task<List<UserRole>> GetAllUserRolesAsync()
        {
            try
            {
              
                string RouteSufix = string.Format("UserRoles");
                //armamos la ruta completa al endpoint en el api

                string URL = Services.ApiConection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                //agregamos el mecanismo de seguridad, en este caso api key 
                Request.AddHeader(Services.ApiConection.ApiKeyName, Services.ApiConection.ApiKeyValue);

                //ejecutamos la llamada al api
                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien
                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    var list = JsonConvert.DeserializeObject<List<UserRole>>(response.Content);
                    return list;
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

    }
}
