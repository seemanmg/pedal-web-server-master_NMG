

namespace UniversalGym.Requests
{
    public class addGymHoursRequest : BaseRequest
    {
        public string day { get; set; }
        public gymHour startTime { get; set; }
        public gymHour endTime { get; set; }

        public class gymHour
        {
            public int hour { get; set; }
            
            public int minute { get; set; }

        }
    }

}

