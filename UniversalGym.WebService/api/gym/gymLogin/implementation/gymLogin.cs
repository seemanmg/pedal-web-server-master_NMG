using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;

namespace UniversalGym.Gym
{
    public class gymLogin
    {
        public static GymDataResponse gymLoginImplementation(UserDataRequest request)
        {
            using (var db = new UniversalGymEntities())
            {

                if (String.IsNullOrWhiteSpace(request.emailAddress))
                {
                    return new GymDataResponse
                    {
                        message = "Please enter an email.",
                        status = 404,
                        success = false,
                    };
                }

                if (String.IsNullOrWhiteSpace(request.password))
                {
                    return new GymDataResponse
                    {
                        message = "Please enter a password.",
                        status = 404,
                        success = false,
                    };
                }


                // admin access
                if (!request.password.Equals(Constants.MarketingToken)) {
  
                    if (!MembershipHelper.userExists(request.emailAddress, request.password, Constants.GymRole))
                    {
                        return new GymDataResponse { message = "Username or Password incorrect!!", status = 401, success = false }; ;
                    }
                }

                var user = MembershipHelper.getUser(request.emailAddress, Constants.GymRole);
                Guid guid = Guid.Parse(user.ProviderUserKey.ToString());
                var gym = db.Gyms.FirstOrDefault(cs => cs.GymGuid == guid);

                return gymDataHelper.CreateGymDataResponse(gym.GymId, true);
            }
        }
    }
}