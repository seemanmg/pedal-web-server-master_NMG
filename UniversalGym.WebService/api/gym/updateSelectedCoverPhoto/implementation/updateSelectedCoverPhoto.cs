using System;
using System.Linq;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;

namespace UniversalGym.Gym
{
    public class updateSelectedCoverPhoto
    {
        public static BasicResponse updateSelectedCoverPhotoImplementation(UpdateCoverPhotoRequest request)
        {
            if (request == null || String.IsNullOrWhiteSpace(request.authToken) || request.accountId == null)
            {
                return new BasicResponse
                {
                    message = "Gym not found.",
                    status = 404,
                    success = false,
                };
            }

            using (var db = new UniversalGymEntities())
            {
                var gym = db.Gyms.SingleOrDefault(a => a.CurrentToken == request.authToken && a.GymId == request.accountId);
                if (gym == null)
                {
                    return new BasicResponse
                    {
                        message = "Gym not found.",
                        status = 404,
                        success = false,
                    };
                }

                var photoes = db.GymPhotoGalleries.Where(w => w.GymId == gym.GymId);
                foreach (var photo in photoes)
                {
                    // set all others to false
                    photo.IsCoverPhoto = photo.GymPhotoGalleryId == request.pictureId;
                }
                db.SaveChanges();
                return new BasicResponse { status = 200, success = true, message = "Success!" };
            }
        }
    }
}