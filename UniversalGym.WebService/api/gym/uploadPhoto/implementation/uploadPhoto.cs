using System.Linq;
using UniversalGym.Data;
using UniversalGym.Responses;
using Microsoft.WindowsAzure.Storage;
using System;
using Microsoft.WindowsAzure.Storage.Blob;
using UniversalGym.Requests;
using System.IO;

namespace UniversalGym.Gym
{

    public class uploadPhoto
    {

        public static UploadPictureResponse uploadPhotoImplementation(Stream file, string token, string accountId)
        {
            if (String.IsNullOrWhiteSpace(token))
            {
                return new UploadPictureResponse
                {
                    message = "Gym not found.",
                    status = 404,
                    success = false,
                };
            }

            int accountIdAsInt;
            if (String.IsNullOrWhiteSpace(accountId) || !Int32.TryParse(accountId, out accountIdAsInt))
            {
                return new UploadPictureResponse
                {
                    message = "Gym not found.",
                    status = 404,
                    success = false,
                };
            }


            var parser = new MultipartParser(file);
            using (var db = new UniversalGymEntities())
            {
                var gym = db.Gyms.SingleOrDefault(a => a.CurrentToken == token && a.GymId == accountIdAsInt);
                if (gym == null)
                {
                    new UploadPictureResponse
                    {
                        message = "Gym not found.",
                        status = 404,
                        success = false,
                    };
                }

                if (gym.GymPhotoGalleries.Count >= 8)
                {
                    return new UploadPictureResponse { message = "You can only upload up to 8 images.", status = 404, success = false };
                }

                if (parser.Success)
                {
                    // Make sure really is image and the kind we want
                    string[] supportedMimeTypes = { "images/png", "image/png", "image/jpeg", "images/jpeg", "image/jpg", "images/jpg" };

                    if (!supportedMimeTypes.Contains(parser.ContentType.ToString().ToLower()))
                    {
                        return new UploadPictureResponse { message = "Image needs to be .png or .jpg", status = 404, success = false };
                    }

                    var megabyte = 1024;
                    var fileSizeLimit = megabyte * 2;
                    if (parser.FileContents.Length < fileSizeLimit)
                    {
                        return new UploadPictureResponse { message = "Image too large", status = 404, success = false };
                    }


                    var picture = new Data.GymPhotoGallery
                    {
                        GymId = gym.GymId,
                        IsCoverPhoto = false
                    };
                    db.GymPhotoGalleries.Add(picture);


                    var filename = gym.GymId + "_" + Guid.NewGuid();

                    var storageAccount = CloudStorageAccount.Parse(Constants.BlobStorageConnectionString);

                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer blobContainer = blobClient.GetContainerReference(Constants.GymPicturesBlobContainerName);

                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(filename);
                    blob.Properties.ContentType = parser.ContentType;
                    blob.UploadFromByteArrayAsync(parser.FileContents, 0, parser.FileContents.Length);

                    //  the magical url to the picture
                    var url = blob.Uri.ToString();

                    picture.Photo = url;

                    // if no other photos, set isCoverPhoto to true
                    var hasCoverPhoto = db.GymPhotoGalleries.SingleOrDefault(a => a.GymId == gym.GymId && a.IsCoverPhoto == true);
                    if (hasCoverPhoto == null)
                    {
                        picture.IsCoverPhoto = true;
                    }

                    db.SaveChanges();

                    var uploadedPhoto = new pictureResult
                        {
                            url = picture.Photo,
                            pictureId = picture.GymPhotoGalleryId,
                            isCoverPhoto = picture.IsCoverPhoto,
                        };

                    // return back new url
                    return new UploadPictureResponse { status = 200, picture = uploadedPhoto, success = true, message = "Success!" };


                }
                else
                {
                    return new UploadPictureResponse { status = 500, success = false, message = "Not able to parse file"};
                }
            }

        }


    }
        
}
