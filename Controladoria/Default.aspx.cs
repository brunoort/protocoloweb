using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProtocoloWeb
{
    public partial class _Default : Page
    {
        public Dictionary<int, string> ListaAreas() //Lista das áreas
        {
            var listaView = new Dictionary<int, string>();
            listaView.Add(1, "Controladoria");
            listaView.Add(2, "Financeira");
            listaView.Add(3, "Comex");
            listaView.Add(4, "PceAdiantamentos");
            listaView.Add(5, "Fiscal");
            listaView.Add(6, "Fornecedor");
            listaView.Add(7, "Input");
            
            return listaView;
        } 
        

        protected void Page_Load(object sender, EventArgs e)
        {       
            if (Session["User"] != null)
            {
                lblLogin.Text = Session["User"].ToString();
                verificaUsuario();

                //Carrega Counts das áreas
                
                var listaView = ListaAreas();

                //Abrindo Conexão
                Conexao cnx = new Conexao();
                cnx.Conectar();

                string strSQL = "SELECT ";
                strSQL = strSQL + "ISNULL(( SELECT count(*) FROM Formulario WHERE statusControladoria = 'False' and concluido is NULL),0) AS cControladoria, ";
                strSQL = strSQL + "ISNULL(( SELECT count(*) FROM Formulario WHERE statusFinanceira = 'False' and concluido is NULL),0) AS cFinanceira, ";
                strSQL = strSQL + "ISNULL(( SELECT count(*) FROM Formulario WHERE statusComex = 'False' and concluido is NULL),0) AS cComex, ";
                strSQL = strSQL + "ISNULL(( SELECT count(*) FROM Formulario WHERE statusPCeAdiantamentos = 'False' and concluido is NULL),0) AS cPCeAdiantamentos, ";
                strSQL = strSQL + "ISNULL(( SELECT count(*) FROM Formulario WHERE statusFiscal = 'False' and concluido is NULL),0) AS cFiscal, ";
                strSQL = strSQL + "ISNULL(( SELECT count(*) FROM Formulario WHERE statusFornecedor = 'False' and concluido is NULL),0) AS cFornecedor, ";
                strSQL = strSQL + "ISNULL(( SELECT count(*) FROM Formulario WHERE statusInput = 'False' and concluido is NULL),0) AS cInput ";
                strSQL = strSQL + "FROM Formulario";

                SqlCommand command = new SqlCommand(strSQL, cnx.cn);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lblCountControladoria.Text = reader["cControladoria"].ToString();
                        lblCountFinanceira.Text = reader["cFinanceira"].ToString();
                        lblCountComex.Text = reader["cComex"].ToString();
                        lblCountPceAdiantamentos.Text = reader["cPCeAdiantamentos"].ToString();
                        lblCountFiscal.Text = reader["cFiscal"].ToString();
                        lblCountFornecedor.Text = reader["cFornecedor"].ToString();
                        lblCountInput.Text = reader["cInput"].ToString();
                    }
                    reader.Close();
                }

                //Autoriza o usuário a ver os painéis
                VisualizarPaineis();

                cnx.Desconectar();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }

        protected void VisualizarPaineis()
        {
            //Abrindo Conexão
            Conexao cnx = new Conexao();
            cnx.Conectar();

            string vUser = Session["User"].ToString();
            string strSQL = "SELECT * FROM Usuarios WHERE Login = '"+ vUser +"'";            
            SqlCommand command = new SqlCommand(strSQL, cnx.cn);

            Session["pAdministrador"] = "False";
            Session["pFormulario"] = "False";
            Session["pPesquisar"] = "False";
            Session["pControladoria"] = "False";
            Session["pFinanceira"] = "False";
            Session["pComex"] = "False";
            Session["pPCeAdiantamentos"] = "False";
            Session["pFiscal"] = "False";
            Session["pFornecedor"] = "False";
            Session["pInput"] = "False";
            Session["pExclusao"] = "False";

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Session["pAdministrador"] = reader["Administrador"].ToString();
                    Session["pFormulario"] = reader["Formulario"].ToString();
                    Session["pPesquisar"] = reader["Pesquisar"].ToString();
                    Session["pControladoria"] = reader["Controladoria"].ToString();
                    Session["pFinanceira"] = reader["Financeira"].ToString();
                    Session["pComex"] = reader["Comex"].ToString();
                    Session["pPCeAdiantamentos"] = reader["PCeAdiantamentos"].ToString();
                    Session["pFiscal"] = reader["Fiscal"].ToString();
                    Session["pFornecedor"] = reader["Fornecedor"].ToString();
                    Session["pInput"] = reader["Input"].ToString();
                    Session["pExclusao"] = reader["Exclusao"].ToString();
                }
                reader.Close();
            }

            cnx.Desconectar();
        }

        public bool verificaUsuario()
        {
            try
            {
                string logado = Session["User"].ToString(); ;
                IDictionary<string, string> userData;
                Usuario usr = new Usuario();
                userData = usr.verificaUsuario(logado);
                if (userData["Status"].ToString() == "200")
                {
                    Session["userIngest"] = userData["Grupo"].ToString();
                    return true;
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