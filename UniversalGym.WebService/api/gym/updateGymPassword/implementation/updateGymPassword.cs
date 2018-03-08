using System;
using System.Linq;
using System.Web.Security;
using UniversalGym.Data;
using UniversalGym.Responses;

namespace UniversalGym.UpdatePassword
{
    public class updateGymPassword
    {
        public static BasicResponse updateGymPasswordImplementation(UpdatePasswordRequest request)
        {
            if (request == null || String.IsNullOrWhiteSpace(request.authToken) || request.accountId == null)
            {
                return new BasicResponse
                {
                    message = "Gym not found.",
                    status = 404,
                    success = false,
                };
            }

            if (String.IsNullOrWhiteSpace(request.password) || request.password.Length < 6 || String.IsNullOrEmpty(request.confirmPassword) || request.confirmPassword.Length < 6)
            {
               return new BasicResponse
                {
                    message = "Password must be greater than 5 characters",
                    status = 404,
                    success = false,
                };
            }

            if (!request.password.Equals(request.confirmPassword))
            {
                return new BasicResponse
                {
                    message = "Passwords do not match",
                    status = 404,
                    success = false,
                };
            }


            using (var db = new UniversalGymEntities())
            {
                var gym = db.Gyms.SingleOrDefault(a => a.CurrentToken == request.authToken && a.GymId == request.accountId);
                if (gym == null)
                {
                    return new BasicResponse
                    {
                        message = "Gym not found",
                        status = 404,
                        success = false,
                    };
                }

                var gym2 = MembershipHelper.getUser(gym.ContactInfo.Email, Constants.GymRole);
                var newPassword = gym2.ResetPassword();
                gym2.ChangePassword(newPassword, request.password);
                return new BasicResponse { message = "Password changed successfully.", status = 200, success = true };
            }
        }
    }
}