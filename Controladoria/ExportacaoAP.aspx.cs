using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using NPOI.POIFS.FileSystem;
using NPOI.HPSF;
using Newtonsoft.Json;
using ProtocoloWeb;
using System.Net.Http;
using Excel = Microsoft.Office.Interop.Excel;

namespace Controladoria
{
    public partial class ExportacaoAP : System.Web.UI.Page
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
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        public static string CreateRequest(string getAp)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getAp);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream webStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(webStream);
            string jsonResult = reader.ReadToEnd();

            reader.Close();
            response.Close();

            return jsonResult;
        }

        public static List<RelatorioAP> CreateObjectList(string json)
        {

            json = json.Replace("[", "");
            json = json.Replace("]", "");
            json = json.Replace("},{", "}#{");
            string[] values = json.Split('#');
            List<RelatorioAP> lstObjects = new List<RelatorioAP>();

            foreach (var value in values)
            {
                lstObjects.Add(CreateObject(value));
            }

            return lstObjects;
        }

        public static RelatorioAP CreateObject(string value)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Deserialize<RelatorioAP>(value);
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (txtPeriodoInicial.Value != null && txtPeriodoFinal.Value != null)
            {
                string dtInicial = txtPeriodoInicial.Value;
                string dtFinal = txtPeriodoFinal.Value;
                
                string url = Comum.BuscarChaveWebConfig("UrlWebAPI") + "usuario=" + usuario + "&aplicacao=" + aplicacao +
                             "&token=" + token + "&dtInicial=" + dtInicial.Replace(@"/", "-") + "&dtFinal=" + dtFinal.Replace(@"/", "-");

                string retorno = CreateRequest(url);
                List<RelatorioAP> relatorios = CreateObjectList(retorno);

                GridView1.DataSource = relatorios;
                GridView1.DataBind();

                if (relatorios.Any())
                {
                    lblResposta.Text = "";
                    ExportarAP();
                }
                else
                {
                    lblResposta.Text = "Não existe nenhuma informação para o período informado.";
                }

                //HttpClient client;
                //client = new HttpClient();
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                //HttpResponseMessage response = client.GetAsync("http://request.rederecord.com.br/ap/relatorio?dtInicial=" + dtInicial.Replace(@"/", "-") + "&dtFinal=" + dtFinal.Replace(@"/", "-")).Result;

                ////se retornar com sucesso busca os dados
                //if (response.IsSuccessStatusCode)
                //{
                //    //Pegando os dados do Rest e armazenando na variável usuários
                //    var relatorios = response.Content.ReadAsAsync<IEnumerable<RelatorioAP>>().Result;
                //    //preenchendo a lista com os dados retornados da variável
                //    GridView1.DataSource = relatorios;
                //    GridView1.DataBind();

                //    if (relatorios.Any())
                //    {
                //        lblResposta.Text = "";
                //        ExportarAP();
                //    }
                //    else
                //    {
                //        lblResposta.Text = "Não existe nenhuma informação para o período informado.";
                //    }
                //}
            }
        }

        public string UrlCorrection(string text)
        {
            StringBuilder correctedText = new StringBuilder(text);

            return correctedText.Replace("&#231;", "ç")
                                .Replace("&#227;", "ã")
                                .Replace("&#192;", "À")
                                .Replace("&#193;", "Á")
                                .Replace("&#194;", "Â")
                                .Replace("&#195;", "Ã")
                                .Replace("&#199;", "Ç")
                                .Replace("&#200;", "È")
                                .Replace("&#201;", "É")
                                .Replace("&#202;", "Ê")
                                .Replace("&#204;", "Ì")
                                .Replace("&#205;", "Í")
                                .Replace("&#206;", "Î")
                                .Replace("&#210;", "Ò")
                                .Replace("&#211;", "Ó")
                                .Replace("&#212;", "Ô")
                                .Replace("&#213;", "Õ")
                                .Replace("&#217;", "Ù")
                                .Replace("&#218;", "Ú")
                                .Replace("&#219;", "Û")
                                .Replace("&#224;", "à")
                                .Replace("&#225;", "á")
                                .Replace("&#226;", "â")
                                .Replace("&#227;", "ã")
                                .Replace("&#231;", "ç")
                                .Replace("&#232;", "è")
                                .Replace("&#233;", "é")
                                .Replace("&#234;", "ê")
                                .Replace("&#236;", "ì")
                                .Replace("&#237;", "í")
                                .Replace("&#242;", "ò")
                                .Replace("&#243;", "ó")
                                .Replace("&#244;", "ô")
                                .Replace("&#245;", "õ")
                                .Replace("&#249;", "ù")
                                .Replace("&#250;", "ú")
                                .ToString();
        }



        public void ExportarAP()
        {
            // Create a new workbook and a sheet named "User Accounts"

            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Relatórios AP");

            //Header 
            var headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("COD_AP");
            headerRow.CreateCell(1).SetCellValue("STATUS");
            headerRow.CreateCell(2).SetCellValue("PEDIDO SAP");
            headerRow.CreateCell(3).SetCellValue("DATA EMIS NF");
            headerRow.CreateCell(4).SetCellValue("MÊS COMPETÊNCIA");
            headerRow.CreateCell(5).SetCellValue("ANO COMPETÊNCIA");
            headerRow.CreateCell(6).SetCellValue("DATA AP");
            headerRow.CreateCell(7).SetCellValue("FORN");
            headerRow.CreateCell(8).SetCellValue("CNPJ");
            headerRow.CreateCell(9).SetCellValue("CONTA");
            headerRow.CreateCell(10).SetCellValue("DESC. CONTA");
            headerRow.CreateCell(11).SetCellValue("DESC. SERV");
            headerRow.CreateCell(12).SetCellValue("TIPO PAGTO.");
            headerRow.CreateCell(13).SetCellValue("CONTRATO");
            headerRow.CreateCell(14).SetCellValue("OBSERVACAO");
            headerRow.CreateCell(15).SetCellValue("VALOR");
            headerRow.CreateCell(16).SetCellValue("NUM CONTRATO");
            headerRow.CreateCell(17).SetCellValue("DATA_VIGENCIA");
            //Conteudo
            int rowNum = 1;
            foreach (GridViewRow row in GridView1.Rows)
            {
                //create a new row
                Row newrow = sheet.CreateRow(rowNum);

                //Set the Values for Cells
                newrow.CreateCell(0).SetCellValue(GridView1.Rows[rowNum - 1].Cells[0].Text);
                newrow.CreateCell(1).SetCellValue(UrlCorrection(GridView1.Rows[rowNum - 1].Cells[2].Text));
                newrow.CreateCell(2).SetCellValue(GridView1.Rows[rowNum - 1].Cells[3].Text);
                newrow.CreateCell(3).SetCellValue(GridView1.Rows[rowNum - 1].Cells[4].Text.Substring(0, 10));
                newrow.CreateCell(4).SetCellValue(GridView1.Rows[rowNum - 1].Cells[5].Text.Substring(0, 1));
                newrow.CreateCell(5).SetCellValue(GridView1.Rows[rowNum - 1].Cells[6].Text.Substring(0, 4));
                newrow.CreateCell(6).SetCellValue(GridView1.Rows[rowNum - 1].Cells[7].Text.Substring(0, 10));
                newrow.CreateCell(7).SetCellValue(UrlCorrection(GridView1.Rows[rowNum - 1].Cells[8].Text));
                newrow.CreateCell(8).SetCellValue(GridView1.Rows[rowNum - 1].Cells[9].Text);
                newrow.CreateCell(9).SetCellValue(GridView1.Rows[rowNum - 1].Cells[10].Text);
                newrow.CreateCell(10).SetCellValue(UrlCorrection(GridView1.Rows[rowNum - 1].Cells[11].Text));
                newrow.CreateCell(11).SetCellValue(UrlCorrection(GridView1.Rows[rowNum - 1].Cells[12].Text));
                newrow.CreateCell(12).SetCellValue(UrlCorrection(GridView1.Rows[rowNum - 1].Cells[13].Text));
                newrow.CreateCell(13).SetCellValue(GridView1.Rows[rowNum - 1].Cells[14].Text);
                newrow.CreateCell(14).SetCellValue(UrlCorrection(GridView1.Rows[rowNum - 1].Cells[15].Text));
                newrow.CreateCell(15).SetCellValue(GridView1.Rows[rowNum - 1].Cells[16].Text);
                newrow.CreateCell(16).SetCellValue(GridView1.Rows[rowNum - 1].Cells[17].Text);
                newrow.CreateCell(17).SetCellValue(GridView1.Rows[rowNum - 1].Cells[18].Text);
                rowNum++;
            }

            // Save the Excel spreadsheet to a file on the web server's file system
            using (var fileData = new FileStream(@"\\rrpvspa1403\Dados\ProtocoloWeb\excel\export" + DateTime.Now.ToString("yyyyMMdd") + "_" + Session["user"] + ".xls", FileMode.Create))
            {
                workbook.Write(fileData);
            }

            Session["nomeExcel"] = "export" + DateTime.Now.ToString("yyyyMMdd") + "_" + Session["user"];

            //Abrir nova Janela
            dados.InnerHtml = "<script>window.location.href='abreExcel.aspx';</script>";
        }


        public class RelatorioAP
        {
            public string Cod_Auto_Pagto { get; set; }
            public string Solicitante { get; set; }
            public string Status { get; set; }
            public string PedidoSAP { get; set; }
            public Nullable<System.DateTime> DataEmissaoNF { get; set; }
            public Nullable<System.Decimal> MesCompetencia { get; set; }
            public Nullable<System.Decimal> AnoCompetencia { get; set; }
            public Nullable<System.DateTime> DataAP { get; set; }
            public string Fornecedor { get; set; }
            public string CNPJ { get; set; }
            public string Conta { get; set; }
            public string DescConta { get; set; }
            public string DescServ { get; set; }
            public string TipoPagto { get; set; }
            public string Contrato { get; set; }
            public string Observacao { get; set; }
            public string Valor { get; set; }
            public string NumContrato { get; set; }
            public string DataVigencia { get; set; }
        }
    }
}