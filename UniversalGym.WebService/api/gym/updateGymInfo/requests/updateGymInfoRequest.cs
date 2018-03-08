using System.Runtime.Serialization;
using UniversalGym.Responses;

namespace UniversalGym.Requests
{
    public class UpdateGymInfoRequest : BaseRequest
    {
        
        [DataMember(Name = "data")]
        public GymDataResponse data{ get; set; }

    }
}
