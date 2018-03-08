using System;
using UniversalGym.Data;
using UniversalGym.Email;
using UniversalGym.Slack;
using UniversalGym.Requests;
using UniversalGym.Responses;
using UniversalGym.Shared;
using User = UniversalGym.Data.User;

namespace UniversalGym.Users
{
    public class appSignUp
    {
        public static AllDataResponse appSignUpImplementation(AppSignUpRequest request)
        {

            if (String.IsNullOrWhiteSpace(request.email))
            {
                return new AllDataResponse
                {
                    message = "Please enter an email.",
                    status = 404,
                    success = false,
                };
            }

            if (String.IsNullOrWhiteSpace(request.firstName))
            {
                return new AllDataResponse
                {
                    message = "Please enter a first name.",
                    status = 404,
                    success = false,
                };
            }

            if (String.IsNullOrWhiteSpace(request.lastName))
            {
                return new AllDataResponse
                {
                    message = "Please enter a last name.",
                    status = 404,
                    success = false,
                };
            }



            if (MembershipHelper.emailAlreadyExists(request.email, Constants.UserRole))
            {

                return new AllDataResponse { message = "Email is already taken", status = 10, success = false };
            }
            else if (String.IsNullOrWhiteSpace(request.password) || request.password.Length < 6)
            {
                return new AllDataResponse { message = "Please enter a password at least 6 characters long", status = 10, success = false };
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
                    hasActiveSubscription = false,
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
                        + user.LastName;
                
                SlackHelper.sendUserSignupChannel(newuserbody);

            }

            EmailTemplateHelper.SendEmail("Welcome to Pedal", request.email, "http://pedal.com", request.firstName, "user_signup.html");

            return allDataHelper.CreateAllDataResponse(userId, true);
        }
    }
}