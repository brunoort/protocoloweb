using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Controladoria
{
    public partial class abreExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string nmExcel = Session["nomeExcel"].ToString();
            string outputFile = @"\\rrpvspa1403\Dados\ProtocoloWeb\excel\" + nmExcel + ".xls";

            FileStream fs = new FileStream(outputFile, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            
            br.Close();
            Response.Clear();
            Response.AddHeader("Content-Disposition", "inline;filename=" + nmExcel + "");
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Cache-Control", "no-cache");
            Response.WriteFile(outputFile);
            fs.Close();
            Response.End();
        }
    }
}