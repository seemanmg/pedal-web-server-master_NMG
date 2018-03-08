using System;
using System.Linq;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;

namespace UniversalGym.Gym
{
    public class getGymInfo
    {
        public static GymDataResponse getGymInfoImplementation(BaseRequest request)
        {

            if (request == null || String.IsNullOrWhiteSpace(request.authToken) || request.accountId == null)
            {
                return new GymDataResponse
                {
                    message = "Gym not found.",
                    status = 404,
                    success = false,
                };
            }

            using (var db = new UniversalGymEntities())
            {

                var gym = db.Gyms.SingleOrDefault(a => a.CurrentToken == request.authToken && a.GymId == request.accountId);
                if (gym == null)
                {
                    return new GymDataResponse
                    {
                        message = "Gym not found.",
                        status = 404,
                        success = false,
                    };
                }


                return gymDataHelper.CreateGymDataResponse(gym.GymId, false);
            }
        }
    }
}