using System;
using System.Reflection;
using System.Web.Security;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;
using UniversalGym.Email;
using System.Linq;

namespace UniversalGym.Users
{
    public class resetUserPasswordEmail
    {
        public static BasicResponse resetUserPasswordEmailImplementation(ResetPasswordEmailRequest request)
        {

            if (String.IsNullOrWhiteSpace(request.email))
            {
                return new BasicResponse { message = "Please enter an email.", status = 601, success = false };
            }

            using (var db = new UniversalGymEntities())
            {
                var user = db.Users.SingleOrDefault(f => f.Email == request.email);
                if (user == null)
                {
                    return new BasicResponse { message = "User not found.", status = 404, success = false };
                }

                var forgotPasswordBody = String.Format("Someone forgot a password :/ -- {0}", request.email);
                EmailNoTemplateHelper.SendEmail("User forgot password", "hello@pedal.com", forgotPasswordBody);

                user.CurrentToken = Guid.NewGuid().ToString();
                db.SaveChanges();

                var link = Constants.PedalWebUrl + "user.html#/resetPassword/" + user.UserId + "/" + user.CurrentToken; ;
                EmailTemplateHelper.SendEmail("Reset Password Link - Pedal", request.email, link, user.FirstName, "reset_password.html");

                return new BasicResponse { message = "User email sent successfully", status = 3, success = true };



            }
        }
    }
}