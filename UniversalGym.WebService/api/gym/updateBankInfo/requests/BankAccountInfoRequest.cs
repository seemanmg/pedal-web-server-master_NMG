using System.Runtime.Serialization;

namespace UniversalGym.Requests
{
    public class BankAccountInfoRequest : BaseRequest
    {
        [DataMember(Name = "stripeToken")]
        public string stripeToken { get; set; }


    }
}
