using UniversalGym.Responses;
using UniversalGym.Requests;
using UniversalGym.Data;
using System.Linq;
using System.Collections.Generic;
using System;

namespace UniversalGym.Admin
{
    public class deleteGym
    {
        public static BasicResponse deleteGymImplementation(deleteGymRequest request)
        {
            if(!Constants.isApiStaging)
            {
                return new BasicResponse() { success = false, status = 404, message = "" };
            }

            if (request.authToken != Constants.MarketingToken)
                return new BasicResponse() { success = false, status = 404, message = "Wrong Token" };

            using (var db = new UniversalGymEntities())
            {
                var gym = db.Gyms.FirstOrDefault(cs => cs.GymId == request.gymId);
                if (gym == null)
                {
                    return new BasicResponse { message = "Gym not found!", status = 404, success = false };
                }

                // delete gym invoices TODO
                // delete gym pass TODO


                // delete gym photogallery
                if (gym.GymPhotoGalleries != null)
                {
                    db.GymPhotoGalleries.RemoveRange(gym.GymPhotoGalleries);
                }

                // delete address of public contact
                if (gym.ContactInfo1.Address != null)
                {
                    db.Addresses.Remove(gym.ContactInfo1.Address);
                }
                
                // delete public contact
                if (gym.ContactInfo1 != null)
                {
                    db.ContactInfoes.Remove(gym.ContactInfo1);
                }
                
                // delete address of owner contact
                if (gym.ContactInfo.Address != null)
                {
                    db.Addresses.Remove(gym.ContactInfo.Address);
                }
                
                // delete owner contact
                if (gym.ContactInfo != null)
                {
                    db.ContactInfoes.Remove(gym.ContactInfo);
                }

                // delete gym
                db.Gyms.Remove(gym);
                db.SaveChanges();
            }

            return new BasicResponse() { success = true, status = 200, message = "Success" };
        }
    }
}