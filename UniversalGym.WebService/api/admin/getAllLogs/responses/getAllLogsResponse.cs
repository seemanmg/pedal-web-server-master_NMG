using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniversalGym.Responses
{
    public class getAllLogsResponse : BasicResponse
    {
        [DataMember(Name = "logs")]
        public IList<Logs> logs { get; set; }


        public getAllLogsResponse()
        {

        }
    }



    public class Logs
    {
        [DataMember(Name = "LogId")]
        public string LogId { get; set; }

        [DataMember(Name = "LogMessage")]
        public string LogMessage { get; set; }

        [DataMember(Name = "InsertDate")]
        public string InsertDate { get; set; }

        [DataMember(Name = "IsError")]
        public string IsError { get; set; }
    }

}

