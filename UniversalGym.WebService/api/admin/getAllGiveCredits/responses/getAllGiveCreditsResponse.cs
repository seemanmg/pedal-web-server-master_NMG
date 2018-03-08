using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniversalGym.Responses
{
    public class getAllGiveCreditsResponse : BasicResponse
    {
        [DataMember(Name = "giveCredits")]
        public IList<GiveCredits> giveCredits { get; set; }


        public getAllGiveCreditsResponse()
        {

        }
        
    }



    public class GiveCredits
    {
        [DataMember(Name = "GiveCreditsId")]
        public string GiveCreditsId { get; set; }

        [DataMember(Name = "AmountToGiveNewUser")]
        public string AmountToGiveNewUser { get; set; }

        [DataMember(Name = "UserIdToGiveCredits")]
        public string UserIdToGiveCredits { get; set; }

        [DataMember(Name = "DateTime")]
        public string DateTime { get; set; }

        [DataMember(Name = "UserIdWhoGiveCredits")]
        public string UserIdWhoGiveCredits { get; set; }

        [DataMember(Name = "AmountToGiveReferer")]
        public string AmountToGiveReferer { get; set; }
        



        [DataMember(Name = "GivenUserName")]
        public string GivenUserName { get; set; }
        
        [DataMember(Name = "RefererUserName")]
        public string RefererUserName { get; set; }

    }

}

