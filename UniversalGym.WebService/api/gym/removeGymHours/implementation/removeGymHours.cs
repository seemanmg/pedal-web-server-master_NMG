using System;
using System.Linq;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;

namespace UniversalGym.Gym
{
    public class removeGymHours
    {
        public static BasicResponse removeGymHoursImplementation(removeGymHoursRequest request)
        {
            using (var db = new UniversalGymEntities())
            {
                if (request == null || String.IsNullOrWhiteSpace(request.authToken) || request.accountId == null)
                {
                    return new BasicResponse
                    {
                        message = "Gym not found.",
                        status = 404,
                        success = false,
                    };
                }

                var gym = db.Gyms.SingleOrDefault(a => a.CurrentToken == request.authToken && a.GymId == request.accountId);
                if (gym == null)
                {
                    return new BasicResponse
                    {
                        message = "Gym not found.",
                        status = 404,
                        success = false,
                    };
                }

                var gymHour = db.GymSchedules.First(w => w.GymScheduleId == request.gymHourId);
                if (gymHour == null)
                {
                    return new BasicResponse
                    {
                        message = "Time schedule not found.",
                        status = 404,
                        success = false,
                    };
                }

                db.GymSchedules.Remove(gymHour);
                db.SaveChanges();
                return new BasicResponse { status = 200, success = true, message = "Successfully Removed" };
            }
        }
    }
}
