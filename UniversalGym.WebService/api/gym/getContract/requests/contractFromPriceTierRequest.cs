using System.Runtime.Serialization;

namespace UniversalGym.Requests
{
    public class ContractFromPriceTierRequest : BaseRequest
    {
        [DataMember(Name = "gymName")]
        public string gymName { get; set; }

       [DataMember(Name = "priceTier")]
        public string priceTier { get; set; }
        

    }
}
