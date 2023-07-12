using System;
using System.Collections.Generic;
using System.Text;

namespace MyProtocolsApp_Isaac.Services
{
    public static class ApiConection
    {

        //ACA DEFINIMOS LA DIRECCION URL 9ya sea una ip O UN NOMBRE DE DOMINIO) A LA QUE EL APP
        //DEBE COMPRAR.
        //POR COMODIDAD LA RUTA COMPLETA PARA CONSUMIR LOS RECURSOS DEL API SE HARA EN FORMARO
        //[PREFIJO MAS SUFIJO
        //DONDE EL PREFIJO SERA LA PARATE DE LA URL DONDE NUNCA CAMBIARA Y EL SUFIJO SERA LA PARTE VARIABLE
        //(NOMBRE DEL CONTROLADOR Y SUS PARAMETROS)


        public static string ProductionPrefixURL = "http://192.168.1.125:45455/api/";
        public static string TestingPrefixURL = "http://192.168.1.125:45455/api/";

        public static string ApiKeyName = "PracticaApiKey";
        public static string ApiValue = "IsaacProgra6Asdzxc12345";

    }
}
