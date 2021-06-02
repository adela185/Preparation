using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Preparation
{
    public partial class Cookies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.Cookies["BackgroundColor"] != null)
            {

                ColorSelector.SelectedValue = Request.Cookies["BackgroundColor"].Value;
                Master.BodyTag.Style["background-color"] = ColorSelector.SelectedValue;
            }
        }

        protected void ColorSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            Master.BodyTag.Style["background-color"] = ColorSelector.SelectedValue;
            HttpCookie cookie = new HttpCookie("BackgroundColor");
            cookie.Value = ColorSelector.SelectedValue;
            cookie.Expires = DateTime.Now.AddHours(1);
            Response.SetCookie(cookie);
            //This works same but with sessions, ending in minutes by default other wise stated in config
            //Session["BackgroundColor"] = ColorSelector.SelectedValue;

        }


    }
}