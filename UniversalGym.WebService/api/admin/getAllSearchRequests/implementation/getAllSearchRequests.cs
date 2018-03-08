using UniversalGym.Responses;
using UniversalGym.Requests;
using UniversalGym.Data;
using System.Linq;
using System.Collections.Generic;
using System;

namespace UniversalGym.Admin
{
    public class getAllSearchRequests
    {
        public static getAllSearchRequestsResponse getAllSearchRequestsImplementation(BaseRequest request)
        {
            if (request.authToken != Constants.MarketingToken)
                return new getAllSearchRequestsResponse() { success = false, status = 404, message = "Wrong Token" };

            var rv = new getAllSearchRequestsResponse();


            rv.searchRequests = new List<Responses.SearchRequests>();
            using (var db = new UniversalGymEntities())
            {
                var searchRequests = db.SearchRequests.ToList();
                foreach (var searchRequest in searchRequests)
                {
                    var temp = new Responses.SearchRequests
                    {
                        Id = searchRequest.Id.ToString() ?? "NULL",
                        SearchDate = searchRequest.SearchDate.ToString() ?? "NULL",
                        Request = searchRequest.Request ?? "NULL",
                        UniqueDeviceIndentifier = searchRequest.UniqueDeviceIdentifier ?? "NULL",
                    };

                    if (searchRequest.Position != null)
                    {
                        temp.Position = new Position();
                        temp.Position.Longitude = searchRequest.Position.Longitude.ToString();
                        temp.Position.Latitude = searchRequest.Position.Latitude.ToString();
                    }

                    if (searchRequest.UserId != null)
                    {
                        temp.UserId = searchRequest.UserId.ToString() ?? "NULL";
                        temp.UserName = searchRequest.User.FirstName ?? "NULL";
                        temp.UserName = temp.UserName + " " + searchRequest.User.LastName ?? "NULL";
                    }

                    rv.searchRequests.Add(temp);
                }

            }
            rv.success = true;
            rv.message = "";
            return rv;
        }
    }
}