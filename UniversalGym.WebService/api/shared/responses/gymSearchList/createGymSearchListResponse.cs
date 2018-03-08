using System.Collections.Generic;
using System.Linq;
using UniversalGym.Responses;
using System.Device.Location;

namespace UniversalGym.Users
{

    public class GymSearchListHelper
    {
        public static GymSearchListResponse createGymSearchListResponse(IEnumerable<Data.Gym> gyms, double? userLat, double? userLong)
        {

            // only gyms that are active and approved and have their descriptions filled out
            // at some point gyms were defaulted with Coming Soon! as their description



            var gymResults = gyms.Where(w => w.IsActive && w.IsApproved && !w.GymInfo.Equals("COMING SOON!")).Select(gym => new GymResponse()
            {
                name = gym.GymName,
                description = gym.GymInfo,
                pictureUrl = getPictureUrl(gym.GymPhotoGalleries),
                gymId = gym.GymId,
                price = gym.PriceToCharge,
                city = gym.ContactInfo1.Address.City,
                state = gym.ContactInfo1.Address.TypeState.StateAbbreviation,
                gymUrl = gym.Url,
                isActive = gym.IsActive,
                address = gym.ContactInfo1.Address.StreetLine1 + " " + gym.ContactInfo1.Address.StreetLine2 + "," +
                        gym.ContactInfo1.Address.City + ","
                         + gym.ContactInfo1.Address.TypeState.StateAbbreviation + "," + gym.ContactInfo1.Address.Zip,
                phone = gym.ContactInfo1.Phone,
                latitude = gym.Position.Latitude ?? 0,
                longitude = gym.Position.Longitude ?? 0,
                miles = getMilesFromUser(
                    gym.Position.Latitude ?? 0,
                    gym.Position.Longitude ?? 0,
                    userLat ?? 0,
                    userLong ?? 0),

            }).Take(15).ToList();


            var rv = new GymSearchListResponse
            {

                status = 200,
                success = true,
                message = "Success!",
                results = gymResults,
            };

            return rv;
        }

        public static double getMilesFromUser(double gymLat, double gymLong, double userLat, double userLong)
        {
            var userCoord = new GeoCoordinate(userLat, userLong);
            var gymCoord = new GeoCoordinate(gymLat, gymLong);
            // answer returned in meters, convert to miles
            return userCoord.GetDistanceTo(gymCoord) / 1609.344;
        }

        public static List<string> getPictureUrl(IEnumerable<Data.GymPhotoGallery> gymPhotoGalleries)
        {
            // sort with cover photo first followed by any other photos
            var gymPhotoGalleriesOrdered = gymPhotoGalleries.OrderByDescending(e => e.IsCoverPhoto);

            var pictureUrlArray = new List<string>();

            foreach (var gymPhotoGallery in gymPhotoGalleriesOrdered)
            {
                pictureUrlArray.Add(gymPhotoGallery.Photo);
            }

            if (pictureUrlArray.Count == 0)
            {
                pictureUrlArray.Add("http://d39kbiy71leyho.cloudfront.net/wp-content/uploads/2016/05/09170020/cats-politics-TN.jpg");
            }

            return pictureUrlArray;
        }
    }



}
