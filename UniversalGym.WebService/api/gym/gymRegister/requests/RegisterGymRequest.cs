using System.Runtime.Serialization;

namespace UniversalGym.Requests
{
    public class RegisterGymRequest : BaseRequest
    {
        [DataMember(Name = "gymName")]
        public string gymName { get; set; }

        [DataMember(Name = "gymPhone")]
        public string gymPhone { get; set; }





        [DataMember(Name = "contactName")]
        public string contactName { get; set; }

        [DataMember(Name = "contactEmail")]
        public string contactEmail { get; set; }

        [DataMember(Name = "password")]
        public string password { get; set; }

        [DataMember(Name = "contactPhone")]
        public string contactPhone { get; set; }




        [DataMember(Name = "address")]
        public string address { get; set; }

        [DataMember(Name = "city")]
        public string city { get; set; }

        [DataMember(Name = "state")]
        public string state { get; set; }
        
        [DataMember(Name = "zip")]
        public string zip { get; set; }

        [DataMember(Name = "country")]
        public string country { get; set; }



        [DataMember(Name = "priceTier")]
        public string priceTier { get; set; }

        [DataMember(Name = "website")]
        public string website { get; set; }


    }
}
