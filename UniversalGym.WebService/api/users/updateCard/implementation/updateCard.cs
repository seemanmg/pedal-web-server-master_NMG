using System;
using System.Linq;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;
using UniversalGym.StripeHelper;

namespace UniversalGym.Users
{
    public class updateCard
    {
        public static UpdateCardResponse updateCardImplementation(CreditCardDataRequest request)
        {
            return UpdateCardInfo(request, StripeCustomer.HandleCustomer);
        }

        public delegate string StripeAction(string cardToken, User user);
        public static UpdateCardResponse UpdateCardInfo(CreditCardDataRequest request, StripeAction stripeAction)
        {

            //validate incoming data
            if (String.IsNullOrWhiteSpace(request.token))
            {
                return new UpdateCardResponse { message = "Missing token.", status = 601, success = false };
            }

            if (request == null || String.IsNullOrWhiteSpace(request.authToken) || request.accountId == null)
            {
                return new UpdateCardResponse
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
                    return new UpdateCardResponse
                    {
                        message = "User not found.",
                        status = 404,
                        success = false,
                    };
                }

                var message = "";
                // temporarily add credits to frontend so user immediately sees money
                var shouldAddCredits = false;

                // empty stripe url means new customer
                if (String.IsNullOrWhiteSpace(user.StripeUrl))
                {
                    message = "Card added successfully.";
                    shouldAddCredits = true;
                }

                // has active subscription already means updating card
                if(user.hasActiveSubscription)
                {
                    message = "Card updated successfully.";
                    shouldAddCredits = false;
                }
                // does not have active subscription means re-activating card 
                else
                {
                    message = "Card re-activated successfully.";
                    shouldAddCredits = true;
                }
                 

               

                user.StripeUrl = stripeAction(request.token, user);
                db.SaveChanges();

                return new UpdateCardResponse {
                    message = message,
                    status = 200,
                    success = true,
                    shouldAddCredits = shouldAddCredits
                };
            }
        }
    }
}