using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Preparation.Controllers
{
    public class ColorController : ApiController
    {
        public void Get()
        {
            Redirect("https://localhost:44368/api/Color");
        }
    }
}
