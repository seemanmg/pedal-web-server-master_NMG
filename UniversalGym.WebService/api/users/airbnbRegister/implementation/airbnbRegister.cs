using System;
using System.Linq;
using System.Web.Security;
using UniversalGym.Data;
using UniversalGym.Responses;
using UniversalGym.Requests;
using UniversalGym.Shared;
using UniversalGym.Slack;
using UniversalGym.Email;

namespace UniversalGym.Users
{
    public class AirbnbRegister
    {
        public static BasicResponse airbnbRegisterImplementation(AirbnbRegisterRequest request)
        {

            if (String.IsNullOrWhiteSpace(request.email))
            {
                return new BasicResponse
                {
                    message = "Please enter an email.",
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


            if (MembershipHelper.emailAlreadyExists(request.email, Constants.UserRole))
            {

                return new BasicResponse { message = "Email is already taken", status = 10, success = false };
            }
            else if (String.IsNullOrWhiteSpace(request.password) || request.password.Length < 6)
            {
                return new BasicResponse { message = "Please enter a password at least 6 characters long", status = 10, success = false };
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

            var newUserGuid = MembershipHelper.createMembership(request.email, request.password, Constants.UserRole);
            var userId = 0;
            using (var db = new UniversalGymEntities())
            {
                var newCode = UserCode.GenerateDistinctCode(db);
                var user = new User
                {
                    FirstName = request.firstName,
                    LastName = request.lastName,
                    ReferalUrl = newCode,
                    UserGuid = newUserGuid,
                    Email = request.email.ToLower(),
                    hasActiveSubscription = true,
                    hasCreditCard = false,
                    joinDate = DateTime.Now,
                    Credits = 0,
                };
                db.Users.Add(user);
                db.SaveChanges();
                userId = user.UserId;



                var newuserbody = "User Signup"
                        + Environment.NewLine
                        + "Email: "
                        + user.Email
                        + Environment.NewLine
                        + "Name: "
                        + user.FirstName
                        + user.LastName
                        + Environment.NewLine
                        + "Airbnb Host: https://www.airbnb.com/users/show/"
                        + request.airbnbHostId;



                SlackHelper.sendUserSignupChannel(newuserbody);

            }

            EmailTemplateHelper.SendEmail("Welcome to Pedal", request.email, "http://pedal.com", request.firstName, "user_signup.html");

            return new BasicResponse
            {
                message = "Account created! Just download the Pedal Fitness app and login!",
                status = 200,
                success = true,
            };
        }
    }
}