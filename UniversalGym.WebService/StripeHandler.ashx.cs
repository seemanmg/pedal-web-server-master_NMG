using Stripe;
using System;
using System.IO;
using System.Linq;
using System.Web;
using UniversalGym.Credit;
using UniversalGym.Data;

namespace UniversalGym
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class StripeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {


            try
            {

                Logs.LogsInsertAction("Stripe subscription attempted");

                var json = new StreamReader(context.Request.InputStream).ReadToEnd();

                var stripeEvent = StripeEventUtility.ParseEvent(json);

                switch (stripeEvent.Type)
                {
                    case StripeEvents.InvoicePaymentSucceeded:  // all of the types available are listed in StripeEvents

                        // parse first call
                        StripeInvoice insecureStripeInvoice = Stripe.Mapper<StripeInvoice>.MapFromJson(stripeEvent.Data.Object.ToString());

                        // just get id and retreive from stripe to prevent spoofing
                        var eventService = new StripeEventService();
                        StripeRequestOptions requestOptions = new StripeRequestOptions();
                        requestOptions.ApiKey = Constants.StripeSecretKey;

                        // can't be called more than once (webhooks can send multiple times)
                        requestOptions.IdempotencyKey = stripeEvent.Id;
                        StripeEvent response = eventService.Get(stripeEvent.Id, requestOptions);
                        StripeInvoice stripeInvoice = Stripe.Mapper<StripeInvoice>.MapFromJson(response.Data.Object.ToString());

                        if (stripeInvoice.Paid)
                        {
                            using (var db = new UniversalGymEntities())
                            {
                                var user = db.Users.SingleOrDefault(s => s.StripeUrl == stripeInvoice.CustomerId);
                                int charge = stripeInvoice.Total;
                                new creditAdd(db).AddCredit(user, null, charge, charge);


                                Logs.LogsInsertAction(
                                    "Stripe subscription successful: " 
                                    + stripeInvoice.Total
                                    + " added to " + user.Email);

                            }
                        }

                        break;

                }

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
            }



        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}