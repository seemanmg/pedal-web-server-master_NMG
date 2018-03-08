using System.Runtime.Serialization;

namespace UniversalGym.Requests
{
    public class UpdateProfileRequest: BaseRequest
    {
        
        [DataMember(Name = "firstName")]
        public string firstName{ get; set; }

        [DataMember(Name = "lastName")]
        public string lastName{ get; set; }


    }
}
