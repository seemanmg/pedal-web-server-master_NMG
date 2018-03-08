using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniversalGym.Responses
{
    public class getAllUsersResponse : BasicResponse
    {
        [DataMember(Name = "users")]
        public IList<Users> users { get; set; }


        [DataMember(Name = "totalStatsUsers")]
        public totalStatsUsers totalStatsUsers { get; set; }
        

        public getAllUsersResponse()
        {

        }
    }


    public class totalStatsUsers
    {
        
        [DataMember(Name = "Credits")]
        public int Credits { get; set; }

        [DataMember(Name = "GymPassCount")]
        public int GymPassCount { get; set; }

        [DataMember(Name = "TotalRevenue")]
        public int TotalRevenue { get; set; }

        [DataMember(Name = "TotalCosts")]
        public int TotalCosts { get; set; }

        [DataMember(Name = "TotalProfit")]
        public int TotalProfit { get; set; }

    }
    public class Users
    {
        [DataMember(Name = "UserId")]
        public string UserId { get; set; }

        [DataMember(Name = "UserGuid")]
        public string UserGuid { get; set; }

        [DataMember(Name = "hasCreditCard")]
        public string hasCreditCard { get; set; }

        [DataMember(Name = "FirstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "LastName")]
        public string LastName { get; set; }

        [DataMember(Name = "Email")]
        public string Email { get; set; }

        [DataMember(Name = "hasActiveSubscription")]
        public string hasActiveSubscription { get; set; }

        [DataMember(Name = "StripeUrl")]
        public string StripeUrl { get; set; }

        [DataMember(Name = "CurrentToken")]
        public string CurrentToken { get; set; }

        [DataMember(Name = "Credits")]
        public int Credits { get; set; }

        [DataMember(Name = "joinDate")]
        public string joinDate { get; set; }

        [DataMember(Name = "ReferalUrl")]
        public string ReferalUrl { get; set; }




        [DataMember(Name = "GymPassCount")]
        public int GymPassCount { get; set; }

        [DataMember(Name = "TotalRevenue")]
        public int TotalRevenue { get; set; }

        [DataMember(Name = "TotalCosts")]
        public int TotalCosts { get; set; }

        [DataMember(Name = "TotalProfit")]
        public int TotalProfit { get; set; }

    }

}

