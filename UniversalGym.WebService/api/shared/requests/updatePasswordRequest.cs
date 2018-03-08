using System.Runtime.Serialization;
using UniversalGym.Requests;

namespace UniversalGym.UpdatePassword
{
    public class UpdatePasswordRequest : BaseRequest
    {
        [DataMember(Name = "password")]
        public string confirmPassword { get; set; }


        [DataMember(Name = "password")]
        public string password { get; set; }
    }
}
