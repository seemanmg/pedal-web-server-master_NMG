using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Linq;
using System;
using UniversalGym.Data;
using UniversalGym.Requests;
using UniversalGym.Responses;

namespace UniversalGym.Gym
{
    public class deletePhoto
    {
        public static BasicResponse deletePhotoImplementation(UpdateCoverPhotoRequest request)
        {
            if (request == null || String.IsNullOrWhiteSpace(request.authToken) || request.accountId == null)
            {
                return new addGymHoursResponse { status = 401, success = false, message = "Gym not found" };
            }

            using (var db = new UniversalGymEntities())
            {
                if (request.authToken == null)
                {
                    return new BasicResponse
                    {
                        message = "Gym not found.",
                        status = 404,
                        success = false,
                    };
                }

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

                var picture = db.GymPhotoGalleries.First(w => w.GymPhotoGalleryId == request.pictureId);
                if (picture.IsCoverPhoto)
                {
                    return new BasicResponse
                    {
                        message = "Please set another photo as the cover.",
                        status = 404,
                        success = false,
                    };

                }
                deletePhotoFromBlob(picture.Photo);

                db.GymPhotoGalleries.Remove(picture);
                db.SaveChanges();
                return new BasicResponse {
                    status = 200,
                    success = true,
                    message = "Photo Gallery removed"
                };
            }
        }

        public static void deletePhotoFromBlob(string fullUrl)
        {

            // example format:
            // fullUrl = http://pictures.pedal.com/gympictures/5616_01454890-49db-41b8-a9d2-91f3d94af8d02014-09-06 06.53.34.jpg
            // blobName = 5616_01454890-49db-41b8-a9d2-91f3d94af8d02014-09-06 06.53.34.jpg


            // 1 for the slashes "/ + containerName.length + /"
            int removeSubstringTotal = 1 + Constants.GymPicturesBlobContainerName.Length + 1;
            var blobName = fullUrl.Substring(fullUrl.LastIndexOf("/" + Constants.GymPicturesBlobContainerName + "/") + removeSubstringTotal); ;
            
            var storageAccount = CloudStorageAccount.Parse(Constants.BlobStorageConnectionString);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(Constants.GymPicturesBlobContainerName);
            
            CloudBlockBlob blob = blobContainer.GetBlockBlobReference(blobName);

            var didDelete = blob.DeleteIfExists();
        }
    }
}
