namespace UniversalGym.Responses
{
    public class UploadPictureResponse : BasicResponse
    {
        public pictureResult picture { get; set; }
    }

    public class pictureResult
    {
        public string url { get; set; }
        public int pictureId { get; set; }

        public bool isCoverPhoto { get; set; }

    }


}


