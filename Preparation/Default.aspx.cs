using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Preparation
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ltMessage.Text = this.Title;

            MyUserInfoBoxControl.UserName = "Nancy";
            MyUserInfoBoxControl.UserAge = 30;
            MyUserInfoBoxControl.UserCountry = "Canada";

            phUserInfoBox.Controls.Add(LoadControl("~/UserInfoBoxControl.ascx"));

            UserInfoBoxControl control = (UserInfoBoxControl)LoadControl("~/UserInfoBoxControl.ascx");
            control.UserName = "Car Wizard";
            control.UserAge = 49;
            control.UserCountry = "USA";
            phUserInfoBox.Controls.Add(control);
        }

        protected void btnRedirect_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/gmr_reg");
        }
    }
}