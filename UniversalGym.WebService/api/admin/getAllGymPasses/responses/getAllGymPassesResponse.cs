using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniversalGym.Responses
{
    public class getAllGymPassesResponse : BasicResponse
    {
        [DataMember(Name = "gymPasses")]
        public IList<GymPasses> gymPasses { get; set; }


        [DataMember(Name = "totalStatsGymPasses")]
        public totalStatsGymPasses totalStatsGymPasses { get; set; }



        public getAllGymPassesResponse()
        {

        }
    }

    public class totalStatsGymPasses
    {

        [DataMember(Name = "GymPassCost")]
        public int GymPassCost { get; set; }

        [DataMember(Name = "GymPassProfit")]
        public int GymPassProfit { get; set; }

        [DataMember(Name = "CreditsUsed")]
        public int CreditsUsed { get; set; }

        [DataMember(Name = "AmountCharged")]
        public int AmountCharged { get; set; }

    }


    public class GymPasses
    {
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        [DataMember(Name = "UserId")]
        public string UserId { get; set; }

        [DataMember(Name = "GymId")]
        public string GymId { get; set; }

        [DataMember(Name = "LocalDateBought")]
        public string LocalDateBought { get; set; }

        [DataMember(Name = "LocalDateExpired")]
        public string LocalDateExpired { get; set; }

        [DataMember(Name = "CreditsUsed")]
        public int CreditsUsed { get; set; }

        [DataMember(Name = "AmountCharged")]
        public int AmountCharged { get; set; }

        [DataMember(Name = "ServerTimeDateBought")]
        public string ServerTimeDateBought { get; set; }

        [DataMember(Name = "Position")]
        public Position Position { get; set; }

        [DataMember(Name = "UserName")]
        public string UserName { get; set; }

        [DataMember(Name = "GymName")]
        public string GymName { get; set; }

        [DataMember(Name = "GymPassCost")]
        public int GymPassCost { get; set; }

        [DataMember(Name = "GymPassProfit")]
        public int GymPassProfit { get; set; }


    }

}

