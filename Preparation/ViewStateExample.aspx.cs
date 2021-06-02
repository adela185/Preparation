using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Preparation
{
    public partial class ViewStateExample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
                ltPostBack.Text = "This is a post back and this page be \"sticky\".";
            else
                ltPostBack.Text = "Fill out this form. Don't worry about saving your info, we have view states!";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ltMessage.Text = "Valid Page";
            }
            else
            {
                valSummaryForm.Visible = true;
            }
        }

        protected void cv8chars_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value.Length != 8)
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    }
}