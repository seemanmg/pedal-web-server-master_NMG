using System;
using System.Linq;
using System.Reflection;
using System.Web.Security;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;

namespace UniversalGym.Users
{
    public class userLogin
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static AllDataResponse userLoginImplementation(UserDataRequest request)
        {

            using (var db = new UniversalGymEntities())
            {
                if (String.IsNullOrWhiteSpace(request.emailAddress))
                {
                    return new AllDataResponse
                    {
                        message = "Please enter an email.",
                        status = 404,
                        success = false,
                    };
                }

                if (String.IsNullOrWhiteSpace(request.password))
                {
                    return new AllDataResponse
                    {
                        message = "Please enter a password.",
                        status = 404,
                        success = false,
                    };
                }

                if (!MembershipHelper.userExists(request.emailAddress, request.password, Constants.UserRole))
                {
                    return new AllDataResponse { message = "Username or Password incorrect", status = 5, success = false };
                }

                MembershipUser user = MembershipHelper.getUser(request.emailAddress, Constants.UserRole);
                Guid userGuid = Guid.Parse(user.ProviderUserKey.ToString());

                var user2 = (from cs in db.Users
                             where cs.UserGuid == userGuid
                             select cs).FirstOrDefault();

                return allDataHelper.CreateAllDataResponse(user2.UserId, true);
            }
        }
    }
}