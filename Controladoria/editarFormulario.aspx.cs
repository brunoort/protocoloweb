using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controladoria.Entities;

namespace ProtocoloWeb
{
    public partial class editarFormulario : System.Web.UI.Page
    {
        string cnString = ConfigurationManager.ConnectionStrings["ProtocoloWebConnectionString"].ConnectionString;

        public double FormatarDouble(string valor) // pode ser decimal
        {
            return double.Parse(valor, NumberStyles.Currency); // pode ser decimal
        }

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

            using (ProtocoloWebEntities context = new ProtocoloWebEntities())
            {
                var pgo = context.Formulario.FirstOrDefault(p => p.idFormulario == idForm);
                txtAP.Value = pgo.NumAP;
                txtEmpresa.Value = pgo.Empresa;
                txtCnpj.Value = pgo.CNPJ;
                sltMatriz.SelectedIndex = sltMatriz.Items.IndexOf(sltMatriz.Items.FindByValue(pgo.Matriz));
                txtNF.Value = pgo.NumNF;
                txtDataVencimento.Value = Convert.ToDateTime(pgo.dtVencimento).ToString("dd/MM/yyyy");
                txtValorNF.Value = Convert.ToDouble(pgo.valorNF).ToString("###,###,##0.00");
                txtDataEmissaoDoc.Value = Convert.ToDateTime(pgo.dtEmissaoDOC).ToString("dd/MM/yyyy");
                sltCodJustificativa.SelectedIndex = sltCodJustificativa.Items.IndexOf(sltCodJustificativa.Items.FindByValue(pgo.codJustificativa));
                txtDepartamento.Value = pgo.Departamento;
                sltTipoDocumento.SelectedIndex = sltTipoDocumento.Items.IndexOf(sltTipoDocumento.Items.FindByValue(pgo.tipoDocumento));
                txtDocumento.Value = pgo.documento;
                sltVinculo.SelectedIndex = sltVinculo.Items.IndexOf(sltVinculo.Items.FindByValue(pgo.vinculo));
                sltTipoContrato.SelectedIndex = sltTipoContrato.Items.IndexOf(sltTipoContrato.Items.FindByValue(pgo.tipoContrato));
                txtContratoInicio.Value = Convert.ToDateTime(pgo.contratoInicio).ToString("dd/MM/yyyy");
                txtContratoFim.Value = Convert.ToDateTime(pgo.contratoFim).ToString("dd/MM/yyyy");
                txtReceitaFederal.Value = Convert.ToDateTime(pgo.receitaFederal).ToString("dd/MM/yyyy");
                txtPrefeitura.Value = Convert.ToDateTime(pgo.prefeitura).ToString("dd/MM/yyyy");
                txtCNAE.Value = Convert.ToDateTime(pgo.cnae).ToString("dd/MM/yyyy");
                sltOptanteSimples.SelectedIndex = sltOptanteSimples.Items.IndexOf(sltOptanteSimples.Items.FindByValue(pgo.optanteSimples.ToString()));
                txtOptanteSimples.Value = Convert.ToDateTime(pgo.optanteSimplesData).ToString("dd/MM/yyyy");
                txtSintegra.Value = Convert.ToDateTime(pgo.sintegra).ToString("dd/MM/yyyy"); 
                txtJuntaComercial.Value = Convert.ToDateTime(pgo.juntaComercial).ToString("dd/MM/yyyy");
                txtIRpct.Value = pgo.IRpct.ToString();
                txtIRvalor.Value = Convert.ToDouble(pgo.IRvalor).ToString("###,###,##0.00");
                txtPCCpct.Value = pgo.PCCpct.ToString();
                txtPCCvalor.Value = Convert.ToDouble(pgo.PCCvalor).ToString("###,###,##0.00");
                txtISSpct.Value = pgo.ISSpct.ToString();
                txtISSvalor.Value = Convert.ToDouble(pgo.ISSvalor).ToString("###,###,##0.00");
                txtINSSpct.Value = pgo.INSSpct.ToString();
                txtINSSvalor.Value = Convert.ToDouble(pgo.INSSvalor).ToString("###,###,##0.00");
                txtLiquido.Value = pgo.liquido;
                txtCadastroMunicipal.Value = Convert.ToDateTime(pgo.cadastroMunicipal).ToString("dd/MM/yyyy");
                txtCodRetencaoISS.Value = pgo.CodRetencaoISS;
                txtPCCnfs.Value = pgo.PCCnfs;
                txtCFOP.Value = pgo.CFOP;

                
                if (pgo.ZA == true)
                {
                    categoriaNFZA.Checked = true;
                }

                if (pgo.ZS == true)
                {
                    categoriaNFZS.Checked = true;
                }

                if (pgo.NT == true)
                {
                    categoriaNFNT.Checked = true;
                }

                txtCodServico.Value = pgo.codServico;
                txtValorDeducao.Value = pgo.valorDeducao;
                txtAliquota.Value = pgo.aliquota;
                txtTipoDocumento.Value = pgo.tipoDeDocumento;
                txtTributacaoServico.Value = pgo.tributacaoServico;
                txtCodPrestador.Value = pgo.codigoPrestador;
                txtAtribuicaoISS.Value = pgo.atribuicaoISS;
                txtSituacaoNTFS.Value = pgo.situacaoNTFS;
                txtISSRet.Value = pgo.ISSretido;
                txtRegTributacao.Value = pgo.registroTributacao;
                //RJ
                if (vMtz == "RN")
                {
                    txtOptSimples.Value = pgo.optSimples;
                    txtTipoNFConv.Value = pgo.tipoNFConv;
                    txtCodServicoMun.Value = pgo.codServicoMun;
                    txtTipoTribServico.Value = pgo.tpTribServico;
                }

            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            string vMtz = Request.QueryString["mtz"];
            if (vMtz == "RN")
            {
                divCarimboRJ.InnerHtml = "<script>showCarimboRJ();</script>";
            }
        }

