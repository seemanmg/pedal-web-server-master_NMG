using System.IO;

namespace UniversalGym.Requests
{
    public class UpdatePictureRequest : BaseRequest
    {
        public Stream file { get; set; }
        public int pictureId { get; set; }
    }

}

