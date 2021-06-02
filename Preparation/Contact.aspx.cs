using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Preparation
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            divMessage.Attributes.Add("style", "color:blue");
            spanDouchebag.Attributes.Add("style", "color:red");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string message = string.Format("You said your name is {0} and that your age is {1}. You can be contacted at {2}. Your favorite color is {3}.",
                txtName.Text, txtAge.Text, txtEmail.Text, ddlColor.Text);
            ltMessage.Text = message;
        }
    }
}