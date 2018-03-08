using System;
using System.Linq;
using System.Web.Security;
using UniversalGym.Data;
using UniversalGym.Responses;

namespace UniversalGym.UpdatePassword
{
    public class updateUserPassword
    {
        public static BasicResponse updateUserPasswordImplementation(UpdatePasswordRequest request)
        {
            if (request == null || String.IsNullOrWhiteSpace(request.authToken) || request.accountId == null)
            {
                return new BasicResponse
                {
                    message = "User not found.",
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
                var user = db.Users.SingleOrDefault(a => a.CurrentToken == request.authToken && a.UserId == request.accountId);
                if (user == null)
                {
                    return new BasicResponse
                    {
                        message = "User not found",
                        status = 404,
                        success = false,
                    };
                }


                var user2 = MembershipHelper.getUser(user.Email, Constants.UserRole);
                var newPassword = user2.ResetPassword();
                user2.ChangePassword(newPassword, request.password);
                return new BasicResponse { message = "Password changed successfully.", status = 200, success = true };
            }
        }
    }
}