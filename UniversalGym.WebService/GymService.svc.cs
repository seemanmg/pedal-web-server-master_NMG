using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Activation;
using UniversalGym.Requests;
using UniversalGym.Responses;
using UniversalGym.UpdatePassword;

namespace UniversalGym.API
{

    //[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    //[ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class GymService : IGymService
    {

        // FUNCTIONS FOR USERS


        // SIGNING UP

        // user registration from app
        public AllDataResponse SignUp(AppSignUpRequest request)
        {
            try
            {
                Logs.LogsInsertAction("User signup attempted");
                var validResponse = Users.appSignUp.appSignUpImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("User signup success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("User signup failure: " + validResponse.message);
                }

                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new AllDataResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // user registration from airbnb
        public BasicResponse AirbnbRegister(AirbnbRegisterRequest request)
        {
            try
            {
                Logs.LogsInsertAction("User airbnb register attempted");
                var validResponse = Users.AirbnbRegister.airbnbRegisterImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("User airbnb register success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("User airbnb register failure: " + validResponse.message);
                }

                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new BasicResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // Send email to reset password for user

        public BasicResponse resetUserPasswordEmail(ResetPasswordEmailRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Reset user password attempted");
                var validResponse = Users.resetUserPasswordEmail.resetUserPasswordEmailImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Reset user password success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Reset user password failure: " + validResponse.message);
                }
                return validResponse;
            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new BasicResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // User Logging in
        public AllDataResponse NewLogin(UserDataRequest request)
        {

            try
            {
                Logs.LogsInsertAction("User login attempted");
                var validResponse = Users.userLogin.userLoginImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("User login success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("User login failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new AllDataResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }


        // PROFILES

        // update user profile
        public BasicResponse UpdateProfile(UpdateProfileRequest request)
        {
            try
            {
                Logs.LogsInsertAction("User update profile attempted");
                var validResponse = Users.updateProfile.updateProfileImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("User update profile success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("User update profile failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new BasicResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }


        // update user password from profile or app
        public BasicResponse UpdateUserPassword(UpdatePasswordRequest request)
        {
            try
            {
                Logs.LogsInsertAction("User update password attempted");
                var validResponse = updateUserPassword.updateUserPasswordImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("User update password success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("User update password failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new BasicResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }


        // FINANCIALS

        // add/updates credit card
        public UpdateCardResponse UpdateCard(CreditCardDataRequest request)
        {
            try
            {
                Logs.LogsInsertAction("User credit card attempted");
                var validResponse = Users.updateCard.updateCardImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("User credit card success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("User credit card failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new UpdateCardResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }

        }

        // cancels subscription
        public BasicResponse SuspendSubscription(BaseRequest request)
        {
            try
            {
                Logs.LogsInsertAction("User suspend subscription attempted");
                var validResponse = Users.suspendSubscription.suspendSubscriptionImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("User suspend subscription success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("User suspend subscription failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new BasicResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        public PurchaseGymResponse PurchaseGym(PurchaseDayPassRequest request)
        {
            try
            {
                Logs.LogsInsertAction("User purchase gym pass attempted");
                var validResponse = Users.purchaseGym.purchaseGymImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("User purchase gym pass success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("User purchase gym pass failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new PurchaseGymResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }


        // SEARCHING


        // when app is first opened, search by user location
        public GymSearchListResponse SearchByLocation(SearchGymRequest request)
        {
            try
            {
                Logs.LogsInsertAction("User search by location attempted");
                var validResponse = Users.searchByLocation.searchByLocationImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("User search by location success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("User search by location failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new GymSearchListResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }

        }

        // search by city
        public GymSearchListResponse SearchGymsByCity(SearchGymRequest request)
        {
            try
            {
                Logs.LogsInsertAction("User search by city attempted");
                var validResponse = Users.searchGymsByCity.searchGymsByCityImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("User search by city success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("User search by city failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new GymSearchListResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }
        

        // ADMIN FUNCTIONS

        // get all gyms in database (only used for admin) 
        public getAllGymsResponse getAllGyms(BaseRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Admin get all gyms attempted");
                var validResponse = Admin.getAllGyms.getAllGymsImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Admin get all gyms success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Admin get all gyms failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new getAllGymsResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // get all users in database (only used for admin) 
        public getAllUsersResponse getAllUsers(BaseRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Admin get all users attempted");
                var validResponse = Admin.getAllUsers.getAllUsersImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Admin get all users success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Admin get all users failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new getAllUsersResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // get all gymPasses in database (only used for admin)
        public getAllGymPassesResponse getAllGymPasses(BaseRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Admin get all gym passes attempted");
                var validResponse = Admin.getAllGymPasses.getAllGymPassesImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Admin get all gym passes success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Admin get all gym passes failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new getAllGymPassesResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }        
        
        // get all searchRequest in database (only used for admin)
        public getAllSearchRequestsResponse getAllSearchRequests(BaseRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Admin get all search requests attempted");
                var validResponse = Admin.getAllSearchRequests.getAllSearchRequestsImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Admin get all search requests success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Admin get all search requests failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new getAllSearchRequestsResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // get all giveCredit in database (only used for admin)
        public getAllGiveCreditsResponse getAllGiveCredits(BaseRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Admin get all give credits attempted");
                var validResponse = Admin.getAllGiveCredits.getAllGiveCreditsImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Admin get all give credits success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Admin get all give credits failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new getAllGiveCreditsResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // get all gymInvoices in database (only used for admin)
        public getAllGymInvoicesResponse getAllGymInvoices(BaseRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Admin get all gym invoices attempted");
                var validResponse = Admin.getAllGymInvoices.getAllGymInvoicesImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Admin get all gym invoices success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Admin get all gym invoices failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new getAllGymInvoicesResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // get all logs in database (only used for admin)
        public getAllLogsResponse getAllLogs(getAllLogsRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Admin get all logs attempted");
                var validResponse = Admin.getAllLogs.getAllLogsImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Admin get all logs success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Admin get all logs failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new getAllLogsResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // delete a gym in database (only used for admin)
        public BasicResponse deleteGym(deleteGymRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Admin delete gym attempted");
                var validResponse = Admin.deleteGym.deleteGymImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Admin delete gym success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Admin delete gym failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new getAllLogsResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }





        // GETTING INFORMATION TO SHOW USERS


        // retreive user for displaying information (already logged in)
        public AllDataResponse GetUser(BaseRequest request)
        {
            try
            {
                Logs.LogsInsertAction("User get user attempted");
                var validResponse = Users.getUser.getUserImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("User get user success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("User get user failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new AllDataResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }



        // FUNCTIONS FOR GYMS


        // LOGGING IN

        // gym login
        public GymDataResponse GymLogin(UserDataRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Gym login attempted");
                var validResponse = Gym.gymLogin.gymLoginImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Gym login success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Gym login failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new GymDataResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // register gym
        public GymDataResponse AddGymFromMarketing(RegisterGymRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Gym signup attempted");
                var validResponse = Gym.gymRegister.gymRegisterImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Gym signup success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Gym signup failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new GymDataResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }



        // GYM PROFILE INFORMATION

        // Get gym profile
        public GymDataResponse GetGymInfo(BaseRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Gym get profile attempted");
                var validResponse = Gym.getGymInfo.getGymInfoImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Gym get profile success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Gym get profile failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new GymDataResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // update gym password from profile
        public BasicResponse UpdateGymPassword(UpdatePasswordRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Gym update password attempted");
                var validResponse = updateGymPassword.updateGymPasswordImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Gym update password success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Gym update password failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new BasicResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // Send email to reset password for gyms

        public BasicResponse resetGymPasswordEmail(ResetPasswordEmailRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Reset gym password attempted");
                var validResponse = Gym.resetGymPasswordEmail.resetGymPasswordEmailImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Reset gym password success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Reset gym password failure: " + validResponse.message);
                }
                return validResponse;
            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new BasicResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // set gym profile
        public BasicResponse UpdateGymInfo(UpdateGymInfoRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Gym update profile attempted");
                var validResponse = Gym.updateGymInfo.updateGymInfoImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Gym update profile success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Gym update profile failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new BasicResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        //  get all the checkins
        public GymCheckinsResponse GetGymCheckIns(GetGymCheckInsRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Gym get checkins attempted");
                var validResponse = Gym.getGymCheckIns.getGymCheckInsImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Gym get checkins success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Gym get checkins failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new GymCheckinsResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // add Gym hours
        public addGymHoursResponse addGymSchedule(addGymHoursRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Add gym hours attempted");
                var validResponse = Gym.addGymHours.addGymHoursImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Add gym hours success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Add gym hours failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new addGymHoursResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // remove gym hours
        public BasicResponse removeGymHours(removeGymHoursRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Remove gym hours attempted");
                var validResponse = Gym.removeGymHours.removeGymHoursImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Remove gym hours success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Remove gym hours failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new addGymHoursResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // GYM PHOTOS

        // set photo
        public BasicResponse UpdateSelectedCoverPhoto(UpdateCoverPhotoRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Gym update cover photo attempted");
                var validResponse = Gym.updateSelectedCoverPhoto.updateSelectedCoverPhotoImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Gym update cover photo success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Gym update cover photo failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new BasicResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // delete photo
        public BasicResponse DeletePhoto(UpdateCoverPhotoRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Gym delete photo attempted");
                var validResponse = Gym.deletePhoto.deletePhotoImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Gym delete photo success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Gym delete photo failure: " + validResponse.message);
                }
                return validResponse;

            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new BasicResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }


        //// upload photo
        public UploadPictureResponse Upload(Stream file, string token, string accountId)
        {
            try
            {
                Logs.LogsInsertAction("Gym upload photo attempted");
                var validResponse = Gym.uploadPhoto.uploadPhotoImplementation(file, token, accountId);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Gym upload photo success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Gym upload photo failure: " + validResponse.message);
                }
                return validResponse;
            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new UploadPictureResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

    


        // FINANCIALS

        // Bank ach information for gyms to get paid
        public BasicResponse UpdateBankInfo(BankAccountInfoRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Gym ach attempted");
                var validResponse = Gym.updateBankInfo.updateBankInfoImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Gym ach success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Gym ach failure: " + validResponse.message);
                }
                return validResponse;
            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new BasicResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // CONTRACTS

        // Get contract based on pricetier for gyms still registering
        public ContractResponse ContractFromPriceTier(ContractFromPriceTierRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Gym signup contract attempted");
                var validResponse = Gym.contractFromPriceTier.contractFromPriceTierImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Gym signup contract success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Gym signup contract failure: " + validResponse.message);
                }
                return validResponse;
            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new ContractResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // Get contract based on gymId in database
        public ContractResponse ContractFromGymId(BaseRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Gym contract attempted");
                var validResponse = Gym.contractFromGymId.contractFromGymIdImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Gym contract success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Gym contract failure: " + validResponse.message);
                }
                return validResponse;
            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new ContractResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }

        // SHARED

        // Send email from contact form

        public BasicResponse contactForm(ContactFormRequest request)
        {
            try
            {
                Logs.LogsInsertAction("Contact form attempted");
                var validResponse = Shared.contactForm.contactFormImplementation(request);
                if (validResponse.success == true)
                {
                    Logs.LogsInsertAction("Contact form success: " + validResponse.message);
                }
                else
                {
                    Logs.LogsInsertAction("Contact form failure: " + validResponse.message);
                }
                return validResponse;
            }
            catch (Exception exception)
            {
                Logs.LogsInsertError(exception);
                return new BasicResponse { message = "An error occured. The Pedal team has been notified.", success = false };
            }
        }
        
    } 
}

