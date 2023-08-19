using MyProtocolsApp_Isaac.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProtocolsApp_Isaac
{
    public static class GlobalObjects
    {

        //definimos las propiedades de codificacion de los json
        //que usaremos en los modelos


        public static string MimeType = "application/json";
        public static string ContentType = "Content-Type";

        //crear el objeto global de usuario
        public static UserDTO MyLocalUser = new UserDTO();



    }
}
