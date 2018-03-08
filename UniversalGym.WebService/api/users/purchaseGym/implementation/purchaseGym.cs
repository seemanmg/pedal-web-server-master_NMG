using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using UniversalGym.Credit;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;
using UniversalGym.StripeHelper;
using UniversalGym.Slack;

namespace UniversalGym.Users
{
    public class purchaseGym
    {

        private static string intToMoney(int money)
        {
            decimal result = money / 100m;
            string formatted = result.ToString("c");
            return formatted;
        }
        public static PurchaseGymResponse purchaseGymImplementation(PurchaseDayPassRequest request)
        {
            if (request == null || String.IsNullOrWhiteSpace(request.authToken) || request.accountId == null)
            {
                return new PurchaseGymResponse
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
                    return new PurchaseGymResponse
                    {
                        message = "User not found.",
                        status = 404,
                        success = false,
                    };
                }

                if (String.IsNullOrWhiteSpace(user.StripeUrl))
                {
                    return new PurchaseGymResponse { message = "Please add a credit card.", status = 703, success = false };
                }
                var gym = db.Gyms.SingleOrDefault(w => w.GymId == request.gymId);
                if (gym == null)
                {
                    return new PurchaseGymResponse { message = "Gym does not exist.", status = 701, success = false };
                }

                var creditsUsed = new CreditUse(db).UseCredit(user, gym.PriceToCharge);
                var amountToCharge = gym.PriceToCharge - creditsUsed;

                if (amountToCharge > 0)
                {

                    new StripeCharge().ChargeCardForPass(user.StripeUrl, amountToCharge, gym.GymName);


                }
                var locationString = "location=" + gym.Position.Latitude + ", " + gym.Position.Longitude;

                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                TimeSpan diff = DateTime.UtcNow.ToUniversalTime() - origin;
                var timeStamp = Math.Floor(diff.TotalSeconds);
                var timeStampString = "timestamp=" + timeStamp;
                var googleUrl = "https://maps.googleapis.com/maps/api/timezone/json?key=" + Constants.GoogleApiKey + "&" + timeStampString + "&" + locationString;

                Uri uri = new Uri(googleUrl);
                WebRequest webRequest = WebRequest.Create(uri);
                WebResponse response = webRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                String responseData = streamReader.ReadToEnd();
                var outObject = JsonConvert.DeserializeObject<GoogleTimeZoneResponse>(responseData);
                DateTime gymLocalDateTime = DateTime.UtcNow.AddSeconds(Convert.ToDouble(outObject.dstOffset) + Convert.ToDouble(outObject.rawOffset));

                var gymLocalEndOfDay = new DateTime(gymLocalDateTime.Year, gymLocalDateTime.Month, gymLocalDateTime.Day, 0, 0, 0, 000);
                gymLocalEndOfDay = gymLocalEndOfDay.AddDays(1);

                var item = new GymPass
                {
                    UserId = user.UserId,
                    GymId = request.gymId,
                    LocalDateBought = gymLocalDateTime,
                    LocalDateExpired = gymLocalEndOfDay,
                    CreditsUsed = creditsUsed,
                    ServerTimeDateBought = DateTime.Now,
                    AmountCharged = amountToCharge,
                    GymPassCost = gym.CreditDollarValue,

                };
                db.GymPasses.Add(item);
                db.SaveChanges();

                var passText = "Pass Purchase"
                + Environment.NewLine
                + "User: "
                + item.User.Email
                + Environment.NewLine
                + "Gym: "
                + item.Gym.GymName
                + Environment.NewLine
                + "Phone Number: "
                + item.Gym.ContactInfo.Phone
                + Environment.NewLine
                + "LocalDateBought: "
                + item.LocalDateBought
                + Environment.NewLine
                + "+ Credits Used: "
                + intToMoney(item.CreditsUsed)
                + Environment.NewLine
                + "+ Amount Charged: "
                + intToMoney(item.AmountCharged)
                + Environment.NewLine
                + "- Gym Pass Cost: "
                + intToMoney(item.GymPassCost);

                SlackHelper.sendPassChannel(passText, item.Gym.Position.Latitude.ToString(), item.Gym.Position.Longitude.ToString());


                return new PurchaseGymResponse
                {
                    message = "Pass Activated.",
                    status = 200,
                    success = true,
                    newCreditValue = user.Credits,
                    localGymDateTimeBought = gymLocalDateTime.ToString("MMM d, yyyy").ToUpper(),
                    localGymDateTimeExpiration = gymLocalEndOfDay.ToString("MMM d, yyyy").ToUpper()
                };




            }
        }

    }


}