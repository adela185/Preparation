using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Preparation
{
    public partial class EventUserControl : System.Web.UI.UserControl
    {
        private string pageTitle;
        public event EventHandler PageTitleUpdated;

        protected void btnUpdatePageTitle_Click(object sender, EventArgs e)
        {
            this.pageTitle = txtPageTitle.Text;
            //if (PageTitleUpdated != null)
                PageTitleUpdated?.Invoke(this, EventArgs.Empty);
        }

        public string PageTitle { get { return pageTitle; } }
    }
}