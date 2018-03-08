using UniversalGym.Requests;
using UniversalGym.Responses;
using UniversalGym.UpdatePassword;
using System.ComponentModel;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace UniversalGym.API
{

    [ServiceContract(SessionMode = SessionMode.NotAllowed)]
    public interface IGymService
    {

        // FUNCTIONS FOR USERS


        // SIGNING UP
        
            
        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        AllDataResponse SignUp(AppSignUpRequest request);

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        BasicResponse AirbnbRegister(AirbnbRegisterRequest request);


        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare), OperationContract, Description("Description for GET /template1")]
        AllDataResponse NewLogin(UserDataRequest request);

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare)]
        BasicResponse resetUserPasswordEmail(ResetPasswordEmailRequest request);


        // PROFILES

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        BasicResponse UpdateProfile(UpdateProfileRequest request);

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        BasicResponse UpdateUserPassword(UpdatePasswordRequest request);




        // FINANCIALS
        

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        UpdateCardResponse UpdateCard(CreditCardDataRequest request);


        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        BasicResponse SuspendSubscription(BaseRequest request);


        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        PurchaseGymResponse PurchaseGym(PurchaseDayPassRequest request);


        // SEARCHING


        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        GymSearchListResponse SearchByLocation(SearchGymRequest request);



        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        GymSearchListResponse SearchGymsByCity(SearchGymRequest request);


        // GETTING INFORMATION TO SHOW USERS


        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        AllDataResponse GetUser(BaseRequest request);



        // FUNCTIONS FOR GYMS


        // LOGGING IN

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare), OperationContract, Description("Description for GET /template1")]
        GymDataResponse GymLogin(UserDataRequest request);



        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        GymDataResponse AddGymFromMarketing(RegisterGymRequest request);

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare)]
        BasicResponse resetGymPasswordEmail(ResetPasswordEmailRequest request);



        // GYM PROFILE INFORMATION

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare), OperationContract, Description("Description for GET /template1")]
        GymDataResponse GetGymInfo(BaseRequest request);


        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare), OperationContract, Description("Description for GET /template1")]
        BasicResponse UpdateGymInfo(UpdateGymInfoRequest request);


        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare), OperationContract, Description("Description for GET /template1")]
        GymCheckinsResponse GetGymCheckIns(GetGymCheckInsRequest request);


        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare), OperationContract, Description("Description for GET /template1")]
        addGymHoursResponse addGymSchedule(addGymHoursRequest request);


        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare), OperationContract, Description("Description for GET /template1")]
        BasicResponse removeGymHours(removeGymHoursRequest request);

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        BasicResponse UpdateGymPassword(UpdatePasswordRequest request);



        // GYM PHOTOS


        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        BasicResponse UpdateSelectedCoverPhoto(UpdateCoverPhotoRequest request);

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        BasicResponse DeletePhoto(UpdateCoverPhotoRequest request);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Upload/{token}/{accountId}", ResponseFormat = WebMessageFormat.Json)]
        UploadPictureResponse Upload(Stream file, string token, string accountId);



        // CONTRACTS

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ContractResponse ContractFromPriceTier(ContractFromPriceTierRequest request);

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        ContractResponse ContractFromGymId(BaseRequest request);





        // FINANCIALS


        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare)]
        BasicResponse UpdateBankInfo(BankAccountInfoRequest request);

        // ADMIN
        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare)]
        getAllGymsResponse getAllGyms(BaseRequest request);

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        getAllUsersResponse getAllUsers(BaseRequest request);

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        getAllGymPassesResponse getAllGymPasses(BaseRequest request);

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        getAllSearchRequestsResponse getAllSearchRequests(BaseRequest request);

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        getAllGiveCreditsResponse getAllGiveCredits(BaseRequest request);

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        getAllGymInvoicesResponse getAllGymInvoices(BaseRequest request);

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        getAllLogsResponse getAllLogs(getAllLogsRequest request);

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare)]
        BasicResponse deleteGym(deleteGymRequest request);



        // SHARED FUNCTIONS

        [OperationContract, WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
             BodyStyle = WebMessageBodyStyle.Bare)]
        BasicResponse contactForm(ContactFormRequest request);


    }




}

