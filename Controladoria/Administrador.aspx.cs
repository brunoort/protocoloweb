using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices.ActiveDirectory;

namespace ProtocoloWeb
{

    public partial class Administrador : System.Web.UI.Page
    {
        private string cnString = ConfigurationManager.ConnectionStrings["ProtocoloWebConnectionString"].ConnectionString;

        protected override void AddedControl(Control control, int index)
        {
            // This is necessary because Safari and Chrome browsers don't display the Menu control correctly. 
            // Add this to the code in your master page. 
            if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
                this.Page.ClientTarget = "uplevel";

            base.AddedControl(control, index);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
            {
                Request.Browser.Adapters.Clear();
            }

            if (Session["User"] != null)
            {
                lblLogin.Text = Session["User"].ToString();

                var def = new _Default();
                def.verificaUsuario();
            }
        }

        private void carregaUsuarios()
        {

            string strSQL = String.Empty;
            strSQL = "SELECT * FROM Usuarios ORDER BY idUsuario DESC";
            SqlConnection sqlConnection = new SqlConnection(cnString);
            SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            sqlConnection.Close();
        }

        protected bool ConsultaUsuario(string usuario)
        {
            string nome = String.Empty;
            DirectoryEntry Acesso = AcessoAD();
            DirectorySearcher pesquisa = new DirectorySearcher();
            pesquisa.SearchRoot = Acesso;
            pesquisa.SearchScope = SearchScope.Subtree;
            pesquisa.Filter = "(ObjectClass=user)";
            pesquisa.Filter = "(&(objectClass=user)(samaccountname=" + usuario + "))";
            foreach (SearchResult filtro in pesquisa.FindAll())
            {
                nome = filtro.Properties["cn"][0].ToString();
            }

            if (!String.IsNullOrEmpty(nome))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static DirectoryEntry AcessoAD()
        {
            DirectoryEntry de = new DirectoryEntry(ConfigurationManager.AppSettings["LDAPPath"], ConfigurationManager.AppSettings["LDAPUser"], ConfigurationManager.AppSettings["LDAPPassword"]);
            return de;
        }

        

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            string vLogin = txtNomeUsuario.Value;
            bool vLoginExiste = false;

            //Vendo se o gridview já possuí esse login cadastrado
            for (int Count = 0; Count < GridView1.Rows.Count; Count++)
            {
                if (GridView1.Rows[Count].Cells[1].Text == vLogin)
                {
                    vLoginExiste = true;
                }
            }

            if (vLogin != "" && vLoginExiste == false)
            {
                bool loginExisteAD = ConsultaUsuario(vLogin);

                if (loginExisteAD == true)
                {
                    string strSQL = "INSERT INTO Usuarios (Login) VALUES ('" + vLogin + "')";
                    SqlConnection sqlConnection = new SqlConnection(cnString);
                    SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
                    sqlConnection.Open();
                    sqlCommand.ExecuteNonQuery();

                    carregaUsuarios();

                    lblResposta.Text = "";    
                }
                else
                {
                    lblResposta.Text = "Esse usuário não existe.";
                }

                
            }
            else
            {
                lblResposta.Text = "Atenção: Esse login já está cadastrado.";
            }
        }

    }
}