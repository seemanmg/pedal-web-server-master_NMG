using System;
using System.Linq;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;
using UniversalGym.Slack;

namespace UniversalGym.Users
{
    public class searchByLocation
    {
        public static GymSearchListResponse searchByLocationImplementation(SearchGymRequest request)
        {

            var location = System.Data.Entity.Spatial.DbGeography.FromText(String.Format("POINT({0} {1})", request.longitude, request.latitude));

            GymSearchListResponse rv;
            using (var db = new UniversalGymEntities())
            {
                var searchRequest = new Data.SearchRequest
                {
                    SearchDate = DateTime.Now,
                    Position = location,
                    UniqueDeviceIdentifier = request.uniqueDeviceId,

                };

                var user = db.Users.SingleOrDefault(a => a.CurrentToken == request.authToken && a.UserId == request.accountId);
                if (user != null)
                {
                    searchRequest.UserId = user.UserId;

                }
                   

                db.SearchRequests.Add(searchRequest);
                db.SaveChanges();

                var searchText = "DeviceId: "
                + request.uniqueDeviceId
                + Environment.NewLine;
                if (searchRequest.User != null)
                {
                    searchText = searchText
                    + "User: "
                    + searchRequest.User.Email;
                }
                
                SlackHelper.sendSearchChannel(searchText, request.latitude.ToString(), request.longitude.ToString());


                var gyms = db.Gyms.Where(w => w.Position != null).OrderBy(o => o.Position.Distance(location));
                rv = GymSearchListHelper.createGymSearchListResponse(gyms, location.Latitude, location.Longitude);
                if (user != null)
                {
                    rv.credits = user.Credits;
                }
                else
                {
                    rv.credits = 0;
                }
            }
            return rv;



        }


    }


}