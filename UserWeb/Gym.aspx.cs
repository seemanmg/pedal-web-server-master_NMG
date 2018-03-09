using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversalGym.Email;

namespace UserWeb
{
    public partial class Gym : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnsubmit_click(object sender, EventArgs e)
        {

            try
            {

                var contactFormBody = "Gym/Studio Name: "
                + gymName.Value
                + Environment.NewLine
                + "Contact Name: "
                + contactName.Value
                + Environment.NewLine
                + "Contact Email: "
                + contactEmail.Value
                + Environment.NewLine
                + "Gym Street Address: "
                + address.Value
                + Environment.NewLine
                + "State: "
                + ddlstate.Value
                + Environment.NewLine
                + "Zip Code: "
                + postalcode.Value
                + Environment.NewLine
                + "gym Phone: "
                + gymPhone.Value
                 + Environment.NewLine
                + "Gym Website: "
                + website.Value
                  + Environment.NewLine
                + "Day Pass Price: "
                + daypassprice.Value;



                //EmailNoTemplateHelper.SendEmail("Pedal Partner Registration", "jaiprakashk@newmediaguru.co.uk", contactFormBody); //for testing
                EmailNoTemplateHelper.SendEmail("Sign-up for exclusive updates", "hello@pedal.com", contactFormBody);
                //EmailTemplateHelper.SendEmail("Pedal Contact", request.email, Constants.PedalWebUrl, request.name, "contact_form.html", request.message);


            }
            catch (Exception ex)
            {


            }

            gymName.Value = "";
            contactName.Value = "";
            contactEmail.Value = "";
            address.Value = "";
            postalcode.Value = "";
            gymPhone.Value = "";
            website.Value = "";
            daypassprice.Value = "";
            divpartnermessage.Attributes.Add("style", "display:block");
        
        
        }

    }
}