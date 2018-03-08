using System;
using System.Linq;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;

namespace UniversalGym.Gym
{
    public class contractFromGymId
    {
        public static ContractResponse contractFromGymIdImplementation(BaseRequest request)
        {
            using (var db = new UniversalGymEntities())
            {
                if (request == null || String.IsNullOrWhiteSpace(request.authToken) || request.accountId == null)
                {
                    return new ContractResponse { status = 401, success = false, message = "Gym not found" };
                }

                var gym = db.Gyms.SingleOrDefault(a => a.CurrentToken == request.authToken && a.GymId == request.accountId);
                if (gym == null)
                {
                    return new ContractResponse
                    {
                        message = "Gym not found.",
                        status = 404,
                        success = false,
                    };
                }

                var price = gym.CreditDollarValue;
                var date = gym.ApplicationDate.ToShortDateString();
                var gymName = gym.GymName;
                return new ContractResponse { gymName = gymName, price = price, date = date, status = 200, success = true, message = "Contract retreived" };

            }

        }
    }
}