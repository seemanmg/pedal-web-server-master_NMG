using System;
using System.Linq;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;

namespace UniversalGym.Users
{
    public class updateProfile
    {
        public static BasicResponse updateProfileImplementation(UpdateProfileRequest request)
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

            if (String.IsNullOrWhiteSpace(request.firstName))
            {
                return new BasicResponse
                {
                    message = "Please enter a first name.",
                    status = 404,
                    success = false,
                };
            }

            if (String.IsNullOrWhiteSpace(request.lastName))
            {
                return new BasicResponse
                {
                    message = "Please enter a last name.",
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
                        message = "User not found.",
                        status = 404,
                        success = false,
                    };
                }


                user.FirstName = request.firstName;
                user.LastName = request.lastName;
                db.SaveChanges();
            }
            return new BasicResponse { message = "Profile was successfully changed.", status = 200, success = true };

        }
    }
}