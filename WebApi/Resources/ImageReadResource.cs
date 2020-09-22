using System;

namespace LaXiS.ImageHash.WebApi.Resources
{
    public class ImageReadResource
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Md5 { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
