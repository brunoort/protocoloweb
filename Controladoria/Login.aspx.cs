using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controladoria;
using EnCryptDecrypt;

namespace ProtocoloWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected bool AutorizaUsuario(string usuario, string departamento)
        {
            //Verificando se o usuário tem permissão para acessar o sistema
            string cnString = ConfigurationManager.ConnectionStrings["ProtocoloWebConnectionString"].ConnectionString;
            string strSQL = "SELECT * FROM Usuario WHERE user = '" + usuario + "' and departamento = '" + departamento + "'";
            SqlConnection sqlConnection = new SqlConnection(cnString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
            int contador = Convert.ToInt32(sqlCommand.ExecuteScalar());

            if (contador > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["logout"] == "Y")
            {
                Session["User"] = null;
            }


        }

        protected void btnLogar_Click(object sender, EventArgs e)
        {
            try
            {
                ADServ ad = new ADServ();
                ADUsuario usuario = ad.GetUserByCredentials(txtLogin.Value, txtSenha.Value);

                if (usuario != null)
                {
                    Session["User"] = usuario.Login;
                    Session["Departamento"] = usuario.Departamento;
                    Session["Email"] = usuario.Email;

                    Response.Redirect("Default.aspx", true);
                }
                else
                {
                    liMsg.Visible = true;
                    lblMsg.Text = "Usuário ou senha inválido, verifique os dados e tente novamente!";
                }
                //CryptorEngine cripto = new CryptorEngine();
                //string userEncript = cripto.Encrypt(txtLogin.Value);
                //string senhaEncript = cripto.Encrypt(txtSenha.Value);

                //string userDecript = cripto.Decrypt(userEncript);

                //Usuario usuario = new Usuario();
                //usuario.verificaUsuarioLogin(userEncript, senhaEncript);

                //if (usuario.DicUsuario.Any())
                //{
                //    Session["User"] = usuario.DicUsuario["Login"];
                //    Session["Departamento"] = usuario.DicUsuario["Departamento"];
                //    Session["Email"] = usuario.DicUsuario["Email"];

                //    Response.Redirect("Default.aspx", false);
                //}
                //else
                //{
                //    liMsg.Visible = true;
                //    lblMsg.Text = "Usuário ou senha inválido, verifique os dados e tente novamente!";
                //}
            }
            catch (Exception ex)
            {
                liMsg.Visible = true;
                lblMsg.Text = ex.Message;
            }
        }

    }
}