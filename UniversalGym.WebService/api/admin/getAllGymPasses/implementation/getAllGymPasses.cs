using UniversalGym.Responses;
using UniversalGym.Requests;
using UniversalGym.Data;
using System.Linq;
using System.Collections.Generic;
using System;

namespace UniversalGym.Admin
{
    public class getAllGymPasses
    {
        public static getAllGymPassesResponse getAllGymPassesImplementation(BaseRequest request)
        {

            if (request.authToken != Constants.MarketingToken)
                return new getAllGymPassesResponse() { success = false, status = 404, message = "Wrong Token" };

            var rv = new getAllGymPassesResponse();


            rv.gymPasses = new List<Responses.GymPasses>();
            rv.totalStatsGymPasses = new totalStatsGymPasses();
            rv.totalStatsGymPasses.GymPassCost = 0;
            rv.totalStatsGymPasses.GymPassProfit = 0;
            rv.totalStatsGymPasses.CreditsUsed = 0;
            rv.totalStatsGymPasses.AmountCharged = 0;


            using (var db = new UniversalGymEntities())
            {
                var gymPasses = db.GymPasses.ToList();


                foreach (var gymPass in gymPasses)
                {
                    var temp = new Responses.GymPasses
                    {
                        Id = gymPass.Id.ToString() ?? "NULL",
                        UserId = gymPass.UserId.ToString() ?? "NULL",
                        GymId = gymPass.GymId.ToString() ?? "NULL",
                        LocalDateBought = gymPass.LocalDateBought.ToString() ?? "NULL",
                        LocalDateExpired = gymPass.LocalDateExpired.ToString() ?? "NULL",
                        CreditsUsed = gymPass.CreditsUsed,
                        AmountCharged = gymPass.AmountCharged,
                        ServerTimeDateBought = gymPass.ServerTimeDateBought.ToString() ?? "NULL",
                        GymPassCost = gymPass.GymPassCost,
                    };

                    temp.GymPassProfit = (temp.GymPassCost + temp.CreditsUsed) - gymPass.GymPassCost;
                    rv.totalStatsGymPasses.GymPassCost = rv.totalStatsGymPasses.GymPassCost + temp.GymPassCost;
                    rv.totalStatsGymPasses.GymPassProfit = rv.totalStatsGymPasses.GymPassProfit + temp.GymPassProfit;
                    rv.totalStatsGymPasses.CreditsUsed = rv.totalStatsGymPasses.CreditsUsed + temp.CreditsUsed;
                    rv.totalStatsGymPasses.AmountCharged = rv.totalStatsGymPasses.AmountCharged + temp.AmountCharged;


                    if (gymPass.Gym != null || gymPass.Gym.Position != null)
                    {
                        temp.Position = new Position();
                        temp.Position.Longitude = gymPass.Gym.Position.Longitude.ToString();
                        temp.Position.Latitude = gymPass.Gym.Position.Latitude.ToString();
                        temp.GymName = gymPass.Gym.GymName;
                    }

                    if (gymPass.User != null)
                    {
                        temp.UserName = gymPass.User.FirstName + " " + gymPass.User.LastName;
                    }

                    rv.gymPasses.Add(temp);
                }

            }

            rv.success = true;
            rv.message = "";
            return rv;
        }
    }
}