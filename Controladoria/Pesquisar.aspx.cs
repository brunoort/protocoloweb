using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProtocoloWeb
{
    public partial class Pesquisar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                lblLogin.Text = Session["User"].ToString();

                var def = new _Default();
                def.verificaUsuario();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        public class Dados
        {
            public String Empresa { get; set; }
            public String Matriz { get; set; }
            public int Nf { get; set; }
            public int Ap { get; set; }
            public string dtVencimento { get; set; }
            public double Valor { get; set; }
            public string dtEmissaoDOC { get; set; }
            public int codJustificativa { get; set; }
            public int Departamento { get; set; }
            public String tipodocumento { get; set; }
            public String documento { get; set; }
        }

        public void btnBuscar_Click(object sender, EventArgs e)
        {
            string vInicial = txtDataInicial.Value;
            string vFinal = txtDataFinal.Value;
            string vNf = txtNF.Text;
            string vAp = txtAP.Text;
            string vDocumento = sltTipoDocumento.Value;
            string vCNPJ = txtCNPJ.Value;
            bool iniciarBusca = true;
            string vErro = "";

            if (vFinal != "" && vInicial == "")
            {
                vErro = "Informe uma data inicial para pesquisar.";
                iniciarBusca = false;
            }
            else
            {
                iniciarBusca = true;
            }

            if (iniciarBusca == true)
            {
                //Encaminhar dados para exibeResultado

                Session["vInicial"] = txtDataInicial.Value;
                Session["vFinal"] = txtDataFinal.Value;
                Session["vNf"] = txtNF.Text;
                Session["vAp"] = txtAP.Text;
                Session["vStatus"] = txtStatus.Value;
                Session["vDocumento"] = sltTipoDocumento.Value;
                Session["vCNPJ"] = txtCNPJ.Value;

                dados.InnerHtml = "<script>redireciona();</script>";
            }
            else
            {
                lblResposta.Text = vErro;
            }
        }

    }

}