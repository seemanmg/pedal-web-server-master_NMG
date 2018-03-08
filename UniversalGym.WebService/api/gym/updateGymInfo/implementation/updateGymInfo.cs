using System;
using System.Linq;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;
using UniversalGym.Shared;

namespace UniversalGym.Gym
{
    public class updateGymInfo
    {
        public static BasicResponse updateGymInfoImplementation(UpdateGymInfoRequest request)
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

            if (String.IsNullOrWhiteSpace(request.data.address))
            {
                return new BasicResponse
                {
                    message = "Please enter an address.",
                    status = 404,
                    success = false,
                };
            }

            if (String.IsNullOrWhiteSpace(request.data.city))
            {
                return new BasicResponse
                {
                    message = "Please enter a city.",
                    status = 404,
                    success = false,
                };
            }

            if (String.IsNullOrWhiteSpace(request.data.contactName))
            {
                return new BasicResponse
                {
                    message = "Please enter a contact name.",
                    status = 404,
                    success = false,
                };
            }

            if (String.IsNullOrWhiteSpace(request.data.contactPhone))
            {
                return new BasicResponse
                {
                    message = "Please enter a contact phone number.",
                    status = 404,
                    success = false,
                };
            }

            if (String.IsNullOrWhiteSpace(request.data.gymName))
            {
                return new BasicResponse
                {
                    message = "Please enter a gym name.",
                    status = 404,
                    success = false,
                };
            }

            if (String.IsNullOrWhiteSpace(request.data.gymPhone))
            {
                return new BasicResponse
                {
                    message = "Please enter a gym phone number.",
                    status = 404,
                    success = false,
                };
            }

            if (String.IsNullOrWhiteSpace(request.data.gymUrl))
            {
                return new BasicResponse
                {
                    message = "Please enter a gym url.",
                    status = 404,
                    success = false,
                };
            }

            if (String.IsNullOrWhiteSpace(request.data.state))
            {
                return new BasicResponse
                {
                    message = "Please enter a state.",
                    status = 404,
                    success = false,
                };
            }

            if (String.IsNullOrWhiteSpace(request.data.zip))
            {
                return new BasicResponse
                {
                    message = "Please enter a zip code.",
                    status = 404,
                    success = false,
                };
            }

            using (var db = new UniversalGymEntities())
            {

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


                var state = db.TypeStates.FirstOrDefault(f => f.StateAbbreviation.ToLower() == request.data.state.ToLower()) ??
                            db.TypeStates.First(f => f.StateAbbreviation == "00");
                gym.ContactInfo1.Address.TypeStateId = state.TypeStateId;
                gym.ContactInfo1.Address.City = request.data.city;
                gym.ContactInfo1.Address.Zip = request.data.zip;
                gym.ContactInfo.Phone = request.data.contactPhone;
                gym.ContactInfo1.Phone = request.data.gymPhone;
                gym.GymName = request.data.gymName;
                gym.GymInfo = request.data.description;
                gym.OwnerName = request.data.contactName;
                var url = request.data.gymUrl;
                if (!String.IsNullOrWhiteSpace(url))
                {
                    gym.Url = new UriBuilder(url).Uri.ToString();
                }
                db.SaveChanges();


                db.SaveChanges();

                var target = request.data.address + " " + request.data.state + " " + request.data.zip;
                var geocoded = Geocoder.GeoCodeAddress(target);

                if (geocoded != null)
                {
                    gym.Position = System.Data.Entity.Spatial.DbGeography.FromText(geocoded.GetPointString());
                    db.SaveChanges();
                }

                return new BasicResponse() { success = true, status = 200, message = "Success!" };


            }

        }
    }
}