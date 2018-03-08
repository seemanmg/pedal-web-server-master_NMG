using UniversalGym.Responses;
using UniversalGym.Requests;
using UniversalGym.Data;
using System.Linq;
using System.Collections.Generic;
using System;

namespace UniversalGym.Admin
{
    public class getAllGiveCredits
    {
        public static getAllGiveCreditsResponse getAllGiveCreditsImplementation(BaseRequest request)
        {

            if (request.authToken != Constants.MarketingToken)
                return new getAllGiveCreditsResponse() { success = false, status = 404, message = "Wrong Token" };

            var rv = new getAllGiveCreditsResponse();


            rv.giveCredits = new List<Responses.GiveCredits>();
            using (var db = new UniversalGymEntities())
            {
                var giveCredits = db.GiveCredits.ToList();
                foreach (var giveCredit in giveCredits)
                {
                    var temp = new Responses.GiveCredits
                    {
                        GiveCreditsId = giveCredit.GiveCreditsId.ToString() ?? "NULL",
                        AmountToGiveNewUser = giveCredit.AmountToGiveNewUser.ToString() ?? "NULL",
                        UserIdToGiveCredits = giveCredit.UserIdToGiveCredits.ToString() ?? "NULL",
                        DateTime = giveCredit.DateTime.ToString() ?? "NULL",
                        UserIdWhoGiveCredits = giveCredit.UserIdToGiveCredits.ToString() ?? "NULL",
                        AmountToGiveReferer = giveCredit.AmountToGiveReferer.ToString() ?? "NULL",
                    };

                    if (giveCredit.User != null)
                    {
                        temp.GivenUserName = giveCredit.User.FirstName ?? "NULL" + " " + giveCredit.User.LastName ?? "NULL";
                    }

                    if (giveCredit.User1 != null)
                    {
                        temp.RefererUserName = giveCredit.User1.FirstName ?? "NULL" + " " + giveCredit.User1.LastName ?? "NULL";
                    }
                    rv.giveCredits.Add(temp);
                }

            }
            rv.success = true;
            rv.message = "";
            return rv;
        }
    }
}