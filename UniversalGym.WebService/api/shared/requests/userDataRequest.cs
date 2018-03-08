using System.Runtime.Serialization;

namespace UniversalGym.Requests
{
    [DataContract(Name = "newrequestData")]
    public class UserDataRequest
    {
        [DataMember(Name = "password")]
        public string password { get; set; }

        [DataMember(Name = "emailAddress")]
        public string emailAddress { get; set; }

    }
}
