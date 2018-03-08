using System;
using System.Linq;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;
using UniversalGym.Shared;
using UniversalGym.Slack;

namespace UniversalGym.Users
{
    public class searchGymsByCity
    {
        public static GymSearchListResponse searchGymsByCityImplementation(SearchGymRequest request)
        {

            GymSearchListResponse rv;
            using (var db = new UniversalGymEntities())
            {
                var searchRequest = new Data.SearchRequest
                {
                    SearchDate = DateTime.Now,
                    UniqueDeviceIdentifier = request.uniqueDeviceId,

                };

                if (request.longitude != 0 && request.latitude != 0)
                {
                    searchRequest.Position = System.Data.Entity.Spatial.DbGeography.FromText(String.Format("POINT({0} {1})", request.longitude, request.latitude));
                }



                if (!request.city.Equals("NULL"))
                {
                    searchRequest.Request = request.city;
                }

                if (!request.state.Equals("NULL"))
                {
                    searchRequest.Request = searchRequest.Request + " " + request.state ?? "NO STATE";
                }

                var user = db.Users.SingleOrDefault(a => a.CurrentToken == request.authToken && a.UserId == request.accountId);
                if (user != null)
                {
                    searchRequest.UserId = user.UserId;

                }


                db.SearchRequests.Add(searchRequest);
                db.SaveChanges();




                // convert city, state to lat, long
                var target = searchRequest.Request;
                var geocoded = Geocoder.GeoCodeAddress(target);
                if (geocoded != null)
                {

                    var cityLocation = System.Data.Entity.Spatial.DbGeography.FromText(
                        String.Format("POINT({0} {1})",
                        geocoded.Longitude,
                        geocoded.Latitude));


                    var gyms = db.Gyms.Where(w => w.Position != null).OrderBy(o => o.Position.Distance(cityLocation));
                    rv = GymSearchListHelper.createGymSearchListResponse(gyms, cityLocation.Latitude, cityLocation.Longitude);
                    if (user != null)
                    {
                        rv.credits = user.Credits;

                    }
                    else
                    {
                        rv.credits = 0;
                    }

                    var searchText = "DeviceId: "
                    + request.uniqueDeviceId
                    + Environment.NewLine;
                    if (searchRequest.User != null)
                    {
                        searchText = searchText
                        + "User: "
                        + searchRequest.User.Email
                        + Environment.NewLine;
                    }

                    searchText = searchText
                    + "Request: "
                    + searchRequest.Request;

                    SlackHelper.sendSearchChannel(searchText, request.latitude.ToString(), request.longitude.ToString());


                }
                else
                {
                    var searchText = "";
                    if (searchRequest.User != null)
                    {
                        searchText = searchText
                        + "User: "
                        + searchRequest.User.Email
                        + Environment.NewLine;
                    }

                    searchText = searchText
                    + "Request: "
                    + searchRequest.Request;

                    SlackHelper.sendSearchChannelNotFound(searchText);

                    rv = new GymSearchListResponse {
                        success = false,
                        status = 400,
                        message = "Could not find location"
                    };
                    if (user != null)
                    {
                        rv.credits = user.Credits;

                    }
                    else
                    {
                        rv.credits = 0;
                    }
                }
            }
            return rv;

        }



    }


}