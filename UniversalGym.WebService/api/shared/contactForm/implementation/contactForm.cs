using System;
using UniversalGym.Requests;
using UniversalGym.Responses;
using UniversalGym.Email;
using UniversalGym.Slack;

namespace UniversalGym.Shared
{
    public class contactForm
    {
        public static BasicResponse contactFormImplementation(ContactFormRequest request)
        {

            if (String.IsNullOrWhiteSpace(request.email))
            {
                return new BasicResponse { message = "Please enter an email", status = 200, success = false };
            }
            else if (String.IsNullOrWhiteSpace(request.name))
            {
                return new BasicResponse { message = "Please enter a name", status = 200, success = false };
            }
            else if (String.IsNullOrWhiteSpace(request.message))
            {
                return new BasicResponse { message = "Please enter a message.", status = 200, success = false };
            }

            var  contactFormBody = "Name: " 
                + request.name
                + Environment.NewLine
                + "Email: " 
                + request.email
                + Environment.NewLine
                + "Message: " 
                + request.message;

            SlackHelper.sendSupportChannel(contactFormBody);

            EmailNoTemplateHelper.SendEmail("Pedal Contact", "hello@pedal.com", contactFormBody);
            EmailTemplateHelper.SendEmail("Pedal Contact", request.email, Constants.PedalWebUrl, request.name, "contact_form.html", request.message);
            
            return new BasicResponse { message = "Success", status = 200, success = true };
        }
    }
}