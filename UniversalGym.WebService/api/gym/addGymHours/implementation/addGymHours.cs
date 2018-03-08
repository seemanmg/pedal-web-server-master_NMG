using System;
using System.Linq;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;

namespace UniversalGym.Gym
{
    public class addGymHours
    {
        public static addGymHoursResponse addGymHoursImplementation(addGymHoursRequest request)
        {

            using (var db = new UniversalGymEntities())
            {
                if (request == null || String.IsNullOrWhiteSpace(request.authToken) || request.accountId == null)
                {
                    return new addGymHoursResponse { status = 401, success = false, message = "Gym not found" };
                }
                else if (String.IsNullOrWhiteSpace(request.day))
                {
                    return new addGymHoursResponse { status = 401, success = false, message = "Please select a day" };
                }
                else if (request.startTime == null)
                {
                    return new addGymHoursResponse { status = 401, success = false, message = "Please select a start time" };
                }
                else if (request.endTime == null)
                {
                    return new addGymHoursResponse { status = 401, success = false, message = "Please select an end time" };
                }


                var gym = db.Gyms.SingleOrDefault(a => a.CurrentToken == request.authToken && a.GymId == request.accountId);
                if (gym == null)
                {
                    return new addGymHoursResponse
                    {
                        message = "Gym not found.",
                        status = 404,
                        success = false,
                    };
                }

                var weekDay = db.TypeWeekDays.SingleOrDefault(s => s.TypeDescription == request.day);

                if (weekDay == null)
                {
                    return new addGymHoursResponse { status = 401, success = false, message = "Day not valid" };
                }

                var startTime = new DateTime();
                var endTime = new DateTime();

                if (request.startTime.hour == 24)
                {
                    // next day at 0 hour
                    startTime = new DateTime(1990, 11, 20, 0, request.startTime.minute, 0, 0);
                }
                else
                {
                    startTime = new DateTime(1990, 11, 19, request.startTime.hour, request.startTime.minute, 0, 0);
                }

                if (request.endTime.hour == 24)
                {
                    // next day at 0 hour

                    endTime = new DateTime(1990, 11, 20, 0, 0, 0, 0);
                }
                else
                {

                    endTime = new DateTime(1990, 11, 19, request.endTime.hour, request.endTime.minute, 0, 0);
                }

                if (startTime >= endTime)
                {
                    return new addGymHoursResponse { status = 401, success = false, message = "The start time has to come before the end time" };
                }

                // non-intersection between two dates = if (A1 > B2 || B1 > A2)
                // so intersection is the opposite !
                var overlapSchedules = db.GymSchedules.Any(s => s.GymId == request.accountId 
                    && s.TypeWeekDayId == weekDay.TypeWeekDayId 
                    && (!(s.StartTime >= endTime ||startTime >= s.EndTime)));
                if (overlapSchedules)
                {
                    return new addGymHoursResponse
                    {
                        message = "Schedule overlaps with another existing schedule.",
                        status = 404,
                        success = false,
                    };

                }


                var gymHours = new Data.GymSchedule
                {
                    TypeWeekDayId = weekDay.TypeWeekDayId,
                    StartTime = startTime,
                    EndTime = endTime,
                    GymId = gym.GymId,
                };
                db.GymSchedules.Add(gymHours);
                db.SaveChanges();


                return new addGymHoursResponse { status = 200, success = true, message = "Gym hours added!", gymHourId = gymHours.GymScheduleId };
            }
        }
    }
}
