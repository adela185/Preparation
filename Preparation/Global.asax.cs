using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Preparation
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //public override string GetVaryByCustomString(HttpContext context, string custom)
        //{
        //    if (custom == "Browser")
        //    {
        //        return context.Request.Browser.Browser;
        //    }
        //    return String.Empty;
        //}
    }
}