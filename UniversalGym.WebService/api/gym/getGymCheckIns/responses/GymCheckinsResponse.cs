using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniversalGym.Responses
{
    public class GymCheckinsResponse:BasicResponse
    {
        [DataMember(Name = "items")]
        public IList<Checkin> items { get; set; }
    }

    public class Checkin
    {
        [DataMember(Name = "name")]
        public string name { get; set; }
        
        [DataMember(Name = "email")]
        public string email { get; set; }
        
        [DataMember(Name = "dateBought")]
        public string dateBought { get; set; }

    }

}

