using System;
using System.Reflection;
using System.Web.Security;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;
using UniversalGym.Email;
using System.Linq;

namespace UniversalGym.Gym
{
    public class resetGymPasswordEmail
    {
        public static BasicResponse resetGymPasswordEmailImplementation(ResetPasswordEmailRequest request)
        {

            if (String.IsNullOrWhiteSpace(request.email))
            {
                return new BasicResponse { message = "Please enter an email.", status = 601, success = false };
            }

            using (var db = new UniversalGymEntities())
            {
                var gym = db.Gyms.SingleOrDefault(f => f.ContactInfo.Email == request.email && f.badData == false);
                if (gym == null)
                {
                    return new BasicResponse { message = "Gym not found.", status = 404, success = false };
                }

                var forgotPasswordBody = String.Format("Someone forgot a password :/ -- {0}", request.email);
                EmailNoTemplateHelper.SendEmail("Gym forgot password", "hello@pedal.com", forgotPasswordBody);

                gym.CurrentToken = Guid.NewGuid().ToString();
                db.SaveChanges();

                var link = Constants.PedalWebUrl + "gym.html#/resetPassword/" + gym.GymId + "/" + gym.CurrentToken;
                EmailTemplateHelper.SendEmail("Reset Password Link - Pedal", request.email, link, gym.OwnerName, "reset_password.html");

                return new BasicResponse { message = "Gym email sent successfully", status = 3, success = true };



            }
        }
    }
}