using System;
using System.Linq;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;
using UniversalGym.StripeHelper;

namespace UniversalGym.Gym
{
    public class updateBankInfo
    {
        public static BasicResponse updateBankInfoImplementation(BankAccountInfoRequest request)
        {
            //validate incoming data
            if (String.IsNullOrWhiteSpace(request.stripeToken))
            {
                return new BasicResponse { message = "Bank Routing Number or Account Number was not valid.", status = 601, success = false };
            }

            if (request == null || String.IsNullOrWhiteSpace(request.authToken) || request.accountId == null)
            {
                return new BasicResponse
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
                    return new BasicResponse
                    {
                        message = "Gym not found.",
                        status = 404,
                        success = false,
                    };
                }

                var recipId = new StripeAch().CreateRecipient(gym.ContactInfo.Email, gym.GymName, "account for " + gym.GymName, request.stripeToken);
                gym.StripeUrl = recipId;
                db.SaveChanges();
                return new BasicResponse { status = 200, success = true, message = "Success!" };
            }
        }
    }
}