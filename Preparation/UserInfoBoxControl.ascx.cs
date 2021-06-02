using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Preparation
{
    public partial class UserInfoBoxControl : System.Web.UI.UserControl
    {
        private string userName = "Default";
        private int userAge = -1;
        private string userCountry = "Default";

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public int UserAge
        {
            get { return userAge; }
            set { userAge = value; }
        }
        public string UserCountry
        {
            get { return userCountry; }
            set { userCountry = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}