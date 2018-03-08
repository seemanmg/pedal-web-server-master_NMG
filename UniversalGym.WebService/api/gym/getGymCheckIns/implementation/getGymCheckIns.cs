using System;
using System.Data.Entity;
using System.Linq;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;

namespace UniversalGym.Gym
{
    public class getGymCheckIns
    {
        public static GymCheckinsResponse getGymCheckInsImplementation(GetGymCheckInsRequest request)
        {
            using (var db = new UniversalGymEntities())
            {

                if (request == null || String.IsNullOrWhiteSpace(request.authToken) || request.accountId == null)
                {
                    return new GymCheckinsResponse
                    {
                        message = "Gym not found.",
                        status = 404,
                        success = false,
                    };
                }

                var gym = db.Gyms.SingleOrDefault(a => a.CurrentToken == request.authToken && a.GymId == request.accountId);
                if (gym == null)
                {
                    return new GymCheckinsResponse
                    {
                        message = "Gym not found.",
                        status = 404,
                        success = false,
                    };
                }

                var month = DateTime.UtcNow.Month;
                var year = DateTime.UtcNow.Year;
                var startDate = new DateTime(year, month, 1);
                var endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
                var newCheckInds = db.GymPasses.Where(w => w.LocalDateBought >= startDate && w.LocalDateBought <= endDate && w.GymId == request.accountId).OrderByDescending(o => o.LocalDateBought).ThenByDescending(t => t.LocalDateBought).Take(request.count).ToList();
                var newVms = newCheckInds.Select(
                        s =>
                            new Checkin()
                            {
                                name = s.User.FirstName + " " + s.User.LastName,
                                email = s.User.Email,
                                dateBought = s.LocalDateBought.ToString(),
                            }).ToList();
                var rv = new GymCheckinsResponse()
                {
                    items = newVms,
                    status = 200,
                    success = true,
                    message = "Success!"
                };
                return rv;

            }

        }
    }
}