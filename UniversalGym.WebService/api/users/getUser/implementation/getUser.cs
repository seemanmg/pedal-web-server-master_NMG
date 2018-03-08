using System;
using System.Linq;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;

namespace UniversalGym.Users
{
    public class getUser
    {
        public static AllDataResponse getUserImplementation(BaseRequest request)
        {

            if (request == null || String.IsNullOrWhiteSpace(request.authToken) || request.accountId == null)
            {
                return new AllDataResponse
                {
                    message = "User not found.",
                    status = 404,
                    success = false,
                };
            }

            using (var db = new UniversalGymEntities())
            {
                var user = db.Users.SingleOrDefault(a => a.CurrentToken == request.authToken && a.UserId == request.accountId);
                if (user == null)
                {
                    return new AllDataResponse
                    {
                        message = "User not found.",
                        status = 404,
                        success = false,
                    };
                }
                return allDataHelper.CreateAllDataResponse(request.accountId, false);
            }
        }
    }
}