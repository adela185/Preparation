using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Preparation
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ViewState["NameOfUser"] != null)
                NameLabel.Text = ViewState["NameOfUser"].ToString();
            else
                NameLabel.Text = "Not set yet...";
        }

        protected void SubmitForm_Click(object sender, EventArgs e)
        {
            ViewState["NameOfUser"] = NameField.Text;
            NameLabel.Text = NameField.Text;
        }
    }
}