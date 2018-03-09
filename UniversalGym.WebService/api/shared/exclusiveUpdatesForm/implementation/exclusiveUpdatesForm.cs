using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversalGym.Email;
using UniversalGym.Requests;
using UniversalGym.Responses;
using UniversalGym.Slack;

namespace UniversalGym.Shared
{
    public class exclusiveUpdatesForm
    {
        public static BasicResponse exclusiveUpdatesImplementation(exclusiveUpdatesFormRequest request)
        {

            if (String.IsNullOrWhiteSpace(request.emailaddress))
            {
                return new BasicResponse { message = "Please enter an email", status = 200, success = false };
            }
            else if (String.IsNullOrWhiteSpace(request.firstname))
            {
                return new BasicResponse { message = "Please enter a first name", status = 200, success = false };
            }
            else if (String.IsNullOrWhiteSpace(request.lastname))
            {
                return new BasicResponse { message = "Please enter a last name.", status = 200, success = false };
            }
            else if (String.IsNullOrWhiteSpace(request.phonenumber))
            {
                return new BasicResponse { message = "Please enter a phone number.", status = 200, success = false };
            }
            else if (String.IsNullOrWhiteSpace(request.city))
            {
                return new BasicResponse { message = "Please enter a city.", status = 200, success = false };
            }
            else if (String.IsNullOrWhiteSpace(request.state))
            {
                return new BasicResponse { message = "Please select state.", status = 200, success = false };
            }

            var contactFormBody = "Name: "
                + request.firstname +' '+ request.lastname
                + Environment.NewLine
                + "Email: "
                + request.emailaddress
                + Environment.NewLine
                + "Phone number: "
                + request.phonenumber
                +Environment.NewLine
                +"City: "
                + request.city
                +Environment.NewLine
                + "State: "
                + request.state;

            SlackHelper.sendSupportChannel(contactFormBody);

            EmailNoTemplateHelper.SendEmail("Sign-up for exclusive updates", "jaiprakashk@newmediaguru.co.uk", contactFormBody);
            //EmailNoTemplateHelper.SendEmail("Sign-up for exclusive updates", "hello@pedal.com", contactFormBody);
            //EmailTemplateHelper.SendEmail("Pedal Contact", request.email, Constants.PedalWebUrl, request.name, "contact_form.html", request.message);

            return new BasicResponse { message = "Success", status = 200, success = true };
        }

    }
}