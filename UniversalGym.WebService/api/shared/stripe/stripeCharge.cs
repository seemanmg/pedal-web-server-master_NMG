using Stripe;

namespace UniversalGym.StripeHelper
{
    public class StripeCharge
    {

        public StripeCharge()
        {

        }
       

        public void ChargeCardForPass(string stripeUrl, int cents, string gymName)
        {
            this.ChargeStripeAccount(stripeUrl, cents, "Pedal pass for " + gymName);
        }

        public void ChargeStripeAccount(string stripeUrl, int cents, string description)
        {
            var myCharge = new StripeChargeCreateOptions();

            // always set these properties
            myCharge.Amount = cents;
            myCharge.Currency = "usd";

            // set this if you want to
            myCharge.Description = description;

            // set this property if using a customer
            myCharge.CustomerId = stripeUrl;

            // (not required) set this to false if you don't want to capture the charge yet - requires you call capture later
            myCharge.Capture = true;

            var chargeService = new StripeChargeService(Constants.StripeSecretKey);
            var stripeCharge = chargeService.Create(myCharge);
        }

    }
}
