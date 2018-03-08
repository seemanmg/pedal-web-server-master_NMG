using System.Runtime.Serialization;

namespace UniversalGym.Requests
{
    public class getAllLogsRequest : BaseRequest
    {
        [DataMember(Name = "getError")]
        public bool getError { get; set; }

        [DataMember(Name = "getAction")]
        public bool getAction { get; set; }


    }
}
