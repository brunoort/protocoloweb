using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProtocoloWeb
{
    public class VerificaUsuario
    {
        public IDictionary<string, string> InfoUser()
        {
            string logado = Environment.UserName;
            IDictionary<string, string> userData;
            Usuario usr = new Usuario();
            userData = usr.verificaUsuario(logado);

            return userData;
        }

        public bool verificar()
        {
            try
            {
                string logado = Environment.UserName;
                IDictionary<string, string> userData;
                Usuario usr = new Usuario();
                userData = usr.verificaUsuario(logado);
                if (userData["Status"].ToString() == "200")
                {
                    //Verificando se o usuário é do Grupo Autorizado
                    if (userData["Departamento"] == "Tecnologia da Informação")
                    {
                        
                        //Session["userIngest"] = userData["Grupo"].ToString();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}