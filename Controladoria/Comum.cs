using System;
using System.Configuration;

namespace Controladoria
{
    public class Comum
    {
        public static string BuscarChaveWebConfig(string keyName)
        {
            if (ConfigurationManager.AppSettings[keyName] != null)
                return ConfigurationManager.AppSettings[keyName];

            throw new ArgumentException("A chave com o nome:  " + keyName + " nao foi encontrada.", keyName);
        }

        public static string GeraToken(string vAplicativo)
        {
            string[] hora = DateTime.Now.ToLongTimeString().Split(':');
            string token = vAplicativo + DateTime.Now.ToString("ddMMyyyy") + hora[0] + hora[1] + hora[2];
            return token;
        }
    }
}