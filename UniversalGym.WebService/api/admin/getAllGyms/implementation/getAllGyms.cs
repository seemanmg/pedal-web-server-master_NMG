using UniversalGym.Responses;
using UniversalGym.Requests;
using UniversalGym.Data;
using System.Linq;
using System.Collections.Generic;
using System;

namespace UniversalGym.Admin
{
    public class getAllGyms
    {
        public static getAllGymsResponse getAllGymsImplementation(BaseRequest request)
        {


            if (request.authToken != Constants.MarketingToken)
                return new getAllGymsResponse() { success = false, status = 404, message = "Wrong Token" };

            var rv = new getAllGymsResponse();
            rv.totalStatsGyms = new Responses.totalStatsGyms();
            rv.totalStatsGyms.GymPassCount = 0;
            rv.totalStatsGyms.TotalRevenue = 0;
            rv.totalStatsGyms.TotalCosts = 0;
            rv.totalStatsGyms.TotalPayNotCollected = 0;
            rv.totalStatsGyms.TotalProfit = 0;


            rv.gyms = new List<Gyms>();
            using (var db = new UniversalGymEntities())
            {
                var gyms = db.Gyms.ToList();
                foreach (var gym in gyms)
                {
                    var temp = new Gyms
                    {
                        GymId = gym.GymId.ToString() ?? "NULL",
                        GymName = gym.GymName ?? "NULL",
                        GymInfo = gym.GymInfo ?? "NULL",
                        ApplicationDate = gym.ApplicationDate.ToString() ?? "NULL",
                        IsActive = gym.IsActive.ToString() ?? "NULL",
                        CreditDollarValue = gym.CreditDollarValue.ToString() ?? "NULL",
                        GymGuid = gym.GymGuid.ToString() ?? "NULL",
                        IsApproved = gym.IsApproved.ToString() ?? "NULL",
                        Url = gym.Url ?? "NULL",
                        OwnerName = gym.OwnerName ?? "NULL",
                        PriceToCharge = gym.PriceToCharge.ToString() ?? "NULL",
                        CurrentToken = gym.CurrentToken ?? "NULL",
                        ScheduleUrl = gym.ScheduleUrl ?? "NULL",
                        StripeUrl = gym.StripeUrl ?? "NULL",
                        ActiveDate = gym.ActiveDate.ToString() ?? "NULL",

                    };
                    // Set Position
                    if (gym.Position != null)
                    {
                        temp.Position = new Position();
                        temp.Position.Latitude = gym.Position.Latitude.ToString() ?? "NULL";
                        temp.Position.Longitude = gym.Position.Longitude.ToString() ?? "NULL";
                    }

                    // Set Public Contact
                    temp.PublicContactInfo = new Responses.ContactInfo();
                    temp.PublicContactInfo.ContactInfoId = gym.ContactInfo1.ContactInfoId.ToString() ?? "NULL";
                    temp.PublicContactInfo.Email = gym.ContactInfo1.Email ?? "NULL";
                    temp.PublicContactInfo.Phone = gym.ContactInfo1.Phone ?? "NULL";

                    if (gym.ContactInfo1.Address != null)
                    {

                        // Set Public Contact Address
                        temp.PublicContactInfo.Address = new Responses.Address();
                        temp.PublicContactInfo.Address.AddressId = gym.ContactInfo1.Address.AddressId.ToString() ?? "NULL";
                        temp.PublicContactInfo.Address.StreetLine1 = gym.ContactInfo1.Address.StreetLine1 ?? "NULL";
                        temp.PublicContactInfo.Address.StreetLine2 = gym.ContactInfo1.Address.StreetLine2 ?? "NULL";
                        temp.PublicContactInfo.Address.City = gym.ContactInfo1.Address.City ?? "NULL";
                        temp.PublicContactInfo.Address.Zip = gym.ContactInfo1.Address.Zip ?? "NULL";

                        // Set Public Contact Address State
                        temp.PublicContactInfo.Address.TypeState = new Responses.TypeState();
                        temp.PublicContactInfo.Address.TypeState.TypeStateId = gym.ContactInfo1.Address.TypeState.TypeStateId.ToString() ?? "NULL";
                        temp.PublicContactInfo.Address.TypeState.StateName = gym.ContactInfo1.Address.TypeState.StateName ?? "NULL";
                        temp.PublicContactInfo.Address.TypeState.StateAbbreviation = gym.ContactInfo1.Address.TypeState.StateAbbreviation ?? "NULL";

                    }

                    // Set Owner Contact
                    temp.OwnerContactInfo = new Responses.ContactInfo();
                    temp.OwnerContactInfo.ContactInfoId = gym.ContactInfo.ContactInfoId.ToString() ?? "NULL";
                    temp.OwnerContactInfo.Email = gym.ContactInfo.Email ?? "NULL";
                    temp.OwnerContactInfo.Phone = gym.ContactInfo.Phone ?? "NULL";

                    if (gym.ContactInfo.Address != null)
                    {
                        // Set Owner Contact Address
                        temp.OwnerContactInfo.Address = new Responses.Address();
                        temp.OwnerContactInfo.Address.AddressId = gym.ContactInfo.Address.AddressId.ToString() ?? "NULL";
                        temp.OwnerContactInfo.Address.StreetLine1 = gym.ContactInfo.Address.StreetLine1 ?? "NULL";
                        temp.OwnerContactInfo.Address.StreetLine2 = gym.ContactInfo.Address.StreetLine2 ?? "NULL";
                        temp.OwnerContactInfo.Address.City = gym.ContactInfo.Address.City ?? "NULL";
                        temp.OwnerContactInfo.Address.Zip = gym.ContactInfo.Address.Zip ?? "NULL";

                        // Set Owner Contact Address State
                        temp.OwnerContactInfo.Address.TypeState = new Responses.TypeState();
                        temp.OwnerContactInfo.Address.TypeState.TypeStateId = gym.ContactInfo.Address.TypeState.TypeStateId.ToString() ?? "NULL";
                        temp.OwnerContactInfo.Address.TypeState.StateName = gym.ContactInfo.Address.TypeState.StateName ?? "NULL";
                        temp.OwnerContactInfo.Address.TypeState.StateAbbreviation = gym.ContactInfo.Address.TypeState.StateAbbreviation ?? "NULL";
                    }
                    // Set GymPhotoGalleries
                    temp.GymPhotoGalleries = new List<Responses.GymPhotoGallery>();
                    foreach (var thisGymPhotoGallery in gym.GymPhotoGalleries)
                    {
                        var tempGymPhotoGallery = new Responses.GymPhotoGallery
                        {
                            GymPhotoGalleryId = thisGymPhotoGallery.GymPhotoGalleryId.ToString() ?? "NULL",
                            IsCoverPhoto = thisGymPhotoGallery.IsCoverPhoto.ToString() ?? "NULL",
                            Photo = thisGymPhotoGallery.Photo ?? "NULL",
                        };

                        temp.GymPhotoGalleries.Add(tempGymPhotoGallery);
                    }

                    temp.GymPassCount = gym.GymPasses.Count;
                    rv.totalStatsGyms.GymPassCount = rv.totalStatsGyms.GymPassCount + temp.GymPassCount;

                    if (gym.GymPasses.Count > 0)
                    {
                        var totalRevenue =
                            from gp in gym.GymPasses
                            group gp by 1 into g
                            select new
                            {
                                totalCredit = g.Sum(x => x.CreditsUsed),
                                totalCharged = g.Sum(x => x.AmountCharged),
                                totalCost = g.Sum(x => x.GymPassCost)
                            };

                        temp.TotalRevenue = totalRevenue.SingleOrDefault().totalCredit + totalRevenue.SingleOrDefault().totalCharged;
                        temp.TotalCosts = totalRevenue.SingleOrDefault().totalCost;
                        temp.TotalProfit =
                            (totalRevenue.SingleOrDefault().totalCredit + totalRevenue.SingleOrDefault().totalCharged)
                            - totalRevenue.SingleOrDefault().totalCost;

                        rv.totalStatsGyms.TotalRevenue = rv.totalStatsGyms.TotalRevenue + temp.TotalRevenue;
                        rv.totalStatsGyms.TotalCosts = rv.totalStatsGyms.TotalCosts + temp.TotalCosts;
                        rv.totalStatsGyms.TotalProfit = rv.totalStatsGyms.TotalProfit + temp.TotalProfit;

                        if (gym.GymInvoices.Count > 0)
                        {
                            int totalCollected = gym.GymInvoices.Where(t => t.IsCollected == true).Select(t => t.AmountPaid).DefaultIfEmpty(0).Sum();
                            temp.TotalPayNotCollected = totalRevenue.SingleOrDefault().totalCost - totalCollected;
                            rv.totalStatsGyms.TotalPayNotCollected = rv.totalStatsGyms.TotalPayNotCollected + temp.TotalPayNotCollected;

                        }
                    }

                    rv.gyms.Add(temp);
                }

            }
            rv.success = true;
            rv.message = "";

            return rv;
        }
    }
}