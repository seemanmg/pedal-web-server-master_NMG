using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UniversalGym.Slack
{

    public static class SlackHelper
    {
        public static void sendErrorChannel(string message)
        {
            sendMessageAsync(message, "#errors");   
        }

        public static void sendActionChannel(string message)
        {
            if (Constants.isApiStaging)
            {
                sendMessageAsync(message, "#actions_test");
            }
            else
            {
                sendMessageAsync(message, "#actions_production");
            }
        }

        public static void sendPassChannel(string message, string latitude, string longitude)
        {
            if (Constants.isApiStaging)
            {
                sendMessageAsyncWithImage(message, "#payments_test", getMapUrl(latitude, longitude), "Pass");
            }
            else
            {
                sendMessageAsyncWithImage(message, "#payments_production", getMapUrl(latitude, longitude), "Pass");
            }
        }

        public static void sendGiveCreditChannel(string message)
        {
            if (Constants.isApiStaging)
            {
                sendMessageAsync(message, "#payments_test");
            }
            else
            {
                sendMessageAsync(message, "#payments_production");
            }
        }

        public static void sendSearchChannel(string message, string latitude, string longitude)
        {
            if (Constants.isApiStaging)
            {
                sendMessageAsyncWithImage(message, "#searches_test", getMapUrl(latitude, longitude), "Search");
            }
            else
            {
                sendMessageAsyncWithImage(message, "#searches_production", getMapUrl(latitude, longitude), "Search");
            }
        }

        public static void sendSearchChannelNotFound(string message)
        {
            if (Constants.isApiStaging)
            {
                sendMessageAsync(message, "#searches_test");
            }
            else
            {
                sendMessageAsync(message, "#searches_production");
            }
        }

        public static void sendSupportChannel(string message)
        {
            if (Constants.isApiStaging)
            {
                sendMessageAsync(message, "#support_test");
            }
            else
            {
                sendMessageAsync(message, "#support_production");
            }
        }

        public static void sendUserSignupChannel(string message)
        {
            if (Constants.isApiStaging)
            {
                sendMessageAsync(message, "#signups_test");
            }
            else
            {
                sendMessageAsync(message, "#signups_production");
            }
        }

        public static void sendGymSignupChannel(string message, string latitude, string longitude)
        {
            if (Constants.isApiStaging)
            {
                sendMessageAsyncWithImage(message, "#signups_test", getMapUrl(latitude, longitude), "Gym Location");
            }
            else
            {
                sendMessageAsyncWithImage(message, "#signups_production", getMapUrl(latitude, longitude), "Gym Location");
            }
        }

        private static string getMapUrl(string latitude, string longitude)
        {
            var mapUrl = "http://maps.googleapis.com/maps/api/staticmap?center="
                + latitude
                + ","
                + longitude
                + "&size=250x120&sensor=false&zoom=8&maptype=roadmap&markers=icon:http://pictures.pedal.com/frontend/Pedal-Marker.png%7C"
                + latitude
                + ","
                + longitude
                + "&style=feature:landscape&key=AIzaSyCmyp_JCVurL6J4wpw1feFBg7-RAz7Yj-w";

            return mapUrl;
        }

        private static async void sendMessageAsync(
            string message,
            string channel)
        {

            message =
                "Is API staging: "
                + Constants.isApiStaging
                + Environment.NewLine
                + message;


            var payload = new
            {
                text = message,
                channel = channel,
           
            };

            
            var httpClient = new HttpClient();
            var serializedPayload = JsonConvert.SerializeObject(payload);
            var webHookUrl = new Uri(Constants.SlackHookUrl);
            var response = await httpClient.PostAsync(webHookUrl,
                new StringContent(serializedPayload, Encoding.UTF8, "application/json"));
        }

        private static async void sendMessageAsyncWithImage(
            string message,
            string channel,
            string imageUrl,
            string imageTitle)
        {

            message =
                "Is API staging: "
                + Constants.isApiStaging
                + Environment.NewLine
                + message;


            var payload = new
            {
                text = message,
                channel = channel,
                attachments = new[] { new { image_url = imageUrl, title = imageTitle } }
            };


            var httpClient = new HttpClient();
            var serializedPayload = JsonConvert.SerializeObject(payload);
            var webHookUrl = new Uri(Constants.SlackHookUrl);
            var response = await httpClient.PostAsync(webHookUrl,
                new StringContent(serializedPayload, Encoding.UTF8, "application/json"));
        }
    }

}

