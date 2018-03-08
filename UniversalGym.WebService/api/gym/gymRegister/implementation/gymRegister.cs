using System;
using System.Linq;
using UniversalGym.Data;
using UniversalGym.Email;
using UniversalGym.Slack;
using UniversalGym.Requests;
using UniversalGym.Responses;
using UniversalGym.Shared;

namespace UniversalGym.Gym
{
    public class gymRegister
    {
        public static GymDataResponse gymRegisterImplementation(RegisterGymRequest request)
        {
            if (String.IsNullOrWhiteSpace(request.gymName))
            {
                return new GymDataResponse { success = false, status = 200, message = "Please enter a gym name" };
            }
            else if (String.IsNullOrWhiteSpace(request.gymPhone))
            {
                return new GymDataResponse { success = false, status = 200, message = "Please enter a gym phone number" };
            }

            else if (String.IsNullOrWhiteSpace(request.contactName))
            {
                return new GymDataResponse { success = false, status = 200, message = "Please enter a contact name" };
            }
            else if (String.IsNullOrWhiteSpace(request.contactEmail))
            {
                return new GymDataResponse { success = false, status = 200, message = "Please enter an email" };
            }
            else if (String.IsNullOrWhiteSpace(request.contactPhone))
            {
                return new GymDataResponse { success = false, status = 200, message = "Please enter a phone number" };
            }
            else if (String.IsNullOrWhiteSpace(request.website))
            {
                return new GymDataResponse { success = false, status = 200, message = "Please enter a website" };
            }
            else if (String.IsNullOrWhiteSpace(request.address))
            {
                return new GymDataResponse { success = false, status = 200, message = "Please enter a street" };
            }
            else if (String.IsNullOrWhiteSpace(request.city))
            {
                return new GymDataResponse { success = false, status = 200, message = "Please enter a city" };
            }
            else if (String.IsNullOrWhiteSpace(request.state))
            {
                return new GymDataResponse { success = false, status = 200, message = "Please enter a state or province" };
            }
            else if (String.IsNullOrWhiteSpace(request.zip))
            {
                return new GymDataResponse { success = false, status = 200, message = "Please enter a zip code" };
            }
            else if (String.IsNullOrWhiteSpace(request.priceTier))
            {
                return new GymDataResponse { success = false, status = 200, message = "Please select one given of the price tiers" };
            }
            else if (String.IsNullOrWhiteSpace(request.password) || request.password.Length < 6)
            {
                return new GymDataResponse { success = false, status = 200, message = "Please enter a password greater than 5 characters" };
            }
            else if (String.IsNullOrWhiteSpace(request.website))
            {
                return new GymDataResponse { success = false, status = 200, message = "Please enter a website" };
            }
            if (MembershipHelper.emailAlreadyExists(request.contactEmail, Constants.GymRole))
            {
                return new GymDataResponse { success = false, status = 10, message = "Email already taken." };
            }


            var newGymGuid = MembershipHelper.createMembership(request.contactEmail, request.password, Constants.GymRole);

            using (var db = new UniversalGymEntities())
            {

                var state = db.TypeStates.FirstOrDefault(f => f.StateAbbreviation.ToLower() == request.state.ToLower()) ??
                            db.TypeStates.First(f => f.StateAbbreviation == "00");

                var address = new Data.Address()
                {
                    StreetLine1 = request.address,
                    City = request.city,
                    Zip = request.zip,
                    TypeStateId = state.TypeStateId
                };
                db.Addresses.Add(address);
                var publicContactInfo = new Data.ContactInfo
                {
                    AddressId = address.AddressId,
                    Phone = request.gymPhone,

                };
                db.ContactInfoes.Add(publicContactInfo);
                db.SaveChanges();
                var ownerContactInfo = new Data.ContactInfo
                {
                    Email = request.contactEmail,
                    Phone = request.contactPhone,

                };
                db.ContactInfoes.Add(ownerContactInfo);
                db.SaveChanges();

                var gym = new Data.Gym
                {
                    CreditDollarValue = Constants.returnGymPay(request.priceTier),
                    PriceToCharge = Constants.returnGymPrice(request.priceTier),
                    GymName = request.gymName,
                    IsApproved = false,
                    OwnerName = request.contactName,
                    Url = request.website.Contains("://") ? request.website : "http://" + request.website,
                    PublicContactInfoId = publicContactInfo.ContactInfoId,
                    OwnerContactInfoId = ownerContactInfo.ContactInfoId,
                    GymInfo = "",
                    GymGuid = newGymGuid,
                    ApplicationDate = DateTime.UtcNow,
                    IsActive = true,
                };


                db.Gyms.Add(gym);
                var target = request.address + " " + request.state + " " + request.zip;
                var geocoded = Geocoder.GeoCodeAddress(target);
                if (geocoded != null)
                {
                    gym.Position = System.Data.Entity.Spatial.DbGeography.FromText(geocoded.GetPointString());
                    db.SaveChanges();

                    var newgymBody = "Gym Registration"
                    + Environment.NewLine
                    + "Gym Name: "
                    + gym.GymName
                    + Environment.NewLine
                    + "Location: "
                    + request.address
                    + Environment.NewLine
                    + "City: "
                    + request.city
                    + Environment.NewLine
                    + "State: "
                    + request.state
                    + Environment.NewLine
                    + "Zip Code: "
                    + request.zip
                    + Environment.NewLine
                    + "Gym Phone"
                    + request.gymPhone
                    + Environment.NewLine
                    + "Contact Name: "
                    + request.contactName
                    + Environment.NewLine
                    + "Contact Email: "
                    + request.contactEmail
                    + Environment.NewLine
                    + "Contact Phone: "
                    + request.contactPhone
                    + Environment.NewLine
                    + "Website: "
                    + gym.Url
                    + Environment.NewLine;

                    SlackHelper.sendGymSignupChannel(newgymBody, geocoded.Latitude.ToString(), geocoded.Longitude.ToString());
                    // add gym id
                    var gymId = gym.GymId;
                    var link = Constants.PedalWebUrl + "gym.html#/login";

                    EmailTemplateHelper.SendEmail("Welcome to Pedal!", request.contactEmail, link, request.gymName, "gym_signup.html");
                }
                else
                {

                    db.ContactInfoes.Remove(gym.ContactInfo);
                    db.ContactInfoes.Remove(gym.ContactInfo1);

                    var supportText = "Gym Registration - Location not found!"
                    + Environment.NewLine
                    + "Gym Name: "
                    + gym.GymName
                    + Environment.NewLine
                    + "Location: "
                    + request.address
                    + Environment.NewLine
                    + "City: "
                    + request.city
                    + Environment.NewLine
                    + "State: "
                    + request.state
                    + Environment.NewLine
                    + "Zip Code: "
                    + request.zip
                    + Environment.NewLine
                    + "Gym Phone"
                    + request.gymPhone
                    + Environment.NewLine
                    + "Contact Name: "
                    + request.contactName
                    + Environment.NewLine
                    + "Contact Email: "
                    + request.contactEmail
                    + Environment.NewLine
                    + "Contact Phone: "
                    + request.contactPhone
                    + Environment.NewLine
                    + "Website: "
                    + gym.Url
                    + Environment.NewLine;

                    SlackHelper.sendSupportChannel(supportText);

                    EmailNoTemplateHelper.SendEmail("Gym Registration Problem", "hello@pedal.com", supportText);

                    return new GymDataResponse { success = false, status = 200, message = "Location could not be found. The Pedal team has been notified to look into this." };
                }


                return gymDataHelper.CreateGymDataResponse(gym.GymId, true);


            }
        }
    }
}