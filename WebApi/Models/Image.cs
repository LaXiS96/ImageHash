using System;

namespace LaXiS.ImageHash.WebApi.Models
{
    public class Image
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Md5 { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
