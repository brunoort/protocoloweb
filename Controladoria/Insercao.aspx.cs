using System;
using System.Configuration;
using System.Data;
using System.Data.Metadata.Edm;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using Controladoria;
using Controladoria.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ProtocoloWeb
{
    public partial class Insercao : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                lblLogin.Text = Session["User"].ToString();

                var def = new _Default();
                def.verificaUsuario();
                loadFiles();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private void loadFiles()
        {
            DirectoryInfo pasta = new DirectoryInfo(Server.MapPath("~/Files/"));

            FileInfo[] arquivos = pasta.GetFiles();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add("Nome");
            dt.Columns.Add("Selecionar");

            //Data Atual
            string dtAtual = DateTime.Now.ToString("dd/MM/yyy");
            string dtAnterior = DateTime.Now.AddDays(-1).ToString("dd/MM/yyy");
            int cArquivos = 0;

            foreach (FileInfo files in arquivos)
            {
                string dtFile = files.LastWriteTime.ToString("dd/MM/yyyy");
                DataRow dr = dt.NewRow();


                dr["Nome"] = files.Name;
                dr["Selecionar"] = "<input type='button' value='Selecionar' onclick=selecionaDocumento('" +
                                   files.Name + "') />";

                if (dtFile == dtAtual || dtFile == dtAnterior)
                {
                    dt.Rows.Add(dr);
                    cArquivos++;
                }
            }

            if (cArquivos > 0)
            {
                ds.Tables.Add(dt);
                dados.InnerHtml = DsToHtml(ds);
            }
            else
            {
                dados.InnerHtml = "Não existem arquivos.";
            }

        }

        public string DsToHtml(DataSet ds)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter w = new HtmlTextWriter(sw);
            string table;

            foreach (DataTable dt in ds.Tables)
            {
                //Create a table
                Table tbl = new Table();

                //Create column header row
                TableHeaderRow thr = new TableHeaderRow();
                foreach (DataColumn col in dt.Columns)
                {
                    TableHeaderCell th = new TableHeaderCell();
                    th.Text = col.Caption;
                    th.HorizontalAlign = HorizontalAlign.Left;
                    th.Width = 220;
                    thr.Controls.Add(th);
                }
                tbl.Controls.Add(thr);

                //Create table rows
                foreach (DataRow row in dt.Rows)
                {
                    TableRow tr = new TableRow();
                    foreach (var value in row.ItemArray)
                    {
                        TableCell td = new TableCell();
                        td.Text = value.ToString();
                        tr.Controls.Add(td);
                    }
                    tbl.Controls.Add(tr);
                }

                tbl.RenderControl(w);

            }

            table = sw.ToString();
            table = table.Replace("<table>", "<table border=1 cellspacing=0 cellpadding=2 bordercolor='666633'>");
            return table;

        }

        protected bool verificaDocumento(string pDocumento)
        {
            //Verificar se o documento existe.
            if (File.Exists(Server.MapPath(@"//dioxido/Work/ProtocoloWeb_bf/protocoloweb/" + pDocumento)))
            {
                return true;
            }
            else
            {
                return false;
            }

            return true;
        }

        class Ap
        {
            public string Empresa { get; set; }
            public string Matriz { get; set; }
            public string NF { get; set; }
            public string DataVencimento { get; set; }
            public string ValorNF { get; set; }
            public string DataEmissaoDoc { get; set; }
            public int CodJustificativa { get; set; }
            public int Departamento { get; set; }
            public string TipoDocumento { get; set; }
        }

        public void PesquisarAP_Click(object sender, EventArgs e)
        {
            string numAP = txtAP.Value;

            //string url = Comum.BuscarChaveWebConfig("UrlWebAPIRelatorio") + numAP + "?usuario=" + usuario + "&aplicacao=" + aplicacao + "&token=" + token;
            string url = "http://paladio03:100/api/Ap/" + numAP;
        
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {

                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {

                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    var result = JsonConvert.DeserializeObject(reader.ReadToEnd());
                    JObject userData = JObject.Parse(result.ToString());
                    if (result.ToString() != "")
                    {
                        bool vAutorizado = false;

                        var Departamento = userData["DEP"].ToString().ToUpperInvariant();

                        var arrDptoAutorizado = Comum.BuscarChaveWebConfig("Departamentos").ToUpperInvariant().Split(',');

                        if (Session["Departamento"].ToString().ToUpperInvariant() == Departamento || Departamento.Contains(Session["Departamento"].ToString().ToUpperInvariant()))
                        {
                            vAutorizado = true;
                        }
                        else
                        {
                            foreach (var dptos in arrDptoAutorizado)
                            {
                                if (vAutorizado != true)
                                {
                                    if (Session["Departamento"].ToString().ToUpperInvariant() == dptos.ToUpperInvariant())
                                    {
                                        vAutorizado = true;
                                    }
                                    else
                                    {
                                        vAutorizado = false;
                                    }
                                }
                            }

                        }


                        if (vAutorizado == true)
                        {
                            txtEmpresa.Value = userData["NOME_FORN"].ToString();
                            txtCnpj.Value = userData["CNPJ_FORN"].ToString();
                            sltMatriz.Value = userData["CENTRO_PAGTO"].ToString();
                            txtNF.Value = userData["NUMNOTA_PAGTO"].ToString();
                            txtNumPedido.Value = userData["NUM_PED"].ToString();
                            txtDataVencimento.Value = userData["VENC_PAGTO"].ToString().Substring(0, 10);
                            txtValorNF.Value = userData["VALOR_NOTA"].ToString().Trim();
                            txtDataEmissaoDoc.Value = userData["DATA_DOCTO"].ToString().Substring(0, 10);
                            txtDepartamento.Value = userData["DEPART_PAGTO"].ToString();

                            lblMensagem.Text = String.Empty;

                            
                            
                        }
                        else
                        {
                            //lblMensagem.Text = "Nenhum registro encontrado para este código/Sem acesso ! (" + Session["Departamento"].ToString().ToUpperInvariant() + " / " + Departamento + ")";
                            lblMensagem.Text = "Nenhum registro encontrado para este código/Sem acesso !";
                            limpaForm();
                        }
                    }
                    else
                    {
                        lblMensagem.Text = "Ap Inexistente";
                        limpaForm();
                    }
                }
            }
            catch (WebException ex)
            {
                throw new WebException(ex.Message);
            }
        }

        public class RelatorioExportAp
        {
            public string NumPedido { get; set; }
            public string Empresa { get; set; }
            public string Cnpj { get; set; }
            public string Matriz { get; set; }
            public string NumNf { get; set; }
            public string DtVencimento { get; set; }
            public string ValorNf { get; set; }
            public string DtEmissaoDoc { get; set; }
            public string Departamento { get; set; }


        }

        public string cnString = ConfigurationManager.ConnectionStrings["ProtocoloWebConnectionString"].ConnectionString;

        protected bool VerificaExisteAP(string pNumAP)
        {
            string strSQL = "SELECT * FROM Formulario WHERE NumAP = '" + pNumAP + "'";
            SqlConnection sqlConnection = new SqlConnection(cnString);
            SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
            sqlConnection.Open();
            SqlDataReader rd = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

            if (rd.HasRows)
            {
                sqlConnection.Close();
                return true;
            }
            else
            {
                sqlConnection.Close();
                return false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            using (ProtocoloWebEntities ctx = new ProtocoloWebEntities())
            {
                //Criando Formulário
                Formulario form = new Formulario();

                //Capturando valores
                form.Empresa = txtEmpresa.Value;
                form.CNPJ = txtCnpj.Value;
                form.Matriz = sltMatriz.Value;
                form.NumNF = txtNF.Value.Trim();
                form.NumPedido = txtNumPedido.Value.Trim();
                form.NumAP = txtAP.Value.Trim();
                form.dtVencimento = DateTime.ParseExact(txtDataVencimento.Value, "dd/MM/yyyy", null);
                //form.valorNF = txtValorNF.Value.Replace(",","").Replace(".","");
                form.valorNF = txtValorNF.Value;
                form.dtEmissaoDOC = DateTime.ParseExact(txtDataEmissaoDoc.Value, "dd/MM/yyyy", null); ;
                form.codJustificativa = txtJustificativa.Value;
                form.Departamento = txtDepartamento.Value;
                form.tipoDocumento = sltTipoDocumento.Value;
                //form.documento = txtDocumento.Value;
                form.emailResponsavel = txtEmailResponsavel.Value;
                form.statusControladoria = false;
                form.loginSolicitante = Session["User"].ToString();
                form.dataCadastro = DateTime.ParseExact(DateTime.Now.ToString(), "dd/MM/yyyy HH:mm:ss", null);
                //form.vinculo = sltVinculo.Value;
                //form.tipoContrato = sltTipoContrato.Value;
                //form.contratoInicio = DateTime.ParseExact(txtContratoInicio.Value, "dd/MM/yyyy", null);
                //form.contratoFim = DateTime.ParseExact(txtContratoFim.Value, "dd/MM/yyyy", null);
                //form.receitaFederal = DateTime.ParseExact(txtReceitaFederal.Value, "dd/MM/yyyy", null);
                //form.prefeitura = DateTime.ParseExact(txtPrefeitura.Value, "dd/MM/yyyy", null);
                //form.cnae = DateTime.ParseExact(txtCNAE.Value, "dd/MM/yyyy", null);
                //form.optanteSimples = Convert.ToBoolean(sltOptanteSimples.Value);
                //form.optanteSimplesData = DateTime.ParseExact(txtOptanteSimples.Value, "dd/MM/yyyy", null);
                //form.sintegra = DateTime.ParseExact(txtSintegra.Value, "dd/MM/yyyy", null);
                //form.juntaComercial = DateTime.ParseExact(txtJuntaComercial.Value, "dd/MM/yyyy", null);
                //form.IRpct = Convert.ToInt32(txtIRpct.Value);
                //form.IRvalor = txtIRvalor.Value;
                //form.PCCpct = Convert.ToInt32(txtPCCpct.Value);
                //form.PCCvalor = txtPCCvalor.Value;
                //form.ISSpct = Convert.ToInt32(txtISSpct.Value);
                //form.ISSvalor = txtISSvalor.Value;
                //form.INSSpct = Convert.ToInt32(txtINSSpct.Value);
                //form.INSSvalor = txtINSSvalor.Value;
                //form.liquido = txtLiquido.Value;
                //form.cadastroMunicipal = DateTime.ParseExact(txtCadastroMunicipal.Value, "dd/MM/yyyy", null);
                //form.CodRetencaoISS = txtCodRetencaoISS.Value;
                //form.PCCnfs = txtPCCnfs.Value;
                //form.CFOP = txtCFOP.Value;
                form.Excluido = false;

                //Verificação da existência do documento
                //string urlDocumento = txtDocumento.Value;
                //bool documentoExiste = verificaDocumento(urlDocumento);
                bool documentoExiste = true;

                ////Capturando dados do Solicitante
                //string logado = Environment.UserName;
                //IDictionary<string, string> userData;
                //Usuario usr = new Usuario();
                //userData = usr.verificaUsuario(logado);

                form.emailSolicitante = Session["Email"].ToString();

                if (txtEmpresa.Value != "" && txtNF.Value != "" && txtDataVencimento.Value != "" && txtValorNF.Value != "" && sltTipoDocumento.Value != "")
                {
                    //Verificando se a AP já existe na base.
                    bool apExiste = VerificaExisteAP(txtAP.Value);

                    if (apExiste == false)
                    {
                        if (documentoExiste == true)
                        {
                            //Salvando form

                            ctx.Formulario.Add(form);
                            ctx.SaveChanges();

                            //Limpando dados cadastrados
                            txtEmpresa.Value = "";
                            txtNumPedido.Value = "";
                            txtCnpj.Value = "";
                            sltMatriz.Value = "";
                            //sltMatriz.SelectedIndex = 0;
                            txtNF.Value = "";
                            txtAP.Value = "";
                            txtDataVencimento.Value = "";
                            txtValorNF.Value = "";
                            txtDataEmissaoDoc.Value = "";
                            sltCodJustificativa.Value = "";
                            txtDepartamento.Value = "";
                            sltTipoDocumento.SelectedIndex = 0;
                            //txtDocumento.Value = "";

                            //Mensagem de Exibição
                            lblMensagem.Text = "Cadastro efetuado com sucesso.";

                            //Enviando E-mail de confirmação de cadastro
                            EnviarEmail enviarEmail = new EnviarEmail();
                            enviarEmail.Enviar("Cadastro", form.NumAP, form.emailSolicitante, form.emailResponsavel, Comum.BuscarChaveWebConfig("mail"));

                        }
                        else
                        {
                            lblMensagem.Text = "O documento informado não existe.";
                        }
                    }
                    else
                    {
                        lblMensagem.Text = "Essa AP já está cadastrada.";
                    }
                }
                else
                {
                    lblMensagem.Text = "O preenchimento de todos os campos é obrigatório.";
                }

            }

        }

      protected void limpaForm()
      {
          //Limpando dados cadastrados
          txtEmpresa.Value = "";
          txtNumPedido.Value = "";
          txtCnpj.Value = "";
          sltMatriz.Value = "";
          //sltMatriz.SelectedIndex = 0;
          txtNF.Value = "";
          txtAP.Value = "";
          txtDataVencimento.Value = "";
          txtValorNF.Value = "";
          txtDataEmissaoDoc.Value = "";
          sltCodJustificativa.Value = "";
          txtDepartamento.Value = "";
          sltTipoDocumento.SelectedIndex = 0;
          //txtDocumento.Value = "";
      }
    }
}