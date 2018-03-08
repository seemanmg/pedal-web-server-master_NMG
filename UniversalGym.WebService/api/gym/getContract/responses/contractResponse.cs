using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniversalGym.Responses
{

    public class ContractResponse : BasicResponse
    {

        public ContractResponse()
        {
            
        }

        [DataMember(Name = "gymName")]
        public string gymName { get; set; }

        [DataMember(Name = "price")]
        public int price { get; set; }
        
        [DataMember(Name = "date")]
        public string date { get; set; }

    }
}
