using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniversalGym.Responses
{
    public class getAllSearchRequestsResponse : BasicResponse
    {
        [DataMember(Name = "gymPasses")]
        public IList<SearchRequests> searchRequests { get; set; }


        public getAllSearchRequestsResponse()
        {

        }
    }



    public class SearchRequests
    {
        [DataMember(Name = "Id")]
        public string Id { get; set; }

        [DataMember(Name = "UserId")]
        public string UserId { get; set; }

        [DataMember(Name = "SearchDate")]
        public string SearchDate { get; set; }

        [DataMember(Name = "Request")]
        public string Request { get; set; }

        [DataMember(Name = "Position")]
        public Position Position { get; set; }

        [DataMember(Name = "UniqueDeviceIndentifier")]
        public string UniqueDeviceIndentifier { get; set; }





        [DataMember(Name = "UserName")]
        public string UserName { get; set; }

    }

}

