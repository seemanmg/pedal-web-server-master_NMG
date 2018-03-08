using System;
using System.Linq;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;
using UniversalGym.StripeHelper;

namespace UniversalGym.Users
{
    public class suspendSubscription
    {
        public static BasicResponse suspendSubscriptionImplementation(BaseRequest request)
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

                if (String.IsNullOrWhiteSpace(user.StripeUrl))
                {
                    return new BasicResponse { message = "User is missing billing information.", status = 901, success = false };
                }

                new StripeSubscription().RemoveCustomerFromSubscription(user.StripeUrl);
                user.hasActiveSubscription = false;

                db.SaveChanges();

                return new BasicResponse { message = "Subscription was successfully cancelled.", status = 200, success = true };
            }
        }
    }
}