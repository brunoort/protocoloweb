using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Controladoria;
using Controladoria.Entities;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using NPOI.POIFS.FileSystem;
using NPOI.HPSF;

namespace ProtocoloWeb
{
    public partial class Painel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                var def = new _Default();
                def.verificaUsuario();

                if (!IsPostBack)
                {
                    //Lista das áreas
                    var listaView = new Dictionary<int, string>();
                    _Default dft = new _Default();
                    listaView = dft.ListaAreas();

                    string idArea = Request.QueryString["idtipo"];
                    string descArea = String.Empty;

                    //Percorrendo a lista 
                    foreach (KeyValuePair<int, string> area in listaView)
                    {
                        if (Convert.ToInt32(idArea) == area.Key)
                        {
                            descArea = area.Value;
                        }
                    }

                    divNomePainel.InnerHtml = "Painel " + descArea;
                    Session["AreaDesc"] = descArea;

                    //Carrega dados
                    carregaDados(descArea);
                }
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

        public string cnString = ConfigurationManager.ConnectionStrings["ProtocoloWebConnectionString"].ConnectionString;

        private void carregaDados(string pDescArea)
        {
            GridView1.DataSource = "";
            GridView1.DataBind();

            string strSQL = String.Empty;
            strSQL =
                "SELECT idFormulario,Empresa,CNPJ, Matriz, NumNF, NumAP,NumPedido, CONVERT(VARCHAR(10), dtVencimento, 103) as dtVencimento, valorNF, CONVERT(VARCHAR(10), dtEmissaoDOC, 103) as dtEmissaoDOC , codJustificativa, Departamento, tipoDocumento, documento, statusControladoria, loginSolicitante, emailSolicitante, CONVERT(VARCHAR(10), dataCadastro, 103) as dataCadastro, Reprovado from Formulario WHERE status" +
                pDescArea + " = 'False' AND concluido is NULL  ORDER BY idFormulario ASC";
            SqlConnection sqlConnection = new SqlConnection(cnString);
            SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

            GridView1.DataSource = reader;
            GridView1.DataBind();

            sqlConnection.Close();
            sqlCommand.Dispose();

            if (GridView1.Rows.Count > 0)
            {

                lblResposta.Text = string.Empty;
                ajustaTabela();
            }
            else
            {
                lblResposta.Text = "Não existe nenhum registro.";
                btnImprimir.Visible = false;
                btnExport.Visible = false;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //in every row I would like to check the text of the second cell
                var ReprovadoValue = GridView1.DataKeys[i]["Reprovado"].ToString();
                
                if (ReprovadoValue == "True")
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.LightSalmon;
                }
            }

        }

        public void ajustaTabela()
        {

            //Ajustes para Mobile
            bool mobile = isMobileBrowser();
            if (mobile)
            {
                loadGridView.InnerHtml = "<script>dimensaoGrid();</script>";
            }

        }

        public static bool isMobileBrowser()
        {

            bool bRetorno = false;
            string parameter = null;

            for (int i = 0; i < list.Length; i++)
            {
                HttpRequest Request = HttpContext.Current.Request;

                parameter = Request.UserAgent;

                if (!String.IsNullOrEmpty(parameter))
                {
                    if ((parameter.ToLower().IndexOf(list[i].ToLower()) > -1))
                    {
                        bRetorno = true;
                    }
                }
            }

            return bRetorno;
        }

        private static string[] list =
            {
                "iphone", "nokia", "blackberry", "lg-",
                "mot-", "palm", "samsung", "sec",
                "android", "and", "sonyericsson", "windows ce", "opera mini",
                "avantgo", "htc"
            };


        protected void CheckBoxReprova_CheckedChanged(object sender, EventArgs e)
        {
            //Variáveis
            int idTipo = Convert.ToInt32(Request.QueryString["idtipo"]);
            string tipoDocumento = String.Empty;
            string vArea = String.Empty;
            string strSQL = String.Empty;
            int countAreas = 0;
            int vIdTipoPrev = idTipo - 1;
            string vAreaPrev = String.Empty;
            List<int> listaReprovadas = new List<int>();
            string vNumThisAp = "";

            if (idTipo > 1)
            {
                //Procura checkbox reprova
                foreach (GridViewRow row in GridView1.Rows)
                {
                    CheckBox ch2 = (CheckBox)row.FindControl("CheckBoxReprova");

                    if (ch2 != null)
                    {
                        if (ch2.Checked)
                        {
                            listaReprovadas.Add(Convert.ToInt32(row.Cells[0].Text));

                            tipoDocumento = row.Cells[11].Text;
                            vNumThisAp = row.Cells[5].Text;
                        }
                    }
                }

                _Default def = new _Default();
                Dictionary<int, string> areas = def.ListaAreas();

                //Identificando qual a área que está sendo atualizada
                foreach (KeyValuePair<int, string> itens in areas)
                {
                    if (idTipo == itens.Key)
                    {
                        vArea = itens.Value;
                    }
                    countAreas++;
                }

                bool ExisteReprovacao = VerificaReprovacao(listaReprovadas[0].ToString(), vArea);

                if (ExisteReprovacao == false)
                {
                    //Abrindo popup de justificativa
                    Reprova(sender, e);

                    //Atualizando Reprovadas
                    foreach (int itens in listaReprovadas)
                    {

                        if (idTipo > 1)
                        {
                            strSQL += "UPDATE Formulario SET status" + vArea + " = Null, Reprovado = 'True' WHERE idFormulario = " + itens +
                                      ";";

                            if (vIdTipoPrev < countAreas)
                            {
                                foreach (KeyValuePair<int, string> item in areas)
                                {
                                    if (vIdTipoPrev == item.Key)
                                    {
                                        vAreaPrev = item.Value;
                                    }
                                }

                                //Fluxo de Retorno
                                if (idTipo == 3 || idTipo == 2)
                                {
                                    strSQL +=
                                        "UPDATE Formulario SET statusControladoria = 'False' WHERE idFormulario = " +
                                        itens + ";";
                                }

                                if (idTipo == 4)
                                {
                                    strSQL +=
                                        "UPDATE Formulario SET statusControladoria = 'False' WHERE idFormulario = " +
                                        itens + ";";
                                }

                                if (idTipo == 5)
                                {
                                    if (tipoDocumento == "PREST. CONTAS" || tipoDocumento == "AMEX VIAGENS" || tipoDocumento == "AMEX CORPORATIVO")
                                    {
                                        strSQL +=
                                            "UPDATE Formulario SET statusPCeAdiantamentos = 'False' WHERE idFormulario = " +
                                            itens + ";";
                                    }
                                    else if (tipoDocumento == "INVOICE" || tipoDocumento == "DI")
                                    {
                                        strSQL +=
                                            "UPDATE Formulario SET statusComex = 'False' WHERE idFormulario = " +
                                            itens + ";";
                                    }
                                    else
                                    {
                                        strSQL +=
                                            "UPDATE Formulario SET statusFinanceira = 'False' WHERE idFormulario = " +
                                            itens + ";";
                                    }

                                    //Tirando do Fiscal e do Fornecedor
                                    strSQL +=
                                        "UPDATE Formulario SET statusFiscal = NULL WHERE idFormulario = " +
                                        itens + ";";
                                    strSQL +=
                                        "UPDATE Formulario SET statusFornecedor = NULL WHERE idFormulario = " +
                                        itens + ";";
                                }

                                if (idTipo == 6)
                                {
                                    if (tipoDocumento == "INVOICE" || tipoDocumento == "DI")
                                    {
                                        strSQL += "UPDATE Formulario SET statusComex = 'False' WHERE idFormulario = " +
                                                  itens +
                                                  ";";
                                    }
                                    else if (tipoDocumento == "PREST. CONTAS" || tipoDocumento == "ADIANTAMENTOS" || tipoDocumento == "AMEX VIAGENS" || tipoDocumento == "AMEX CORPORATIVO")
                                    {
                                        strSQL +=
                                            "UPDATE Formulario SET statusPCeAdiantamentos = 'False' WHERE idFormulario = " +
                                            itens +
                                            ";";
                                    }
                                    else
                                    {
                                        strSQL +=
                                            "UPDATE Formulario SET statusFinanceira = 'False' WHERE idFormulario = " +
                                            itens +
                                            ";";
                                    }

                                    //Tirando do Fiscal e do Fornecedor
                                    strSQL +=
                                        "UPDATE Formulario SET statusFiscal = NULL WHERE idFormulario = " +
                                        itens + ";";
                                    strSQL +=
                                        "UPDATE Formulario SET statusFornecedor = NULL WHERE idFormulario = " +
                                        itens + ";";
                                }

                                if (idTipo == 7)
                                {
                                    if (tipoDocumento != "ADIANTAMENTOS")
                                    {
                                        strSQL += "UPDATE Formulario SET statusFiscal = 'False' WHERE idFormulario = " +
                                                  itens +
                                                  ";";
                                        strSQL +=
                                            "UPDATE Formulario SET statusFornecedor = 'False' WHERE idFormulario = " +
                                            itens + ";";
                                    }
                                    else
                                    {
                                        strSQL +=
                                            "UPDATE Formulario SET statusFornecedor = 'False' WHERE idFormulario = " +
                                            itens + ";";
                                    }
                                }

                                //strSQL += "UPDATE Formulario SET status" + vAreaPrev + " = 'False' WHERE idFormulario = " + itens + ";";
                            }
                        }

                        //Salvando o histórico
                        strSQL += "INSERT INTO Historico (idFormulario,NumAP,login,data,acao,area) VALUES (" + itens + ",'" + vNumThisAp + "','" +
                                  Session["user"] + "',GETDATE(),'reprova','" + vArea + "');";
                    }

                    if ((!String.IsNullOrEmpty(strSQL)))
                    {
                        using (SqlConnection connection = new SqlConnection(cnString))
                        {
                            connection.Open();
                            SqlCommand command = connection.CreateCommand();

                            SqlTransaction transaction;
                            transaction = connection.BeginTransaction("SampleTransaction");

                            command.Connection = connection;
                            command.Transaction = transaction;

                            try
                            {
                                command.CommandText = strSQL;
                                command.ExecuteNonQuery();
                                transaction.Commit();

                                carregaDados(vArea);
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                            }

                        }

                    }
                    else
                    {
                        if (listaReprovadas.Count > 0)
                        {
                            divJustificativa.InnerHtml =
                                "<script>alert('Neste painel não é possível fazer Reprovações');document.location.reload(true);</script>";
                            //lblResposta.Text = "<br/>Você não pode fazer Reprovações<br/><br/>";
                        }
                    }
                }
                else
                {
                    divJustificativa.InnerHtml =
                        "<script>alert('ATENÇÃO: Essa AP já foi Reprovada por outro usuário.');document.location.reload(true);</script>";

                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        CheckBox ch2 = (CheckBox)row.FindControl("CheckBoxReprova");
                        ch2.Checked = false;
                    }
                }
            }
            else
            {
                //lblResposta.Text = "<br/>Você não pode fazer Reprovações<br/><br/>";
                divJustificativa.InnerHtml =
                                "<script>alert('Neste painel não é possível fazer Reprovações');document.location.reload(true);</script>";
                //lblResposta.Text = "<br/>Você não pode fazer Reprovações<br/><br/>";
                foreach (GridViewRow row in GridView1.Rows)
                {
                    CheckBox ch2 = (CheckBox)row.FindControl("CheckBoxReprova");
                    ch2.Checked = false;
                }
            }


        }

        protected void CheckBoxAprova_CheckedChanged(object sender, EventArgs e)
        {
            //Variáveis
            int idTipo = Convert.ToInt32(Request.QueryString["idtipo"]);
            string tipoDocumento = String.Empty;
            string vArea = String.Empty;
            string strSQL = String.Empty;
            int countAreas = 0;
            int vIdTipoNext = idTipo + 1;
            string vAreaNext = String.Empty;
            List<int> listaAprovadas = new List<int>();
            string finaliza = "N";
            string vNumThisAp = "";
            string vEmailSolicitante = "";
            string vEmailResponsavel = "";

            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox ch = (CheckBox)row.FindControl("CheckBoxAprova");
                if (ch != null)
                {
                    if (ch.Checked)
                    {
                        listaAprovadas.Add(Convert.ToInt32(row.Cells[0].Text));
                        tipoDocumento = row.Cells[11].Text;
                        vNumThisAp = row.Cells[5].Text;
                    }
                }
            }

            _Default def = new _Default();
            Dictionary<int, string> areas = def.ListaAreas();

            //Identificando qual a área que está sendo atualizada
            foreach (KeyValuePair<int, string> itens in areas)
            {
                if (idTipo == itens.Key)
                {
                    vArea = itens.Value;
                }
                countAreas++;
            }

            //Verificando se a AP foi aprovada por outro usuário
            bool ExisteAprovacao = VerificaAprovacao(listaAprovadas[0].ToString(), vArea);

            if (ExisteAprovacao == false && tipoDocumento != "")
            {
                //Atualizando Aprovadas
                foreach (int itens in listaAprovadas)
                {
                    strSQL += "UPDATE Formulario SET status" + vArea + " = 'True' WHERE idFormulario = " + itens + ";";
                    strSQL += "UPDATE Formulario SET Reprovado = '' WHERE idFormulario = " + itens + ";";

                    //Identificando próxima área e setando como False
                    //Para que a mesma seja exibida no painel da área
                    if (vIdTipoNext <= countAreas)
                    {
                        //Quando idTipo > 7 será finalizado na query acima
                        if (idTipo <= 7)
                        {
                            foreach (KeyValuePair<int, string> item in areas)
                            {
                                if (vIdTipoNext == item.Key)
                                {
                                    vAreaNext = item.Value;
                                }
                            }

                            // --------------------------
                            //      FLUXO
                            // --------------------------

                            //ProtocoloWeb
                            if (idTipo == 1)
                            {
                                //FLUXO AZUL
                                if (tipoDocumento == "NF" || tipoDocumento == "RECIBO" || tipoDocumento == "CI" ||
                                    tipoDocumento == "GUIA" || tipoDocumento == "CTR" || tipoDocumento == "DANFE" ||
                                    tipoDocumento == "FATURA" || tipoDocumento == "NOTA DE DEBITO")
                                {
                                    strSQL += "UPDATE Formulario SET statusFinanceira = 'False' WHERE idFormulario = " +
                                              itens + ";";
                                }

                                //FLUXO AZUL 2
                                if (tipoDocumento == "PREST. CONTAS" || tipoDocumento == "ADIANTAMENTOS" || tipoDocumento == "AMEX VIAGENS" || tipoDocumento == "AMEX CORPORATIVO")
                                {
                                    strSQL +=
                                        "UPDATE Formulario SET statusPCeAdiantamentos = 'False' WHERE idFormulario = " +
                                        itens + ";";
                                }

                                //FLUXO VERDE
                                if (tipoDocumento == "INVOICE" || tipoDocumento == "DI")
                                {
                                    strSQL += "UPDATE Formulario SET statusComex = 'False' WHERE idFormulario = " +
                                              itens + ";";
                                }

                                //Capturando o Email para enviar a mensagem de finalizado
                                string stringSQL = String.Empty;
                                stringSQL = "SELECT emailSolicitante,emailResponsavel FROM Formulario WHERE idFormulario = " + itens;
                                SqlConnection sqlConnection = new SqlConnection(cnString);
                                SqlCommand command = new SqlCommand(stringSQL, sqlConnection);
                                sqlConnection.Open();
                                SqlDataReader rd = command.ExecuteReader();

                                if (rd.Read())
                                {
                                    vEmailSolicitante = rd["emailSolicitante"].ToString();
                                    vEmailResponsavel = rd["emailResponsavel"].ToString();
                                    //Enviando o e-mail de conclusão do processo
                                    
                                    EnviarEmail enviarEmail = new EnviarEmail();
                                    enviarEmail.Enviar("Controladoria", vNumThisAp, vEmailSolicitante,vEmailResponsavel,Comum.BuscarChaveWebConfig("mail"));
                                }

                                sqlConnection.Close();
                            }

                            if (idTipo == 2 || idTipo == 3)
                            {
                                strSQL += "UPDATE Formulario SET statusFiscal = 'False' WHERE idFormulario = " +
                                          itens + ";";
                                strSQL += "UPDATE Formulario SET statusFornecedor = 'False' WHERE idFormulario = " +
                                          itens + ";";
                            }

                            if (idTipo == 4)
                            {
                                if (tipoDocumento == "PREST. CONTAS" || tipoDocumento == "AMEX VIAGENS" || tipoDocumento == "AMEX CORPORATIVO")
                                {
                                    strSQL += "UPDATE Formulario SET statusFiscal = 'False' WHERE idFormulario = " +
                                              itens + ";";
                                    strSQL += "UPDATE Formulario SET statusFornecedor = 'False' WHERE idFormulario = " +
                                              itens + ";";
                                }

                                if (tipoDocumento == "ADIANTAMENTOS")
                                {
                                    strSQL += "UPDATE Formulario SET statusFornecedor = 'False' WHERE idFormulario = " +
                                              itens + ";";
                                }
                            }

                            if (idTipo == 5 || idTipo == 6)
                            {
                                if (tipoDocumento != "ADIANTAMENTOS")
                                {
                                    //verificar statusFiscal e statusFornecedor
                                    string SQL = String.Empty;
                                    SQL =
                                        "SELECT idFormulario,statusFiscal,statusFornecedor from Formulario WHERE idFormulario = " +
                                        itens + "";
                                    SqlConnection sqlConnection = new SqlConnection(cnString);
                                    SqlCommand cmd = new SqlCommand(SQL, sqlConnection);
                                    sqlConnection.Open();
                                    SqlDataReader rd = cmd.ExecuteReader();

                                    string statusFiscal = String.Empty;
                                    string statusFornecedor = String.Empty;

                                    if (rd.Read())
                                    {
                                        statusFiscal = rd["statusFiscal"].ToString();
                                        statusFornecedor = rd["statusFornecedor"].ToString();
                                    }

                                    //Aprovando Fiscal e Fornecedor
                                    if ((statusFiscal == "True" && idTipo == 6) ||
                                        (statusFornecedor == "True" && idTipo == 5))
                                    {
                                        strSQL += "UPDATE Formulario SET statusInput = 'False' WHERE idFormulario = " +
                                                  itens +
                                                  ";";
                                    }

                                    sqlConnection.Close();
                                }
                                else
                                {
                                    strSQL += "UPDATE Formulario SET statusInput = 'False' WHERE idFormulario = " +
                                              itens +
                                              ";";
                                }
                            }
                        }
                    }

                    //Finalizando -> set Concluido = true
                    if (idTipo == 7)
                    {
                        strSQL += "UPDATE Formulario SET concluido = 'True' WHERE idFormulario = " + itens + ";";

                        finaliza = "Y";
                        strSQL += "INSERT INTO Historico (idFormulario,NumAP,login,data,acao,area) VALUES (" + itens + ",'" + vNumThisAp + "','" + Session["user"] + "',GETDATE(),'saida','" + vArea + "');";

                        //Capturando o Email para enviar a mensagem de finalizado
                        string stringSQL = String.Empty;
                        stringSQL = "SELECT emailSolicitante,emailResponsavel FROM Formulario WHERE idFormulario = " + itens;
                        SqlConnection sqlConnection = new SqlConnection(cnString);
                        SqlCommand command = new SqlCommand(stringSQL, sqlConnection);
                        sqlConnection.Open();
                        SqlDataReader rd = command.ExecuteReader();
                        
                        if (rd.Read())
                        {
                            vEmailSolicitante = rd["emailSolicitante"].ToString();
                            vEmailResponsavel = rd["emailResponsavel"].ToString();
                            //Enviando o e-mail de conclusão do processo
                            EnviarEmail enviarEmail = new EnviarEmail();
                            enviarEmail.Enviar("Finalizado", vNumThisAp, vEmailSolicitante,vEmailResponsavel, Comum.BuscarChaveWebConfig("mail"));
                        }

                        sqlConnection.Close();

                    }

                    //Salvando o histórico
                    if (finaliza != "Y")
                    {
                        strSQL += "INSERT INTO Historico (idFormulario,NumAP,login,data,acao,area) VALUES (" + itens + ",'" + vNumThisAp + "','" +
                                  Session["user"] + "',GETDATE(),'aprova','" + vArea + "');";
                    }
                }

                if (strSQL != "")
                {
                    using (SqlConnection connection = new SqlConnection(cnString))
                    {
                        connection.Open();
                        SqlCommand command = connection.CreateCommand();

                        SqlTransaction transaction;
                        transaction = connection.BeginTransaction("SampleTransaction");

                        command.Connection = connection;
                        command.Transaction = transaction;

                        try
                        {
                            command.CommandText = strSQL;
                            command.ExecuteNonQuery();
                            transaction.Commit();

                            carregaDados(vArea);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                        }

                    }
                }
            }
            else
            {
                if (tipoDocumento == "")
                {
                    divJustificativa.InnerHtml = "<script>alert('ATENÇÃO: Selecione um tipo de documento.');document.location.reload(true);</script>";
                }
                else
                {
                    divJustificativa.InnerHtml = "<script>alert('ATENÇÃO: Essa AP já foi aprovada por outro usuário.');document.location.reload(true);</script>";
                }


                foreach (GridViewRow row in GridView1.Rows)
                {
                    CheckBox ch2 = (CheckBox)row.FindControl("CheckBoxAprova");
                    ch2.Checked = false;
                }
            }
        }

        protected bool VerificaReprovacao(string pIdFormulario, string pDescArea)
        {
            string strSQL = "SELECT * FROM Formulario WHERE idFormulario = '" + pIdFormulario + "' AND status" + pDescArea + " is Null";
            SqlConnection sqlConnection = new SqlConnection(cnString);
            SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
            sqlConnection.Open();
            SqlDataReader rd = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            //DataTable dt = new DataTable();
            //dt.Load(rd);

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

        protected bool VerificaAprovacao(string pIdFormulario, string pDescArea)
        {
            string strSQL = "SELECT * FROM Formulario WHERE idFormulario = '" + pIdFormulario + "' AND status" + pDescArea + " = 'True' ";
            SqlConnection sqlConnection = new SqlConnection(cnString);
            SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
            sqlConnection.Open();
            SqlDataReader rd = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            //DataTable dt = new DataTable();
            //dt.Load(rd);

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

        protected void Reprova(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((CheckBox)sender).Parent.Parent)
            {
                string idForm = row.Cells[0].Text;
                Label1.Text = idForm;

                popSalvaJustificativa.Show();
            }
        }

        protected void SalvarEditar(object send, EventArgs e)
        {
            int idForm = Convert.ToInt32(idEditarForm.Value);
            using (ProtocoloWebEntities context = new ProtocoloWebEntities())
            {
                var pgo = context.Formulario.FirstOrDefault(p => p.idFormulario == idForm);

                pgo.Empresa = txtEmpresa_edit.Value;
                pgo.CNPJ = txtCnpj_edit.Value;
                pgo.Matriz = sltMatriz_edit.Value;
                pgo.NumNF = txtNF_edit.Value;
                pgo.NumAP = txtAP_edit.Value;
                pgo.dtVencimento = DateTime.ParseExact(txtDataVencimento_edit.Value, "dd/MM/yyyy", null);
                pgo.valorNF = txtValorNF_edit.Value;
                pgo.dtEmissaoDOC = DateTime.ParseExact(txtDataEmissaoDoc_edit.Value, "dd/MM/yyyy", null);
                pgo.codJustificativa = txtJustificativa_edit.Value;
                pgo.Departamento = txtDepartamento_edit.Value;
                pgo.tipoDocumento = sltTipoDocumento_edit.Value;
                pgo.documento = txtDocumento_edit.Value;
                pgo.vinculo = sltVinculo_edit.Value;
                pgo.tipoContrato = sltTipoContrato_edit.Value;
                pgo.contratoInicio = DateTime.ParseExact(txtContratoInicio_edit.Value, "dd/MM/yyyy", null);
                pgo.contratoFim = DateTime.ParseExact(txtContratoFim_edit.Value, "dd/MM/yyyy", null);
                pgo.receitaFederal = DateTime.ParseExact(txtReceitaFederal_edit.Value, "dd/MM/yyyy", null);
                pgo.prefeitura = DateTime.ParseExact(txtPrefeitura_edit.Value, "dd/MM/yyyy", null);
                pgo.cnae = DateTime.ParseExact(txtCNAE_edit.Value, "dd/MM/yyyy", null);
                pgo.optanteSimples = Convert.ToBoolean(sltOptanteSimples_edit.Value);
                pgo.optanteSimplesData = DateTime.ParseExact(txtOptanteSimples_edit.Value, "dd/MM/yyyy", null);
                pgo.sintegra = DateTime.ParseExact(txtSintegra_edit.Value, "dd/MM/yyyy", null);
                pgo.juntaComercial = DateTime.ParseExact(txtJuntaComercial_edit.Value, "dd/MM/yyyy", null);
                pgo.emailResponsavel = txtEmailResponsavel_edit.Value;

                context.Entry<Formulario>(pgo).State = EntityState.Modified;
                context.SaveChanges();

                GridView1.DataSource = "";
                GridView1.DataBind();

                string idArea = Request.QueryString["idtipo"];
                string descArea = String.Empty;

                var listaView = new Dictionary<int, string>();
                _Default dft = new _Default();
                listaView = dft.ListaAreas();

                //Percorrendo a lista 
                foreach (KeyValuePair<int, string> area in listaView)
                {
                    if (Convert.ToInt32(idArea) == area.Key)
                    {
                        descArea = area.Value;
                    }
                }

                //Carrega dados
                carregaDados(descArea);
                //divEditar.InnerHtml = "<script>atualizaPagina();</script>";
            }

        }

        protected void salvaJustificativa(object sender, EventArgs e)
        {
            int idForm = Convert.ToInt32(Label1.Text);
            string justificativa = txtJustificativa.Text;

            string strSQL =
                "INSERT INTO Justificativa (idFormulario,Justificativa,dtJustificativa,AreaDesc,UserDesc) VALUES (" +
                idForm + ",'" + justificativa + "','" + DateTime.Today.ToString("yyyy-MM-dd") + "','" +
                Session["AreaDesc"] + "','" + Session["User"] + "')";
            SqlConnection sqlConnection = new SqlConnection(cnString);
            SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void salvarCarimbo(object sender, EventArgs e)
        {
            int idForm = Convert.ToInt32(Label2.Text);

            using (ProtocoloWebEntities context = new ProtocoloWebEntities())
            {
                var pgo = context.Formulario.FirstOrDefault(p => p.idFormulario == idForm);

                pgo.valorNF = txtValorNF_carimbo.Value;

                pgo.IRpct = Convert.ToDecimal(txtIRpct_edit.Value.Replace(".", ","));
                pgo.IRbase = txtIRbase_edit.Value;
                pgo.IRvalor = txtIRvalor_edit.Value;

                pgo.PCCpct = Convert.ToDecimal(txtPCCpct_edit.Value.Replace(".", ","));
                pgo.PCCbase = txtPCCbase_edit.Value;
                pgo.PCCvalor = txtPCCvalor_edit.Value;

                pgo.ISSpct = Convert.ToDecimal(txtISSpct_edit.Value.Replace(".", ","));
                pgo.ISSbase = txtISSbase_edit.Value;
                pgo.ISSvalor = txtISSvalor_edit.Value;

                pgo.INSSpct = Convert.ToDecimal(txtINSSpct_edit.Value.Replace(".", ","));
                pgo.INSSbase = txtINSSbase_edit.Value;
                pgo.INSSvalor = txtINSSvalor_edit.Value;

                pgo.liquido = txtLiquido_edit.Value;
                pgo.cadastroMunicipal = DateTime.ParseExact(txtCadastroMunicipal_edit.Value, "dd/MM/yyyy", null);
                pgo.CodRetencaoISS = txtCodRetencaoISS_edit.Value;
                pgo.PCCnfs = txtPCCnfs_edit.Value;
                pgo.CFOP = txtCFOP_edit.Value;

                if (categoriaNFZA.Checked == true)
                {
                    pgo.ZA = true;
                }
                else
                {
                    pgo.ZA = false;
                }

                if (categoriaNFZS.Checked == true)
                {
                    pgo.ZS = true;
                }
                else
                {
                    pgo.ZS = false;
                }

                if (categoriaNFNT.Checked == true)
                {
                    pgo.NT = true;
                }
                else
                {
                    pgo.NT = false;
                }
                pgo.lote = txtLote.Value;
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
                //divEditar.InnerHtml = "<script>atualizaPagina();</script>";
            }
        }

        protected void showCarimbo(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {
                int idForm = Convert.ToInt32(row.Cells[0].Text);
                string vMatriz = row.Cells[3].Text;

                Label2.Text = idForm.ToString();
                HiddenFieldMatriz.Value = vMatriz;

                if (vMatriz == "RN")
                {
                    Carimbo1.Style.Add("display", "inline");
                    Carimbo2.Style.Add("display", "inline");
                    Carimbo3.Style.Add("display", "inline");
                    Carimbo4.Style.Add("display", "inline");
                }

                using (ProtocoloWebEntities context = new ProtocoloWebEntities())
                {
                    var pgo = context.Formulario.FirstOrDefault(p => p.idFormulario == idForm);

                    txtValorNF_carimbo.Value = pgo.valorNF;

                    txtIRpct_edit.Value = pgo.IRpct.ToString();


                    if (pgo.IRpct == null)
                    {
                        txtIRpct_edit.Value = "0";
                    }
                    else
                    {
                        txtIRpct_edit.Value = pgo.IRpct.ToString();
                    }
                    //txtIRvalor_edit.Value = Convert.ToDouble(pgo.IRvalor).ToString("###,###,##0.00");
                    txtIRbase_edit.Value = pgo.IRbase;
                    txtIRvalor_edit.Value = pgo.IRvalor;

                    if (pgo.PCCpct == null)
                    {
                        txtPCCpct_edit.Value = "0";
                    }
                    else
                    {
                        txtPCCpct_edit.Value = pgo.PCCpct.ToString();
                    }
                    //txtPCCvalor_edit.Value = Convert.ToDouble(pgo.PCCvalor).ToString("###,###,##0.00");
                    txtPCCbase_edit.Value = pgo.PCCbase;
                    txtPCCvalor_edit.Value = pgo.PCCvalor;

                    if (pgo.ISSpct == null)
                    {
                        txtISSpct_edit.Value = "0";
                    }
                    else
                    {
                        txtISSpct_edit.Value = pgo.ISSpct.ToString();
                    }
                    //txtISSvalor_edit.Value = Convert.ToDouble(pgo.ISSvalor).ToString("###,###,##0.00");
                    txtISSbase_edit.Value = pgo.ISSbase;
                    txtISSvalor_edit.Value = pgo.ISSvalor;

                    if (pgo.INSSpct == null)
                    {
                        txtINSSpct_edit.Value = "0";
                    }
                    else
                    {
                        txtINSSpct_edit.Value = pgo.INSSpct.ToString();
                    }
                    //txtINSSvalor_edit.Value = Convert.ToDouble(pgo.INSSvalor).ToString("###,###,##0.00");
                    txtINSSbase_edit.Value = pgo.INSSbase;
                    txtINSSvalor_edit.Value = pgo.INSSvalor;

                    if (pgo.liquido == null)
                    {
                        txtLiquido_edit.Value = "0";
                    }
                    else
                    {
                        txtLiquido_edit.Value = pgo.liquido.ToString();
                    }


                    txtCadastroMunicipal_edit.Value =
                        Convert.ToDateTime(pgo.cadastroMunicipal).ToString("dd/MM/yyyy");

                    if (pgo.CodRetencaoISS == null)
                    {
                        txtCodRetencaoISS_edit.Value = "0";
                    }
                    else
                    {
                        txtCodRetencaoISS_edit.Value = pgo.CodRetencaoISS.ToString();
                    }


                    if (pgo.PCCnfs == null)
                    {
                        txtPCCnfs_edit.Value = "0";
                    }
                    else
                    {
                        txtPCCnfs_edit.Value = pgo.PCCnfs.ToString();
                    }


                    if (pgo.CFOP == null)
                    {
                        txtCFOP_edit.Value = "0";
                    }
                    else
                    {
                        txtCFOP_edit.Value = pgo.CFOP.ToString();
                    }

                    if (pgo.ZA == true)
                    {
                        categoriaNFZA.Checked = true;
                    }
                    else
                    {
                        categoriaNFZA.Checked = false;
                    }

                    if (pgo.ZS == true)
                    {
                        categoriaNFZS.Checked = true;
                    }
                    else
                    {
                        categoriaNFZS.Checked = false;
                    }

                    if (pgo.NT == true)
                    {
                        categoriaNFNT.Checked = true;
                    }
                    else
                    {
                        categoriaNFNT.Checked = false;
                    }
                    txtLote.Value = pgo.lote;
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
                    txtOptSimples.Value = pgo.optSimples;
                    txtTipoNFConv.Value = pgo.tipoNFConv;
                    txtCodServicoMun.Value = pgo.codServicoMun;
                    txtTipoTribServico.Value = pgo.tpTribServico;

                }

                popCarimbo.Show();
            }
        }

        protected void showEditar(object sender, EventArgs e)
        {
            int idTipo = Convert.ToInt32(Request.QueryString["idtipo"]);
            
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {
                int idForm = Convert.ToInt32(row.Cells[0].Text);
                idEditarForm.Value = idForm.ToString();


                using (ProtocoloWebEntities context = new ProtocoloWebEntities())
                {
                    var pgo = context.Formulario.FirstOrDefault(p => p.idFormulario == idForm);

                    if (idTipo == 6)
                    {
                        divEditar.InnerHtml +=
                            "<a href='http://www.receita.fazenda.gov.br/PessoaJuridica/CNPJ/cnpjreva/Cnpjreva_Solicitacao2.asp?cnpj=" + pgo.CNPJ + "' target='_blank'>Receita Federal</a>";
                        divEditar.InnerHtml += "&nbsp;&nbsp;&nbsp;";
                        divEditar.InnerHtml +=
                            "<a href='https://www3.prefeitura.sp.gov.br/fdc/fdc_imp02_cgc.asp' target='_blank'>Prefeitura de SP</a>";
                        divEditar.InnerHtml += "&nbsp;&nbsp;&nbsp;";
                        divEditar.InnerHtml +=
                            "<a href='http://www8.receita.fazenda.gov.br/SIMPLESNACIONAL/aplicacoes.aspx?id=21' target='_blank'>Simples Nacional</a>";
                        divEditar.InnerHtml += "<br/>";

                        divEditar.InnerHtml +=
                            "<a href='https://www.nfe.fazenda.gov.br/PORTAL/principal.aspx' target='_blank'>Portal NF-e</a>";
                        divEditar.InnerHtml += "&nbsp;&nbsp;&nbsp;";
                        divEditar.InnerHtml += "<a href='http://www.sintegra.gov.br/' target='_blank'>Sintegra</a>";
                        divEditar.InnerHtml += "&nbsp;&nbsp;&nbsp;";
                        divEditar.InnerHtml +=
                            "<a href='https://www.jucesponline.sp.gov.br/ResultadoBusca.aspx?IDProduto=#' target='_blank'>Jucesp</a>";
                        divEditar.InnerHtml += "<br/>";

                        divEditar.InnerHtml +=
                            "<a href='https://www.fazenda.sp.gov.br/nfe/empresas/consulta/empresas.asp?nome=119012&Submit=Enviar' target='_blank'>Portal Sefaz</a>";
                        divEditar.InnerHtml += "&nbsp;&nbsp;&nbsp;";
                        divEditar.InnerHtml +=
                            "<a href='http://www.receita.fazenda.gov.br/aplicacoes/atcta/cpf/consultapublica.asp' target='_blank'>Receita Federal (CPF)</a>";
                        divEditar.InnerHtml += "&nbsp;&nbsp;&nbsp;";
                        divEditar.InnerHtml += "<a href='http://www.buscacep.correios.com.br/' target='_blank'>Correios</a>";
                    }

                    
                    txtAP_edit.Value = pgo.NumAP;
                    txtEmpresa_edit.Value = pgo.Empresa;
                    txtCnpj_edit.Value = pgo.CNPJ;
                    sltMatriz_edit.SelectedIndex =
                        sltMatriz_edit.Items.IndexOf(sltMatriz_edit.Items.FindByValue(pgo.Matriz));
                    txtNF_edit.Value = pgo.NumNF;
                    txtDataVencimento_edit.Value = Convert.ToDateTime(pgo.dtVencimento).ToString("dd/MM/yyyy");
                    txtValorNF_edit.Value = pgo.valorNF.ToString();
                    //txtValorNF_edit.Value = Convert.ToDouble(pgo.valorNF).ToString("###,###,##0.00");
                    txtDataEmissaoDoc_edit.Value = Convert.ToDateTime(pgo.dtEmissaoDOC).ToString("dd/MM/yyyy");
                    //sltCodJustificativa_edit.SelectedIndex = sltCodJustificativa_edit.Items.IndexOf(sltCodJustificativa_edit.Items.FindByValue(pgo.codJustificativa));
                    txtJustificativa_edit.Value = pgo.codJustificativa;
                    txtDepartamento_edit.Value = pgo.Departamento;
                    sltTipoDocumento_edit.SelectedIndex =
                        sltTipoDocumento_edit.Items.IndexOf(sltTipoDocumento_edit.Items.FindByValue(pgo.tipoDocumento));
                    txtDocumento_edit.Value = pgo.documento;

                    txtEmailResponsavel_edit.Value = pgo.emailResponsavel;

                    //Para não trazer dados inválidos nos campos de data
                    if (pgo.contratoInicio != null)
                    {

                        sltVinculo_edit.SelectedIndex =
                            sltVinculo_edit.Items.IndexOf(sltVinculo_edit.Items.FindByValue(pgo.vinculo));
                        sltTipoContrato_edit.SelectedIndex =
                            sltTipoContrato_edit.Items.IndexOf(sltTipoContrato_edit.Items.FindByValue(pgo.tipoContrato));
                        txtContratoInicio_edit.Value = Convert.ToDateTime(pgo.contratoInicio).ToString("dd/MM/yyyy");
                        txtContratoFim_edit.Value = Convert.ToDateTime(pgo.contratoFim).ToString("dd/MM/yyyy");
                        txtReceitaFederal_edit.Value = Convert.ToDateTime(pgo.receitaFederal).ToString("dd/MM/yyyy");
                        txtPrefeitura_edit.Value = Convert.ToDateTime(pgo.prefeitura).ToString("dd/MM/yyyy");
                        txtCNAE_edit.Value = Convert.ToDateTime(pgo.cnae).ToString("dd/MM/yyyy");
                        sltOptanteSimples_edit.SelectedIndex =
                            sltOptanteSimples_edit.Items.IndexOf(
                                sltOptanteSimples_edit.Items.FindByValue(pgo.optanteSimples.ToString()));
                        txtOptanteSimples_edit.Value = Convert.ToDateTime(pgo.optanteSimplesData).ToString("dd/MM/yyyy");
                        txtSintegra_edit.Value = Convert.ToDateTime(pgo.sintegra).ToString("dd/MM/yyyy");
                        txtJuntaComercial_edit.Value = Convert.ToDateTime(pgo.juntaComercial).ToString("dd/MM/yyyy");
                        
                    }

                }

                popEditar.Show();
            }
        }


        protected void ExcluirProtocolo(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {
                string idForm = row.Cells[0].Text;
                string vNumThisAp = row.Cells[5].Text;
                lblIdFormulario.Text = idForm;

                //Removendo dos painéis
                string strSQL = String.Empty;
                strSQL = "DELETE FROM Formulario WHERE idFormulario = " + idForm + ";";

                //Salvando o histórico
                strSQL += "INSERT INTO Historico (idFormulario,NumAP,login,data,acao,area) VALUES (" + idForm + ",'" + vNumThisAp + "','" +
                          Session["user"] + "',GETDATE(),'exclusao','" + Session["AreaDesc"].ToString() + "');";

                SqlConnection sqlConnection = new SqlConnection(cnString);
                SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                divJustificativa.InnerHtml = "<script>alert('Protocolo Excluído com sucesso.')</script>";
                carregaDados(Session["AreaDesc"].ToString());
            }
        }


        protected void ShowJustificativas(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {
                string idForm = row.Cells[0].Text;
                lblIdFormulario.Text = idForm;

                string strSQL = String.Empty;
                strSQL =
                    "SELECT idJustificativa,idFormulario,Justificativa,CONVERT(VARCHAR(10), dtJustificativa, 103) as dtJustificativa FROM Justificativa WHERE idFormulario = " +
                    idForm + " ORDER BY idJustificativa DESC";
                SqlConnection sqlConnection = new SqlConnection(cnString);
                SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                GridView2.DataSource = reader;
                GridView2.DataBind();

                popJustificativas.Show();
                sqlConnection.Close();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmd")
            {
                //string filename = e.CommandArgument.ToString();

                //try
                //{
                //    //System.Diagnostics.Process.Start(@"\\dioxido\Work\Controladoria_BF\SCANNER\PROTOCOLOWEB\" + filename +".pdf");
                //    System.Diagnostics.Process.Start(@"\\dioxido\Work\INFORMAT\" + filename + ".pdf");
                //}
                //catch (Exception ex)
                //{
                //    divJustificativa.InnerHtml = "<script>alert('Arquivo inexistente');</script>";
                //}
                //divJustificativa.InnerHtml = "<script>alert('"+ Environment.UserName+"');</script>";

                string filename = e.CommandArgument.ToString();
                //string caminho = @"\\dioxido\Work\INFORMAT\" + filename + ".pdf";
                string caminho = @"\\dioxido\Work\Controladoria_BF\SCANNER\PROTOCOLOWEB\" + filename + ".pdf";
                if (File.Exists(caminho))
                {
                    Response.ContentType = "Application/pdf";
                    //System.Diagnostics.Process.Start(caminho);
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + caminho);
                    Response.TransmitFile(caminho);
                    Response.End();
                }
                else
                {
                    divJustificativa.InnerHtml = "<script>alert('Arquivo inexistente');</script>";
                }

            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            //GridView1.DataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /*Verifies that the control is rendered */
        }

        public void btnExport_Click(object sender, EventArgs e)
        {
            // Create a new workbook and a sheet named "User Accounts"
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("User Accounts");

            //Header 
            var headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("ID");
            headerRow.CreateCell(1).SetCellValue("Empresa");
            headerRow.CreateCell(2).SetCellValue("CNPJ");
            headerRow.CreateCell(3).SetCellValue("Matriz");
            headerRow.CreateCell(4).SetCellValue("Nº NF");
            headerRow.CreateCell(5).SetCellValue("Nº AP");
            headerRow.CreateCell(6).SetCellValue("Nº Pedido");
            headerRow.CreateCell(7).SetCellValue("Vencimento");
            headerRow.CreateCell(8).SetCellValue("Data Emissão");
            headerRow.CreateCell(9).SetCellValue("Justificativa");
            headerRow.CreateCell(10).SetCellValue("Departamento");
            headerRow.CreateCell(11).SetCellValue("Tipo");
            headerRow.CreateCell(12).SetCellValue("Data Cadastro");

            //Conteudo
            int rowNum = 1;
            foreach (GridViewRow row in GridView1.Rows)
            {
                //create a new row
                Row newrow = sheet.CreateRow(rowNum);

                //Set the Values for Cells
                newrow.CreateCell(0).SetCellValue(GridView1.Rows[rowNum - 1].Cells[0].Text);
                newrow.CreateCell(1).SetCellValue(GridView1.Rows[rowNum - 1].Cells[1].Text);
                newrow.CreateCell(2).SetCellValue(GridView1.Rows[rowNum - 1].Cells[2].Text);
                newrow.CreateCell(3).SetCellValue(GridView1.Rows[rowNum - 1].Cells[3].Text);
                newrow.CreateCell(4).SetCellValue(GridView1.Rows[rowNum - 1].Cells[4].Text);
                newrow.CreateCell(5).SetCellValue(GridView1.Rows[rowNum - 1].Cells[5].Text);
                newrow.CreateCell(6).SetCellValue(GridView1.Rows[rowNum - 1].Cells[6].Text);
                newrow.CreateCell(7).SetCellValue(GridView1.Rows[rowNum - 1].Cells[7].Text);
                newrow.CreateCell(8).SetCellValue(GridView1.Rows[rowNum - 1].Cells[8].Text);
                newrow.CreateCell(9).SetCellValue(GridView1.Rows[rowNum - 1].Cells[9].Text);
                newrow.CreateCell(10).SetCellValue(GridView1.Rows[rowNum - 1].Cells[10].Text);
                newrow.CreateCell(11).SetCellValue(GridView1.Rows[rowNum - 1].Cells[11].Text);
                newrow.CreateCell(12).SetCellValue(GridView1.Rows[rowNum - 1].Cells[12].Text);
                rowNum++;
            }

            // Save the Excel spreadsheet to a file on the web server's file system
            using (var fileData = new FileStream(@"\\rrpvspa1403\Dados\ProtocoloWeb\excel\export" + DateTime.Now.ToString("yyyyMMdd") + "_" + Session["user"] + ".xls", FileMode.Create))
            {
                workbook.Write(fileData);
            }

            Session["nomeExcel"] = "export" + DateTime.Now.ToString("yyyyMMdd") + "_" + Session["user"];

            //Abrir nova Janela
            Response.Redirect("abreExcel.aspx");
            //divExportar.InnerHtml = "<script>window.location.href='abreExcel.aspx';</script>";
        }


    }
}