using System.Runtime.Serialization;

namespace UniversalGym.Requests
{
    public class GetGymCheckInsRequest: BaseRequest
    {

       [DataMember(Name = "page")]
        public int page { get; set; }
        [DataMember(Name = "count")]
        public int count { get; set; }
        

    }
}
