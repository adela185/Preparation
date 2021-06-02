using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Preparation
{
    public partial class Localization : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            Page.Culture = "en-GB";
            Page.UICulture = "en-GB";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            lblHello.Text = Resources.MyGlobalResources.HelloString;
            lblHello.Text = GetLocalResourceObject("lblHelloWorld.Text").ToString();
            lblHello.Text = GetGlobalResourceObject("MyGlobalResources", "HelloString").ToString();
            Response.Write(Page.Culture);
        }
    }
}