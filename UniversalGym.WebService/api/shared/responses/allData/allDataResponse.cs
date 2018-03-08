using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniversalGym.Responses
{
    [DataContract(Name = "alldata")]
    public class AllDataResponse : BasicResponse
    {
        
        [DataMember(Name = "user")]
        public UserResponse user { get; set; }

        [DataMember(Name = "authToken")]
        public string authToken { get; set; }
    }
    
    public class UserResponse
    {
        [DataMember(Name = "userId")]
        public int userId { get; set; }
        [DataMember(Name = "email")]
        public string email { get; set; }
        [DataMember(Name = "firstName")]
        public string firstName { get; set; }
        [DataMember(Name = "lastName")]
        public string lastName { get; set; }
        [DataMember(Name = "hasActiveSubscription")]
        public bool hasActiveSubscription { get; set; }
        [DataMember(Name = "referalBaseUrl")]
        public string referalBaseUrl { get; set; }
        [DataMember(Name = "referalCode")]
        public string referalCode { get; set; }
        [DataMember(Name = "credits")]
        public int credits { get; set; }


        [DataMember(Name = "hasCreditCard")]
        public bool hasCreditCard { get; set; }
        [DataMember(Name = "stripeUserId")]
        public string stripeUserId { get; set; }


        [DataMember(Name = "purchasedGyms")]
        public List<GymResponse> purchasedGyms { get; set; }
    }

}
