using System.Runtime.Serialization;

namespace UniversalGym.Responses
{

    [DataContract(Name = "data")]
    public class BasicResponse
    {
        [DataMember(Name = "success")]
        public bool success { get; set; }
        [DataMember(Name = "status")]
        public int status { get; set; }
        [DataMember(Name = "message")]
        public string message{ get; set; }
    }
}
