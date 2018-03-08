using UniversalGym.Responses;
using UniversalGym.Requests;
using UniversalGym.Data;
using System.Linq;
using System.Collections.Generic;
using System;

namespace UniversalGym.Admin
{
    public class getAllUsers
    {
        public static getAllUsersResponse getAllUsersImplementation(BaseRequest request)
        {

            if (request.authToken != Constants.MarketingToken)
                return new getAllUsersResponse() { success = false, status = 404, message = "Wrong Token" };

            var rv = new getAllUsersResponse();
            rv.totalStatsUsers = new totalStatsUsers();
            rv.totalStatsUsers.GymPassCount = 0;
            rv.totalStatsUsers.Credits = 0;
            rv.totalStatsUsers.TotalRevenue = 0;
            rv.totalStatsUsers.TotalCosts = 0;
            rv.totalStatsUsers.TotalProfit = 0;

            rv.users = new List<Responses.Users>();
            using (var db = new UniversalGymEntities())
            {
                var users = db.Users.ToList();
                foreach (var user in users)
                {
                    var temp = new Responses.Users
                    {
                        UserId = user.UserId.ToString() ?? "NULL",
                        UserGuid = user.UserGuid.ToString() ?? "NULL",
                        hasCreditCard = user.hasCreditCard.ToString() ?? "NULL",
                        FirstName = user.FirstName ?? "NULL",
                        LastName = user.LastName ?? "NULL",
                        Email = user.Email ?? "NULL",
                        hasActiveSubscription = user.hasActiveSubscription.ToString() ?? "NULL",
                        StripeUrl = user.StripeUrl ?? "NULL",
                        CurrentToken = user.CurrentToken ?? "NULL",
                        Credits = user.Credits,
                        joinDate = user.joinDate.ToString() ?? "NULL",
                        ReferalUrl = user.ReferalUrl ?? "NULL",
                    };

                    rv.totalStatsUsers.Credits = rv.totalStatsUsers.Credits + temp.Credits;


                    temp.GymPassCount = user.GymPasses.Count;
                    rv.totalStatsUsers.GymPassCount = rv.totalStatsUsers.GymPassCount + temp.GymPassCount;

                    if (user.GymPasses.Count > 0)
                    {
                        var totalRevenue =
                            from gp in user.GymPasses
                            group gp by 1 into g
                            select new
                            {
                                totalCredit = g.Sum(x => x.CreditsUsed),
                                totalCharged = g.Sum(x => x.AmountCharged),
                                totalCost = g.Sum(x => x.GymPassCost)
                            };

                        temp.TotalRevenue = totalRevenue.SingleOrDefault().totalCredit + totalRevenue.SingleOrDefault().totalCharged;
                        temp.TotalCosts = totalRevenue.SingleOrDefault().totalCost;
                        temp.TotalProfit =
                            (totalRevenue.SingleOrDefault().totalCredit + totalRevenue.SingleOrDefault().totalCharged)
                            - totalRevenue.SingleOrDefault().totalCost;

                        rv.totalStatsUsers.TotalRevenue = rv.totalStatsUsers.TotalRevenue + temp.TotalRevenue;
                        rv.totalStatsUsers.TotalCosts = rv.totalStatsUsers.TotalCosts + temp.TotalCosts;
                        rv.totalStatsUsers.TotalProfit = rv.totalStatsUsers.TotalProfit + temp.TotalProfit;

                    }
                }

            }

            rv.success = true;
            rv.message = "";
            return rv;
        }
    }
}