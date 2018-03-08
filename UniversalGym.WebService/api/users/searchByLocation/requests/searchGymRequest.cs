using System.Runtime.Serialization;

namespace UniversalGym.Requests
{
    public class SearchGymRequest : BaseRequest
    {
        [DataMember(Name = "latitude")]
        public double latitude { get; set; }
        [DataMember(Name = "longitude")]
        public double longitude { get; set; }

        [DataMember(Name = "uniqueDeviceId")]
        public string uniqueDeviceId { get; set; }

        [DataMember(Name = "city")]
        public string city { get; set; }
        [DataMember(Name = "state")]
        public string state { get; set; }


    }
}
