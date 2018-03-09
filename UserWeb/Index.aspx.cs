using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UniversalGym.Email;
using UniversalGym.Slack;

namespace UserWeb
{
	public partial class Index : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            
		}


        protected void btnsubmit_click(object sender, EventArgs e)
        {

            try
            {

                var contactFormBody = "Name: "
                + txtFirstName.Value + ' ' + txtLastName.Value
                + Environment.NewLine
                + "Email: "
                + txtEmail.Value
                + Environment.NewLine
                + "Phone number: "
                + txtPhone.Value
                + Environment.NewLine
                + "City: "
                + txtcity.Value
                + Environment.NewLine
                + "State: "
                + ddlstate.Value;

                

                //EmailNoTemplateHelper.SendEmail("Sign-up for exclusive updates", "jaiprakashk@newmediaguru.co.uk", contactFormBody); //for testing
                EmailNoTemplateHelper.SendEmail("Sign-up for exclusive updates", "hello@pedal.com", contactFormBody);
                //EmailTemplateHelper.SendEmail("Pedal Contact", request.email, Constants.PedalWebUrl, request.name, "contact_form.html", request.message);


            }
            catch (Exception ex)
            { 
            
              
            }

            txtFirstName.Value = "";
            txtLastName.Value = "";
            txtEmail.Value = "";
            txtPhone.Value = "";
            txtcity.Value = "";
            divsignupexclusive.Attributes.Add("style", "display:block");
        
        }


        protected void btnGetTouch_click(object sender, EventArgs e)
        {
            try
            {
                var contactFormBody = "Name: "
                    + name.Value
                    + Environment.NewLine
                    + "Email: "
                    + email.Value
                    + Environment.NewLine
                    + "Message: "
                    + message.Value;

                //SlackHelper.sendSupportChannel(contactFormBody);

                //EmailNoTemplateHelper.SendEmail("Pedal Contact", "jaiprakashk@newmediaguru.co.uk", contactFormBody);//for testing
                EmailNoTemplateHelper.SendEmail("Pedal Contact", "hello@pedal.com", contactFormBody);
                //EmailTemplateHelper.SendEmail("Pedal Contact", request.email, Constants.PedalWebUrl, request.name, "contact_form.html", request.message);
            }
            catch (Exception ex)
            { 
            
            }

            divgettouch.Attributes.Add("style", "display:block");
            name.Value = "";
            email.Value = "";
            message.Value = "";
        }



       

	}
}