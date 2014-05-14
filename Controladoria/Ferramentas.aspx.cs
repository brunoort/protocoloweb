using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProtocoloWeb;

namespace Controladoria
{
    public partial class Ferramentas : System.Web.UI.Page
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
    }
}