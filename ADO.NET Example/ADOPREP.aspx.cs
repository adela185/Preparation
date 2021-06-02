using ADO.NET_Class_Library_Ex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.ModelBinding;

namespace ADO.NET_Example
{
    public partial class ADOPREP : System.Web.UI.Page
    {
        ColorDBAccessLayer accessLayer = new ColorDBAccessLayer();
        Color color = new Color();

        protected void Page_Load(object sender, EventArgs e)
        {
            color.nm = txtColor.Text;
            color.hex = txtHex.Text;
            if (IsPostBack)
            {
                ltConfirm.Text = (string)Session["Msg"];
                ltConfirm.Visible = true;
            }
        }

        protected void btnColor_Click(object sender, EventArgs e)
        {
            if (ModelState.IsValid)
            {
                string r = accessLayer.AddColorRecord(color);
                Session["Msg"] = r;
                ltConfirm.Text = (string)Session["Msg"];
                ltConfirm.Visible = true;
            }
        }
    }
}