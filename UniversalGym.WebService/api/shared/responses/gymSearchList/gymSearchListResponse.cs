using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniversalGym.Responses
{

    [DataContract(Name = "gymdata")]
    public class GymSearchListResponse : BasicResponse
    {

        public GymSearchListResponse()
        {
            
        }

        [DataMember(Name = "results")]
        public IList<GymResponse> results { get; set; }

        [DataMember(Name = "credits")]
        public int credits { get; set; }



    }


}
