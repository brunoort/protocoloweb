using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ProtocoloWeb
{
    public partial class exibeResultado : System.Web.UI.Page
    {
        public string cnString = ConfigurationManager.ConnectionStrings["ProtocoloWebConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                var def = new _Default();
                def.verificaUsuario();

                string vInicial = (string)Session["vInicial"];
                string vFinal = (string)Session["vFinal"];
                string vNf = (string)Session["vNf"];
                string vAp = (string)Session["vAp"];
                string vStatus = (string)Session["vStatus"];
                string vDocumento = (string)Session["vDocumento"];
                string vCNPJ = (string)Session["vCNPJ"];

                carregaDados(vInicial, vFinal, vNf, vAp, vStatus, vDocumento, vCNPJ);

            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private void carregaDados(string pInicial, string pFinal, string pNf, string pAp, string pStatus, string pDocumento, string pCNPJ)
        {
            GridView1.DataSource = "";
            GridView1.DataBind();

            string cnString = ConfigurationManager.ConnectionStrings["ProtocoloWebConnectionString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(cnString);

            string strSQL = "";

            string vWhere = "";
            string vPeriodo = "";
            string vNumNf = "";
            string vNumAp = "";
            string vStatus = "";
            string vDocumento = "";
            string vCNPJ = "";

            int vFiltro = 0;

            if (pInicial != "" || pFinal != "" || pNf != "" || pAp != "" || pStatus != "" || pDocumento != "")
            // Todas variáveis de busca
            {

                vWhere = " WHERE ";

                if (pInicial != "" && pFinal != "")
                {
                    string periodoInicial = Convert.ToDateTime(pInicial).ToString("yyyy-MM-dd");
                    string periodoFinal = Convert.ToDateTime(pFinal).ToString("yyyy-MM-dd");
                    //string periodoInicial = Convert.ToDateTime(pInicial).ToString("yyyy-MM-dd HH:mm:ss");
                    //string periodoFinal = Convert.ToDateTime(pFinal).ToString("yyyy-MM-dd HH:mm:ss");

                    vPeriodo = "dtVencimento BETWEEN '" + periodoInicial + "' and '" + periodoFinal + "'";
                    vFiltro = 1;
                }

                if (pInicial != "" && pFinal == "")
                {
                    vPeriodo = "dtVencimento = '" + pInicial + "'";
                    vFiltro = 1;
                }


                //NF
                if (pNf != "")
                {
                    if (vFiltro == 0)
                    {
                        vNumNf = " NumNF = '" + pNf + "'";
                        vFiltro = 1;
                    }
                    else
                    {
                        vNumNf = " AND NumNF = '" + pNf + "'";
                    }
                }

                //AP
                if (pAp != "")
                {
                    if (vFiltro == 0)
                    {
                        vNumAp = " NumAP = '" + pAp + "'";
                        vFiltro = 1;
                    }
                    else
                    {
                        vNumAp = " AND NumAP = '" + pAp + "'";
                    }
                }

                //Documento
                if (pDocumento != "")
                {
                    if (vFiltro == 0)
                    {
                        vDocumento = " tipoDocumento = '" + pDocumento + "'";
                        vFiltro = 1;
                    }
                    else
                    {
                        vDocumento = " AND tipoDocumento = '" + pDocumento + "'";
                    }
                }

                //CNPJ
                if (pCNPJ != "")
                {
                    if (vFiltro == 0)
                    {
                        vCNPJ = " CNPJ = '" + pCNPJ + "'";
                        vFiltro = 1;
                    }
                    else
                    {
                        vCNPJ = " AND CNPJ = '" + pCNPJ + "'";
                    }
                }


                //Status
                if (pStatus != "")
                {
                    if (vFiltro == 0)
                    {
                        if (pStatus == "NULL")
                        {
                            vStatus = " concluido is NULL OR concluido = 'False' OR concluido = 'True' ";
                            vFiltro = 1;
                        }
                        else
                        {
                            vStatus = " concluido = '" + pStatus + "'";
                        }

                    }
                    else
                    {
                        if (pStatus == "NULL")
                        {
                            vStatus = " AND concluido is NULL OR concluido = 'False' ";
                        }
                        else
                        {
                            vStatus = " AND concluido = '" + pStatus + "'";
                        }

                    }
                }
            }

            strSQL =
                "SELECT idFormulario,Empresa,CNPJ, Matriz, NumNF, NumAP,NumPedido, CONVERT(VARCHAR(10), dtVencimento, 103) as dtVencimento, valorNF, CONVERT(VARCHAR(10), dtEmissaoDOC, 103) as dtEmissaoDOC , codJustificativa, Departamento, tipoDocumento, documento, statusControladoria, loginSolicitante, emailSolicitante, CONVERT(VARCHAR(10), dataCadastro, 103) as dataCadastro from Formulario " +
                vWhere + vPeriodo + vNumNf + vNumAp + vDocumento + vCNPJ + vStatus + " ORDER BY idFormulario ASC";

            SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();


            GridView1.DataSource = reader;
            GridView1.DataBind();

            sqlConnection.Close();

            if (GridView1.Rows.Count > 0)
            {
                GridView1.UseAccessibleHeader = true;
                GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
                TableCellCollection cells = GridView1.HeaderRow.Cells;
                cells[0].Attributes.Add("data-class", "expand");
                cells[1].Attributes.Add("data-hide", "phone,tablet");
                cells[2].Attributes.Add("data-hide", "phone,tablet");
                cells[3].Attributes.Add("data-hide", "phone,tablet");

                cells[5].Attributes.Add("data-hide", "phone,tablet");
                cells[6].Attributes.Add("data-hide", "phone,tablet");
                cells[7].Attributes.Add("data-hide", "phone,tablet");
                cells[8].Attributes.Add("data-hide", "phone,tablet");
                cells[9].Attributes.Add("data-hide", "phone,tablet");
                //cells[10].Attributes.Add("data-hide", "phone,tablet");

                lblResposta.Text = string.Empty;
                ajustaTabela();
            }
            else
            {
                lblResposta.Text = "Não existe nenhum dado para os filtros selecionados.";
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmd")
            {
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
                    loadGridView.InnerHtml = "<script>alert('Arquivo inexistente');</script>";
                }

            }


            //if (e.CommandName == "cmd2")
            //{

            //    string idForm = e.CommandArgument.ToString();

            //    lblIdFormulario.Text = idForm;

            //    string strSQL = "";
            //    strSQL = "SELECT idJustificativa,idFormulario,Justificativa,CONVERT(VARCHAR(10), dtJustificativa, 103) as dtJustificativa FROM Justificativa WHERE idFormulario = " + idForm + " ORDER BY idFormulario DESC";
            //    SqlConnection sqlConnection = new SqlConnection(cnString);
            //    SqlCommand sqlCommand = new SqlCommand(strSQL, sqlConnection);
            //    sqlConnection.Open();
            //    SqlDataReader reader = sqlCommand.ExecuteReader();

            //    GridView2.DataSource = reader;
            //    GridView2.DataBind();

            //    divJustificativa.InnerHtml = "<script>showJustificativasPopup();</script>";
            //    divJustificativa.InnerHtml = "<script>return false;</script>";

            //}
        }

        public void ajustaTabela()
        {
            if (IsPostBack)
            {
                //Ajustes para Mobile
                bool mobile = isMobileBrowser();
                if (mobile)
                {
                    loadGridView.InnerHtml = "<script>dimensaoGrid();</script>";
                }
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

        private static string[] list = { "iphone", "nokia", "blackberry", "lg-", 
                                        "mot-", "palm", "samsung", "sec",
                                        "android", "and", "sonyericsson", "windows ce", "opera mini",
                                        "avantgo", "htc"
                                      };

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

        protected void showDetails(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {
                string idForm = row.Cells[0].Text;
                lblIdFormulario.Text = idForm;

                //Capturando a 
                string stringSQL = String.Empty;
                stringSQL = "SELECT login, CONVERT(VARCHAR(10), data, 103) as dataAcao, acao, area FROM Historico WHERE idFormulario = " + idForm + " ORDER BY idHistorico";
                SqlConnection sqlConnection = new SqlConnection(cnString);
                SqlCommand command = new SqlCommand(stringSQL, sqlConnection);
                sqlConnection.Open();
                SqlDataReader rd = command.ExecuteReader();

                GridViewDetalhe.DataSource = rd;
                GridViewDetalhe.DataBind();

                popDetails.Show();

                rd.Close();
                sqlConnection.Close();
            }
        }
    }
}
