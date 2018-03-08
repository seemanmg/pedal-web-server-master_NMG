using System.Runtime.Serialization;

namespace UniversalGym.Requests
{
    public class deleteGymRequest : BaseRequest
    {
        [DataMember(Name = "gymId")]
        public int gymId { get; set; }


    }
}
