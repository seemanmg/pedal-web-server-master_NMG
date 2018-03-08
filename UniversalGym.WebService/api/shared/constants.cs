using System;
using System.Configuration;
using System.Data.Entity;
using UniversalGym.Data;

namespace UniversalGym
{
    public class Constants
    {

        public static bool isApiStaging
        {
            get
            {
                string strIsApiStaging = @Environment.GetEnvironmentVariable("isApiStaging");
                if (strIsApiStaging == null)
                {
                    return true;
                }
                else if ( strIsApiStaging.Equals("yes") )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public static string cloudflareToken
        {
            get
            {
                return "30de0cb04d6b6723d79da696cd46ca89aa718";
            }
        }

        public static string cloudflareZone
        {
            get
            {
                return "4140fa542f282c2de960b5c3a9a9c224";
            }
        }
        
        public static bool isStripeTesting
        {
            get
            {
                string strIsStripeTesting = @Environment.GetEnvironmentVariable("isStripeTesting");
                if (strIsStripeTesting == null)
                {
                    return true;
                }
                else if ( strIsStripeTesting.Equals("yes") )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool isWebStaging
        {
            get
            {
                string strIsStripeTesting = @Environment.GetEnvironmentVariable("isWebStaging");
                if (strIsStripeTesting == null)
                {
                    return true;
                }
                else if ( strIsStripeTesting.Equals("yes") )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static String PedalWebUrl
        {
            get
            {
                if (isWebStaging)
                {
                    return "http://testgo.pedal.com/";
                }
                else
                {
                    return "http://pedal.com/";
                }
            }
        }

        public static String PedalAPIUrl
        {
            get
            {
                if (isApiStaging)
                {
                    return "https://testapi.pedal.com/GymService.svc/";
                }
                else
                {

                    return "https://api.pedal.com/GymService.svc/";
                }
            }
        }

        public static bool ReferralEnabled
        {
            get
            {
                return false;
            }
        }

        public static bool PromoEnabled
        {
            get
            {
                return false;
            }
        }

        public static String MarketingToken
        {
            get
            {
                return "8787c0cf-ede8-4f22-be6d-54bc02426a66";
            }
        }

        public static String InviteAFriendPromoCode
        {
            get
            {
                return "66655939-d460-4287-b78e-a8562ab8b00f";
            }
        }

        public static String FreeCode
        {
            get
            {
                return "exclusive";
            }
        }

        public static String LocalWebUrl
        {
            get
            {
                return "localhost:8080";
            }
        }

        public static String SubscriptionPlanID
        {
            get
            {
                return "base_plan";
            }
        }

        public static String GymRole
        {
            get
            {
                return "GymAdmin";
            }
        }

        public static String UserRole
        {
            get
            {
                return "User";
            }
        }

        public static String GymPicturesBlobContainerName
        {
            get
            {
                return "gympictures";
            }
        }

        public static String BlobStorageConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["AzurePhotoStorage"].ConnectionString;
            }
        }
      
        public static String GoogleApiKey
        {
            get
            {
                return "AIzaSyCmyp_JCVurL6J4wpw1feFBg7-RAz7Yj-w";
            }
        }

        public static String SlackHookUrl
        {
            get
            {
                return "https://hooks.slack.com/services/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";
            }
        }

        public static String StripeSecretKey
        {
            get
            {
                if (isStripeTesting)
                {
                    return "sk_test_3qEzFp2FOJD5jIG6TYWwZ0IO";
                }
                else
                {
                    return "sk_live_abcqeYuukzZB9fR4KSFlaTH7";
                }
            }
        }

        public static String StripePublishableKey
        {
            get
            {
                if (isStripeTesting)
                {
                    return "pk_test_iG4uOsIsAZ2Ml2RXywFCeip0";
                }
                else
                {
                    return "pk_live_hfbYrqkEDwpQ34FsJqulJm3X";
                }
            }
        }

        // pricing
        public static int returnGymPay(String tier)
        {
            if (tier.Equals("budget"))
            {
                return 250;
            }
            else if (tier.Equals("quality"))
            {
                return 350;
            }
            else if (tier.Equals("premium"))
            {
                return 450;
            }
            else
            {
                return 250;
            }
        }

        public static int returnGymPrice(String tier)
        {
            if (tier.Equals("budget"))
            {
                return 500;
            }
            else if (tier.Equals("quality"))
            {
                return 700;
            }
            else if (tier.Equals("premium"))
            {
                return 900;
            }
            else
            {
                return 500;
            }
        }

    }
}
 
 