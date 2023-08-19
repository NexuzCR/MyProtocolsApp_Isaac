using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace MyProtocolsApp_Isaac.Models
{
    public class User
    {
        //es mala idea tener un solo objeto de comunicacion restRequest contra el api
        //recomiendo tener una por cada clase que se comunique con el api
        public RestRequest Request { get; set; }

        //en este ejemplo usaremos los mismos atrinbutos que en el modelo del api
        //posteriormente en otra clase usare el DTO del usuario para simplificar
        //rl json que se envia y recibe desde el api 
        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string BackUpEmail { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Address { get; set; }
        public bool? Active { get; set; }
        public bool? IsBlocked { get; set; }
        public int UserRoleId { get; set; }

        public virtual UserRole? UserRole { get; set; } = null!;



        public User()
        {

        }
        //funciones especificas de llamadas a end point del api

        //funcion que permite validar que los datos digitados en la pagina de 
        //app login sea correcto o no

        public async Task<bool> ValidateUserLogin()
        {
            try
            {
//usaremos el prefijo de la ruta URL del api que se indica en 
//service\apiconnection apatra agregar el sufujo y lograr la ruta
//completa de consumo del end point que se quiere usar



             
                string RouteSufix = string.Format("Users/ValidateLogin?username={0}&password={1}", this.Email, this.Password);
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

        public async Task<bool> AddUserAsync()
        {
            try
            {
              
                string RouteSufix = string.Format("Users");
                //armamos la ruta completa al endpoint en el api

                string URL = Services.ApiConection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Post);

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
