using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProtocoloWeb
{
    public partial class Justificativas : System.Web.UI.Page
    {
        string cnString = ConfigurationManager.ConnectionStrings["ProtocoloWebConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                var def = new _Default();
                def.verificaUsuario();
            }

            int idFormulario = Convert.ToInt32(Request.QueryString["idform"]);
            lblIdFormulario.Text = idFormulario.ToString();

            string leitura = Request.QueryString["read"];
            if (leitura == "Y")
            {
                divSalvaJustificativa.InnerHtml = "";
                carregaJustificativas(idFormulario);
            }
        }

        //Gravando dados na tabela Justificativa
        protected void salvaJustificativa(object sender, EventArgs e)
        {
            int idForm = Convert.ToInt32(lblIdFormulario.Text);
            string justificativa = txtJustificativa.Text;

            if(justificativa != "")
            {
                string strSQL = "INSERT INTO Justificativa (idFormulario,Justificativa,dtJustificativa) VALUES (" + idForm + ",'" + justificativa + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "')";
                SqlConnection sqlConnection = new SqlConnection(cnString);
                SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();

                //divJustificativa.InnerHtml = "<br/><br/><br/><br/>Dados salvos com sucesso.";
                divJustificativa.InnerHtml = "<script>fechaJanela('divJustificativa');</script>";
            }


        }

        protected void carregaJustificativas(int pIdForm)
        {
            string strSQL = "";
            strSQL = "SELECT idJustificativa,idFormulario,Justificativa,CONVERT(VARCHAR(10), dtJustificativa, 103) as dtJustificativa FROM Justificativa WHERE idFormulario = " + pIdForm + " ORDER BY idFormulario DESC";
            SqlConnection sqlConnection = new SqlConnection(cnString);
            SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if(reader.HasRows)
            {
                GridViewJustificativas.DataSource = reader;
                GridViewJustificativas.DataBind();

                GridViewJustificativas.UseAccessibleHeader = true;
                GridViewJustificativas.HeaderRow.TableSection = TableRowSection.TableHeader;
                TableCellCollection cells = GridViewJustificativas.HeaderRow.Cells;
                cells[0].Attributes.Add("data-class", "phone,tablet");
                cells[1].Attributes.Add("data-class", "phone,tablet");
                
            }
        }
    }
}