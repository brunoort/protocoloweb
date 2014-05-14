using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using Controladoria;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProtocoloWeb
{
    public class Usuario
    {
        public string usuario = HttpContext.Current.User.Identity.Name;
        public string aplicacao = Aplicacao;
        public string token = GeraToken();

        public static string Aplicacao
        {
            get
            {
                return "4";
            }
        }

        public string Token
        {
            get { return GeraToken(); }
        }

        private static string GeraToken()
        {
            string[] tempo = DateTime.Now.ToLongTimeString().Split(':');
            string token = Aplicacao + DateTime.Now.ToString("ddMMyyyy") + tempo[0] + tempo[1] + tempo[2];
            return token;
        }

        public IDictionary<string, string> DicUsuario = new Dictionary<string, string>();

        public IDictionary<string, string> verificaUsuarioLogin(string login, string senha)
        {
            

            //string url = Comum.BuscarChaveWebConfig("UrlWebAPIUsuario") + usuario + "&senha=" + senha;
            string url = Comum.BuscarChaveWebConfig("UrlWebAPIUsuario") + "usuario=" + usuario + "&aplicacao=" + aplicacao + "&token=" + token + "&login=" + login + "&senha=" + senha;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {

                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    var result = JsonConvert.DeserializeObject(reader.ReadToEnd());
                    JObject userData = JObject.Parse(result.ToString());

                    DicUsuario.Add("Nome", userData["Nome"].ToString());
                    DicUsuario.Add("Departamento", userData["Departamento"].ToString());
                    DicUsuario.Add("Cargo", userData["Cargo"].ToString());
                    DicUsuario.Add("Login", userData["Login"].ToString());
                    DicUsuario.Add("Grupos", userData["Grupos"].ToString());
                    DicUsuario.Add("Email", userData["Email"].ToString());

                }
            }
            catch (WebException ex)
            {
                throw new WebException(ex.Message);
            }

            return DicUsuario;
        }

        public IDictionary<string, string> verificaUsuario(string login)
        {
            IDictionary<string, string> dic = new Dictionary<string, string>();
            string url = Comum.BuscarChaveWebConfig("UrlWebAPIUser") + "usuario=" + usuario + "&aplicacao=" + aplicacao + "&token=" + token + "&login=" + login;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {

                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    var result = JsonConvert.DeserializeObject(reader.ReadToEnd());
                    JObject userData = JObject.Parse(result.ToString());
                    if (userData["Status"].ToString() == "200")
                    {
                        dic.Add("Status", userData["Status"].ToString());
                        dic.Add("Nome", userData["Result"]["Name"].ToString());
                        dic.Add("Departamento", userData["Result"]["Deparment"].ToString());
                        dic.Add("Cargo", userData["Result"]["Role"].ToString());
                        dic.Add("User", userData["Result"]["UserName"].ToString());
                        dic.Add("Grupo", userData["Result"]["Groups"].ToString().Contains("Ingest").ToString());
                        dic.Add("Email", userData["Result"]["Mail"].ToString());
                    }
                    else
                    {
                        dic.Add("Status", userData["Status"].ToString());
                    }
                }
            }
            catch (WebException ex)
            {
                throw new WebException(ex.Message);
            }

            return dic;
        }
    }
}
