using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controladoria.Entities;

namespace ProtocoloWeb
{
    public partial class Carimbo : System.Web.UI.Page
    {
        string cnString = ConfigurationManager.ConnectionStrings["ProtocoloWebConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                var def = new _Default();
                def.verificaUsuario();
            }

            int idForm = Convert.ToInt32(Request.QueryString["id"]);
            lblIdFormulario.Text = idForm.ToString();

            string vMtz = Request.QueryString["mtz"];
            
            if (vMtz == "RN")
            {
                divFunction.InnerHtml = "<script>showCarimboRJ();</script>";
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            int idForm = Convert.ToInt32(Request.QueryString["id"]);
            using (ProtocoloWebEntities context = new ProtocoloWebEntities())
            {
                var pgo = context.Formulario.FirstOrDefault(p => p.idFormulario == idForm);

                if (categoriaNFZA.Checked == true)
                {
                    pgo.ZA = true;
                }

                if (categoriaNFZS.Checked == true)
                {
                    pgo.ZS = true;
                }

                if (categoriaNFNT.Checked == true)
                {
                    pgo.NT = true;
                }

                pgo.codServico = txtCodServico.Value;
                pgo.valorDeducao = txtValorDeducao.Value;
                pgo.aliquota = txtAliquota.Value;
                pgo.tipoDeDocumento = txtTipoDocumento.Value;
                pgo.tributacaoServico = txtTributacaoServico.Value;
                pgo.codigoPrestador = txtCodPrestador.Value;
                pgo.atribuicaoISS = txtAtribuicaoISS.Value;
                pgo.situacaoNTFS = txtSituacaoNTFS.Value;
                pgo.ISSretido = txtISSRet.Value;
                pgo.registroTributacao = txtRegTributacao.Value;
                pgo.optSimples = txtOptSimples.Value;
                pgo.tipoNFConv = txtTipoNFConv.Value;
                pgo.codServicoMun = txtCodServicoMun.Value;
                pgo.tpTribServico = txtTipoTribServico.Value;

                context.Entry<Formulario>(pgo).State = EntityState.Modified;
                context.SaveChanges();

                lblMensagem.Text = "Dados salvos com sucesso.";

                divFunction.InnerHtml = "<script>fechaJanela('Formulario');</script>";
            }
        }
    }
}