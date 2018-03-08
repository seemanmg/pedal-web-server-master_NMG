using Stripe;

namespace UniversalGym.StripeHelper
{
    public class StripeAch
    {
        private string subscriptionPlanId = Constants.SubscriptionPlanID;
        public StripeAch()
        {

        }

        public string CreateRecipient(string email, string name, string description, string token)
        {
            var recipient = new StripeRecipientCreateOptions
            {
                Email = email,
                Name = name,
                Description = description,
                Type = "corporation"
            };
            recipient.BankAccount = new StripeBankAccountOptions
            {
                TokenId = token
            };
            var service = new StripeRecipientService(Constants.StripeSecretKey);
            var newRecipient = service.Create(recipient);
            return newRecipient.Id;
        }
        
        public string UpdateRecipient(string recipId, string email, string name, string description, string token)
        {
            var recipient = new StripeRecipientUpdateOptions
            {
                Email = email,
                Name = name,
                Description = description
            };
            recipient.BankAccount = new StripeBankAccountOptions
            {
                TokenId = token
            };
            var service = new StripeRecipientService(Constants.StripeSecretKey);
            var newRecipient = service.Update(recipId,recipient);
            return newRecipient.Id;
        }



        public void TransferToRecipient(string recipId, string description, int centsAmount)
        {
            var myTransfer = new StripeTransferCreateOptions();
            myTransfer.Amount = centsAmount;
            myTransfer.Currency = "usd";
            myTransfer.Recipient = recipId;          // can also be "self" if you want to send to your own account
            myTransfer.Description = description;       // optional

            var transferService = new StripeTransferService(Constants.StripeSecretKey);
            var stripeTransfer = transferService.Create(myTransfer);
            
        }
  
        
    }
}
