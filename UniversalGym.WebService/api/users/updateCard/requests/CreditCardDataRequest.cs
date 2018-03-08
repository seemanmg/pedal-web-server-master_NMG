using System.Runtime.Serialization;

namespace UniversalGym.Requests
{
    public class CreditCardDataRequest : BaseRequest
    {
        [DataMember(Name = "token")]
        public string token { get; set; }

    }
}
