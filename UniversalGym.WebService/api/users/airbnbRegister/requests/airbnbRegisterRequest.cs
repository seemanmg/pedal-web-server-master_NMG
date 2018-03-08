using System.Runtime.Serialization;

namespace UniversalGym.Requests
{
    public class AirbnbRegisterRequest: BaseRequest
    {
        
        [DataMember(Name = "firstName")]
        public string firstName { get; set; }

        [DataMember(Name = "lastName")]
        public string lastName { get; set; }

        [DataMember(Name = "email")]
        public string email { get; set; }

        [DataMember(Name = "password")]
        public string password { get; set; }

        [DataMember(Name = "confirmPassword")]
        public string confirmPassword { get; set; }

        [DataMember(Name = "airbnbHostId")]
        public string airbnbHostId { get; set; }


    }
}
