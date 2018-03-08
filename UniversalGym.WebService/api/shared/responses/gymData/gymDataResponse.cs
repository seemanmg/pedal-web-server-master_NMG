using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniversalGym.Responses
{
    [DataContract(Name = "gymdata")]
    public class GymDataResponse : BasicResponse
    {
        public GymDataResponse()
        {

        }


        [DataMember(Name = "gymId")]
        public int gymId { get; set; }

        [DataMember(Name = "token")]
        public string token { get; set; }
        
        [DataMember(Name = "gymName")]
        public string gymName { get; set; }

        [DataMember(Name = "gymPhone")]
        public string gymPhone { get; set; }

        [DataMember(Name = "contactName")]
        public string contactName { get; set; }

        [DataMember(Name = "contactPhone")]
        public string contactPhone { get; set; }

        [DataMember(Name = "contactEmail")]
        public string contactEmail { get; set; }
        
        [DataMember(Name = "description")]
        public string description { get; set; }

        [DataMember(Name = "isApproved")]
        public bool isApproved { get; set; }


        [DataMember(Name = "pictures")]
        public IEnumerable<PictureObject> pictures { get; set; }

        [DataMember(Name = "address")]
        public string address { get; set; }  
        
        [DataMember(Name = "city")]
        public string city { get; set; }
        
        [DataMember(Name = "state")]
        public string state { get; set; }
        [DataMember(Name = "zip")]
        public string zip { get; set; }
  
        [DataMember(Name = "price")]
        public string price{ get; set; }

        [DataMember(Name = "gymUrl")]
        public string gymUrl { get; set; }

        [DataMember(Name = "scheduleUrl")]
        public string scheduleUrl { get; set; }

        [DataMember(Name = "hasAccount")]
        public bool hasAccount { get; set; }



        [DataMember(Name = "opens")]
        public IEnumerable<Schedule> schedules { get; set; }

        [DataMember(Name = "hasRightHours")]
        public bool hasRightHours { get; set; }

        [DataMember(Name = "hasRightGymPhotos")]
        public bool hasRightGymPhotos { get; set; }

        [DataMember(Name = "rightGymPhotosMessage")]
        public string rightGymPhotosMessage { get; set; }

        [DataMember(Name = "rightHourMessage")]
        public string rightHourMessage { get; set; }

        [DataMember(Name = "startingPaymentDate")]
        public string startingPaymentDate { get; set; }

        [DataMember(Name = "endingPaymentDate")]
        public string endingPaymentDate { get; set; }

        [DataMember(Name = "currentPaymentBalance")]
        public int currentPaymentBalance { get; set; }

        [DataMember(Name = "collectPaymentBalance")]
        public int collectPaymentBalance { get; set; }

    }


    public class PictureObject
    {
        public string url { get; set; }

        public bool isCover { get; set; }

        public int id { get; set; }
    }

    public class Schedule
    {
        public string day { get; set; }

        public gymHour startTime { get; set; }

        public gymHour endTime { get; set; }

        public int id { get; set; }
    }

    public class gymHour
    {
        public int hour { get; set; }

        public int minute { get; set; }
    }
}