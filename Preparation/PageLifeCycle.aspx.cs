using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Preparation
{
    public partial class PageLifeCycle : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            bool isPostBack = Page.IsPostBack;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            lblinit.Text = "Page Initialization";
        }

        protected void Page_InitComplete(object sender, EventArgs e)
        {
        
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblPageLoad.Text = "Page Load Text";

            if (Page.IsPostBack)
                lblPostBack.Text = "Page Posted Back";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblButtoneEvent.Text = "Button Clicked";
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {

        }

        protected void Page_Unload(object sender, EventArgs e)
        {

        }
    }
}