using System;
using UniversalGym;

namespace UserWeb
{
    public partial class api_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ApiUrl.Text = Constants.PedalAPIUrl;
            GoogleApiKey.Text = Constants.GoogleApiKey;
            StripeKey.Text = Constants.StripePublishableKey;
            WebUrl.Text = Constants.PedalWebUrl;
            Response.ContentType = "application/javascript";
        }
    }
}