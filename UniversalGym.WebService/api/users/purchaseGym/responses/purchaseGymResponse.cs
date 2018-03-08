
using System;

namespace UniversalGym.Responses
{
    public class PurchaseGymResponse : BasicResponse
    {
        public PurchaseGymResponse() { }

        public int newCreditValue { get; set; }
        public string localGymDateTimeExpiration { get; set; }

        public string localGymDateTimeBought { get; set; }
        

    }
}
