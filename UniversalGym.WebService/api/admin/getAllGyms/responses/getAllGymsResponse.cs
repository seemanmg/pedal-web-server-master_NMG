using System.Collections.Generic;
using System.Runtime.Serialization;

namespace UniversalGym.Responses
{
    public class getAllGymsResponse : BasicResponse
    {
        [DataMember(Name = "gyms")]
        public IList<Gyms> gyms { get; set; }

        [DataMember(Name = "totalStatsGyms")]
        public totalStatsGyms totalStatsGyms { get; set; }
        
        public getAllGymsResponse()
        {

        }
    }

    public class totalStatsGyms
    {

        [DataMember(Name = "GymPassCount")]
        public int GymPassCount { get; set; }

        [DataMember(Name = "TotalRevenue")]
        public int TotalRevenue { get; set; }

        [DataMember(Name = "TotalCosts")]
        public int TotalCosts { get; set; }

        [DataMember(Name = "TotalPayNotCollected")]
        public int TotalPayNotCollected { get; set; }

        [DataMember(Name = "TotalProfit")]
        public int TotalProfit { get; set; }


    }

    public class Gyms
    {
        [DataMember(Name = "GymId")]
        public string GymId { get; set; }

        [DataMember(Name = "GymName")]
        public string GymName { get; set; }

        [DataMember(Name = "GymInfo")]
        public string GymInfo { get; set; }

        [DataMember(Name = "ApplicationDate")]
        public string ApplicationDate { get; set; }

        [DataMember(Name = "IsActive")]
        public string IsActive { get; set; }

        [DataMember(Name = "CreditDollarValue")]
        public string CreditDollarValue { get; set; }

        [DataMember(Name = "GymGuid")]
        public string GymGuid { get; set; }

        [DataMember(Name = "IsApproved")]
        public string IsApproved { get; set; }

        [DataMember(Name = "Position")]
        public Position Position { get; set; }

        [DataMember(Name = "Url")]
        public string Url { get; set; }

        [DataMember(Name = "OwnerName")]
        public string OwnerName { get; set; }

        [DataMember(Name = "PriceToCharge")]
        public string PriceToCharge { get; set; }

        [DataMember(Name = "CurrentToken")]
        public string CurrentToken { get; set; }

        [DataMember(Name = "ScheduleUrl")]
        public string ScheduleUrl { get; set; }

        [DataMember(Name = "StripeUrl")]
        public string StripeUrl { get; set; }

        [DataMember(Name = "PublicContactInfo")]
        public ContactInfo PublicContactInfo { get; set; }

        [DataMember(Name = "OwnerContactInfo")]
        public ContactInfo OwnerContactInfo { get; set; }

        [DataMember(Name = "GymPhotoGalleries")]
        public IList<GymPhotoGallery> GymPhotoGalleries { get; set; }



        [DataMember(Name = "ActiveDate")]
        public string ActiveDate { get; set; }



        [DataMember(Name = "GymPassCount")]
        public int GymPassCount { get; set; }

        [DataMember(Name = "TotalRevenue")]
        public int TotalRevenue { get; set; }

        [DataMember(Name = "TotalCosts")]
        public int TotalCosts { get; set; }

        [DataMember(Name = "TotalPayNotCollected")]
        public int TotalPayNotCollected { get; set; }

        [DataMember(Name = "TotalProfit")]
        public int TotalProfit { get; set; }
        
    }

    public class Position
    { 
        [DataMember(Name = "latitude")]
        public string Latitude { get; set; }

        [DataMember(Name = "longitude")]
        public string Longitude { get; set; }
    }

    public class ContactInfo
    {
        [DataMember(Name = "ContactInfoId")]
        public string ContactInfoId { get; set; }

        [DataMember(Name = "Email")]
        public string Email { get; set; }

        [DataMember(Name = "Phone")]
        public string Phone { get; set; }

        [DataMember(Name = "Address")]
        public Address Address { get; set; }
    }

    public class Address
    {
        [DataMember(Name = "AddressId")]
        public string AddressId { get; set; }

        [DataMember(Name = "StreetLine1")]
        public string StreetLine1 { get; set; }

        [DataMember(Name = "StreetLine2")]
        public string StreetLine2 { get; set; }

        [DataMember(Name = "City")]
        public string City { get; set; }

        [DataMember(Name = "TypeState")]
        public TypeState TypeState { get; set; }

        [DataMember(Name = "Zip")]
        public string Zip { get; set; }
    }

    public class TypeState
    {
        [DataMember(Name = "TypeStateId")]
        public string TypeStateId { get; set; }

        [DataMember(Name = "StateName")]
        public string StateName { get; set; }

        [DataMember(Name = "StateAbbreviation")]
        public string StateAbbreviation { get; set; }
    }

    public class GymPhotoGallery
    {
        [DataMember(Name = "GymPhotoGalleryId")]
        public string GymPhotoGalleryId { get; set; }

        [DataMember(Name = "Photo")]
        public string Photo { get; set; }

        [DataMember(Name = "IsCoverPhoto")]
        public string IsCoverPhoto { get; set; }

    }


}

