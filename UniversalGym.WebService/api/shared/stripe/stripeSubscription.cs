using Stripe;
using System.Linq;

namespace UniversalGym.StripeHelper
{
    public class StripeSubscription
    {

        public StripeSubscription()
        {

        }

        public void AddCustomerToSubscription(string customerId)
        {
            var subscriptionService = new StripeSubscriptionService(Constants.StripeSecretKey);
            var stripeSubscription = subscriptionService.Create(customerId, Constants.SubscriptionPlanID);
        }

        public void RemoveCustomerFromSubscription(string customerId)
        {
            var customerService = new StripeCustomerService(Constants.StripeSecretKey);
            var stripeCustomer = customerService.Get(customerId);
            var subscriptionId = stripeCustomer.StripeSubscriptionList.Data.Select(s => s.Id).Distinct().FirstOrDefault();
            var subscriptionService = new StripeSubscriptionService(Constants.StripeSecretKey);
            subscriptionService.Cancel(customerId, subscriptionId);

        }
    }
}
