namespace UniversalGym
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UniversalGym.Data;
    using UniversalGym.Responses;
    using UniversalGym.Users;

    public class gymDataHelper
    {
        public static GymDataResponse CreateGymDataResponse(int gymId, bool genNewToken)
        {
            using (var db = new UniversalGymEntities())
            {

                var gym = db.Gyms.First(f => f.GymId == gymId);

                var baseRefUrl = "";
                if (genNewToken)
                {
                    gym.CurrentToken = Guid.NewGuid().ToString();
                    db.SaveChanges();
                }



                //gym photo
                var gymPhotoList = db.GymPhotoGalleries
                    .Where(w => w.GymId == gymId)
                    .OrderByDescending(o => o.IsCoverPhoto)
                    .Select(s => new PictureObject
                    {
                        url = s.Photo,
                        isCover = s.IsCoverPhoto,
                        id = s.GymPhotoGalleryId,
                    }).ToList();
                var gymPhotos = gymPhotoList.Any() ? gymPhotoList : new List<PictureObject> { };
                var hasRightGymPhotos = false;
                if (gymPhotos.Count <= 8 && gymPhotos.Count >= 3)
                {
                    hasRightGymPhotos = true;
                }


                //gym schedules
                var gymSchedulesList = db.GymSchedules
                    .Where(w => w.GymId == gymId)
                    .OrderBy(w => w.TypeWeekDay.Order)
                    .ThenBy(n => n.StartTime)
                    .Select(s => new Schedule
                    {
                        day = s.TypeWeekDay.TypeDescription,
                        startTime = new gymHour
                        {
                            hour = s.StartTime.Hour,
                            minute = s.StartTime.Minute,
                        },
                        endTime = new gymHour
                        {
                            hour = s.EndTime.Hour,
                            minute = s.EndTime.Minute,
                        },
                        id = s.GymScheduleId,
                    }).ToList();
                var gymSchedules = gymSchedulesList.Any() ? gymSchedulesList : new List<Schedule> { };
                var hasRightSchedules = false;
                if (gymSchedules.Count > 0)
                {
                    hasRightSchedules = true;
                }

                var startingPaymentDate = "";
                var endingPaymentDate = "";
                var currentPaymentBalance = 0;
                var collectPaymentBalance = 0;

                if (gym.IsApproved && !gym.badData)
                {
                    // equal to same time as endingpaymentdate of last invoice
                    startingPaymentDate = "";

                    // starting date + 14 days?
                    endingPaymentDate = "";

                    // all gympass costs in above time frame
                    currentPaymentBalance = 0;

                    // all previous gym invoices with isCollected set to false
                    collectPaymentBalance = 0;
                }


                return new GymDataResponse
                {
                    address = gym.ContactInfo1.Address.StreetLine1,
                    token = gym.CurrentToken,
                    city = gym.ContactInfo1.Address.City,
                    state = gym.ContactInfo1.Address.TypeState.StateAbbreviation,
                    zip = gym.ContactInfo1.Address.Zip,
                    isApproved = gym.IsApproved,
                    contactName = gym.OwnerName,
                    contactPhone = gym.ContactInfo.Phone,
                    contactEmail = gym.ContactInfo.Email,
                    gymPhone = gym.ContactInfo1.Phone,
                    description = gym.GymInfo,
                    gymId = gym.GymId,
                    gymUrl = gym.Url,
                    scheduleUrl = gym.ScheduleUrl,
                    gymName = gym.GymName,
                    status = 200,
                    success = true,
                    message = "Success!",
                    price = gym.CreditDollarValue.ToString(),
                    pictures = gymPhotos,
                    hasAccount = !String.IsNullOrWhiteSpace(gym.StripeUrl),
                    schedules = gymSchedules,
                    hasRightHours = hasRightSchedules,
                    hasRightGymPhotos = hasRightGymPhotos,
                    rightGymPhotosMessage = "Please upload 3 to 8 photos of your gym.",
                    rightHourMessage = "Please input gym hours.",
                    startingPaymentDate = startingPaymentDate,
                    endingPaymentDate = endingPaymentDate,
                    currentPaymentBalance = currentPaymentBalance,
                    collectPaymentBalance = collectPaymentBalance,
                };
            }
        }


        

    }

   
}

