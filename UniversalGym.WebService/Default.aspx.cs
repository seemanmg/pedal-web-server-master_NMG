using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using UniversalGym.Data;

namespace UniversalGym.API
{
    public partial class _Default : System.Web.UI.Page
    {

        
        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Redirect("http://pedal.com");

        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