        protected void SalvarAlteracao(object send, EventArgs e)
        {
            int idForm = Convert.ToInt32(Request.QueryString["id"]);
            using (ProtocoloWebEntities context = new ProtocoloWebEntities())
            {
                var pgo = context.Formulario.FirstOrDefault(p => p.idFormulario == idForm);

                pgo.Empresa = txtEmpresa.Value;
                pgo.CNPJ = txtCnpj.Value;
                pgo.Matriz = sltMatriz.Value;
                pgo.NumNF = txtNF.Value;
                pgo.NumAP = txtAP.Value;
                pgo.dtVencimento = Convert.ToDateTime(txtDataVencimento.Value);
                pgo.valorNF = txtValorNF.Value.Replace(",", "").Replace(".", "");
                pgo.dtEmissaoDOC = Convert.ToDateTime(txtDataEmissaoDoc.Value);
                pgo.codJustificativa = sltCodJustificativa.Value;
                pgo.Departamento = txtDepartamento.Value;
                pgo.tipoDocumento = sltTipoDocumento.Value;
                pgo.documento = txtDocumento.Value;
                pgo.loginSolicitante = Environment.UserName;
                pgo.vinculo = sltVinculo.Value;
                pgo.tipoContrato = sltTipoContrato.Value;
                pgo.contratoInicio = Convert.ToDateTime(txtContratoInicio.Value);
                pgo.contratoFim = Convert.ToDateTime(txtContratoFim.Value);
                pgo.receitaFederal = Convert.ToDateTime(txtReceitaFederal.Value);
                pgo.prefeitura = Convert.ToDateTime(txtPrefeitura.Value);
                pgo.cnae = Convert.ToDateTime(txtCNAE.Value);
                pgo.optanteSimples = Convert.ToBoolean(sltOptanteSimples.Value);
                pgo.optanteSimplesData = Convert.ToDateTime(txtOptanteSimples.Value);
                pgo.sintegra = Convert.ToDateTime(txtSintegra.Value);
                pgo.juntaComercial = Convert.ToDateTime(txtJuntaComercial.Value);
                pgo.IRpct = Convert.ToInt32(txtIRpct.Value);
                pgo.IRvalor = txtIRvalor.Value;
                pgo.PCCpct = Convert.ToInt32(txtPCCpct.Value);
                pgo.PCCvalor = txtPCCvalor.Value;
                pgo.ISSpct = Convert.ToInt32(txtISSpct.Value);
                pgo.ISSvalor = txtISSvalor.Value;
                pgo.INSSpct = Convert.ToInt32(txtINSSpct.Value);
                pgo.INSSvalor = txtINSSvalor.Value;
                pgo.liquido = txtLiquido.Value;
                pgo.cadastroMunicipal = Convert.ToDateTime(txtCadastroMunicipal.Value);
                pgo.CodRetencaoISS = txtCodRetencaoISS.Value;
                pgo.PCCnfs = txtPCCnfs.Value;
                pgo.CFOP = txtCFOP.Value;

                context.Entry<Formulario>(pgo).State = EntityState.Modified;
                context.SaveChanges();

                lblMensagem.Text = "Dados salvos com sucesso.";

                divFunction.InnerHtml = "<script>fechaJanela('Formulario');</script>";
            }

        }
    }
}