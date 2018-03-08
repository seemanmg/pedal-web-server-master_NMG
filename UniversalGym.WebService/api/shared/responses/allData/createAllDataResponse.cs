namespace UniversalGym
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UniversalGym.Data;
    using UniversalGym.Responses;
    using UniversalGym.Users;

    public class allDataHelper
    {
        public static AllDataResponse CreateAllDataResponse(int userId, bool genNewToken)
        {
            using (var db = new UniversalGymEntities())
            {
                var userItem = db.Users.First(f => f.UserId == userId);

                var baseRefUrl = "";
                if (genNewToken)
                {
                    userItem.CurrentToken = Guid.NewGuid().ToString();
                    db.SaveChanges();
                }

                var user = new UserResponse
                {
                    userId = userId,
                    firstName = userItem.FirstName,
                    lastName = userItem.LastName,
                    email = userItem.Email,
                    referalBaseUrl = baseRefUrl,
                    referalCode = userItem.ReferalUrl,
                    hasActiveSubscription = userItem.hasActiveSubscription,
                    credits = userItem.Credits,
                    stripeUserId = (userItem.StripeUrl ?? "").ToString(),
                    hasCreditCard = userItem.hasCreditCard,
                    purchasedGyms = new List<GymResponse>(),

                };

                var userPasses = db.GymPasses.Include("Gym").Where(w => w.UserId == userId).OrderByDescending(o => o.ServerTimeDateBought).ToList().Take(15);

                foreach (var userPass in userPasses)
                {

                    var temp = new Responses.GymResponse
                    {
                        name = userPass.Gym.GymName,
                        description = userPass.Gym.GymInfo,
                        gymId = userPass.GymId,
                        pictureUrl = GymSearchListHelper.getPictureUrl(userPass.Gym.GymPhotoGalleries),
                        price = userPass.Gym.PriceToCharge,
                        city = userPass.Gym.ContactInfo.Address.City,
                        state = userPass.Gym.ContactInfo.Address.TypeState.StateAbbreviation,
                        gymUrl = userPass.Gym.Url ?? "",
                        isActive = userPass.Gym.IsActive,
                        address = userPass.Gym.ContactInfo.Address.StreetLine1 + " " + userPass.Gym.ContactInfo.Address.StreetLine2 + "," +
                           userPass.Gym.ContactInfo.Address.City + ","
                            + userPass.Gym.ContactInfo.Address.TypeState.StateAbbreviation + "," + userPass.Gym.ContactInfo.Address.Zip,
                        phone = userPass.Gym.ContactInfo.Phone,


                        timeBought = userPass.LocalDateBought,
                        displayTimeBought = userPass.LocalDateBought.ToString("MMM d, yyyy").ToUpper(),
                        localGymDateTimeExpiration = userPass.LocalDateExpired.ToString("MMM d, yyyy").ToUpper(),
                        latitude = userPass.Gym.Position.Latitude ?? 0,
                        longitude = userPass.Gym.Position.Longitude ?? 0,
                        // user position not known so miles not known
                        // it isn't needed to be known here though
                        miles = 0,
                    };
                    user.purchasedGyms.Add(temp);

                }


                // TODO: ONCE TIMEZONE IS SAVED, FILTER OUT EXPIRED PASSES
                

                return new AllDataResponse { user = user, message = "Success!", success = true, status = 3, authToken = userItem.CurrentToken };
            }
        }


        

    }

   
}

