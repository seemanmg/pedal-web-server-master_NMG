using Stripe;
using System;
using UniversalGym.Data;

namespace UniversalGym.StripeHelper
{
    public class StripeCustomer
    {
        private string subscriptionPlanId = Constants.SubscriptionPlanID;

        public StripeCustomer()
        {

        }

        // decides whether to create new customer or update them
        public static string HandleCustomer(string cardToken, Data.User user)
        {
            var rv = "";
            if (String.IsNullOrWhiteSpace(user.StripeUrl))
            {
                // create new customer
                rv = new StripeCustomer().CreateCustomer(user.Email, user.FirstName + " " + user.LastName, cardToken);
                new StripeSubscription().AddCustomerToSubscription(rv);
                using (var db = new UniversalGymEntities())
                {
                    user.StripeUrl = rv;
                    user.hasActiveSubscription = true;
                    user.hasCreditCard = true;
                    db.SaveChanges();
                }

            }
            else
            {

                // update customer
                rv = new StripeCustomer().UpdateCustomer(user.StripeUrl, cardToken);


                // if user suspended account and is reenabling it right now
                using (var db = new UniversalGymEntities())
                {
                    if (user.hasActiveSubscription != true)
                    {
                        new StripeSubscription().AddCustomerToSubscription(rv);
                        user.hasActiveSubscription = true;
                        user.hasCreditCard = true;

                        db.SaveChanges();
                    }
                }
            }

            return rv;
        }

        public string UpdateCustomer(string stripeId, string ccToken)
        {
            //remove old card
            var customerService = new StripeCustomerService(Constants.StripeSecretKey);
            Stripe.StripeCustomer stripeCustomer = customerService.Get(stripeId);

            if (!String.IsNullOrEmpty(stripeCustomer.DefaultSourceId))
            {
                var cardService = new StripeCardService(Constants.StripeSecretKey);
                cardService.Delete(stripeId, stripeCustomer.DefaultSourceId);
            }
            var myCustomer = new StripeCustomerUpdateOptions();

            myCustomer.SourceToken = ccToken;

            // this will set the default card to use for this customer
            var finalCustomer = customerService.Update(stripeId, myCustomer);
            return finalCustomer.Id;

        }
        public string CreateCustomer(string email, string name, string ccToken)
        {
            var myCustomer = new StripeCustomerCreateOptions();

            // set these properties if it makes you happy
            myCustomer.Email = email;
            myCustomer.Description = name;

            myCustomer.SourceToken = ccToken;

            var customerService = new StripeCustomerService(Constants.StripeSecretKey);
            Stripe.StripeCustomer stripeCustomer = customerService.Create(myCustomer);
            return stripeCustomer.Id;
        }


        public string CreateCustomer(string email, string name, string cardNumber, string expMon, string expYr, string cvc)
        {
            var myCustomer = new StripeCustomerCreateOptions();

            // set these properties if it makes you happy
            myCustomer.Email = email;
            myCustomer.Description = name;

            myCustomer.SourceCard = new SourceCard()
            {
                Number = cardNumber,
                ExpirationMonth = expMon,
                ExpirationYear = expYr,
                Cvc = cvc // optional
            };

            var customerService = new StripeCustomerService(Constants.StripeSecretKey);
            var stripeCustomer = customerService.Create(myCustomer);
            return stripeCustomer.Id;
        }
    }

    }
