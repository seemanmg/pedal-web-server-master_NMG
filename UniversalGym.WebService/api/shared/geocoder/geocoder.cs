using Geocoding.Google;
using System.Linq;

namespace UniversalGym.Shared
{
    public static class Geocoder
    {
        private static readonly string ApiKey = Constants.GoogleApiKey;

        public static GeocodedLocation GeoCodeAddress(string target)
        {
            var geocoder = new GoogleGeocoder { ApiKey = ApiKey };
            var geocoded = geocoder.Geocode(target).ToList();

            if (!geocoded.Any()) return null;

            var latitude = geocoded.FirstOrDefault().Coordinates.Latitude;
            var longitude = geocoded.FirstOrDefault().Coordinates.Longitude;
            var rv = new GeocodedLocation {Latitude = latitude, Longitude = longitude};
            return rv;
        }

        
    }

    public class GeocodedLocation
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string GetPointString()
        {
            return string.Format("POINT({0} {1})", this.Longitude, this.Latitude);
        }
    }
}
