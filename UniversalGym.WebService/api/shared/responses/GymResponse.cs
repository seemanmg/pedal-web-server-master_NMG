using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniversalGym.Responses
{

    public class GymResponse : BasicResponse
    {

        public GymResponse()
        {
            
        }

        [DataMember(Name = "timeBought")]
        public DateTime timeBought { get; set; }

        [DataMember(Name = "displayTimeBought")]
        public string displayTimeBought { get; set; }

        [DataMember(Name = "localGymDateTimeExpiration")]
        public string localGymDateTimeExpiration { get; set; }

        [DataMember(Name = "gymId")]
        public int gymId { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "description")]
        public string description { get; set; }

        [DataMember(Name = "pictureUrl")]
        public List<string> pictureUrl { get; set; }

        [DataMember(Name = "price")]
        public decimal price { get; set; }

        [DataMember(Name = "city")]
        public string city { get; set; }

        [DataMember(Name = "state")]
        public string state { get; set; }

        [DataMember(Name = "gymUrl")]
        public string gymUrl { get; set; }

        [DataMember(Name = "phone")]
        public string phone { get; set; }

        [DataMember(Name = "address")]
        public string address { get; set; }

        [DataMember(Name = "isActive")]
        public bool? isActive { get; set; }

        [DataMember(Name = "latitude")]
        public double? latitude { get; set; }

        [DataMember(Name = "longitude")]
        public double? longitude { get; set; }



        [DataMember(Name = "miles")]
        public double miles { get; set; }

    }
}
