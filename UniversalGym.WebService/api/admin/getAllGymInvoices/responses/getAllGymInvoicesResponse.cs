using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniversalGym.Responses
{
    public class getAllGymInvoicesResponse : BasicResponse
    {
        [DataMember(Name = "giveCredits")]
        public IList<GymInvoices> gymInvoices { get; set; }


        public getAllGymInvoicesResponse()
        {

        }
    }



    public class GymInvoices
    {
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        [DataMember(Name = "StartPeriodDate")]
        public string StartPeriodDate { get; set; }

        [DataMember(Name = "EndPeriodDate")]
        public string EndPeriodDate { get; set; }

        [DataMember(Name = "AmountPaid")]
        public string AmountPaid { get; set; }

        [DataMember(Name = "IsCollected")]
        public string IsCollected { get; set; }

        [DataMember(Name = "GymId")]
        public string GymId { get; set; }
     

    }

}

