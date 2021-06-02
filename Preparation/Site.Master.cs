using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Preparation
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public HtmlGenericControl BodyTag
        {
            get
            {
                return MasterBodyTag;
            }
            set
            {
                MasterBodyTag = value;
            }
        }
    }

}