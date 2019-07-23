using System;

namespace LaXiS.ImageHash.WebApi.Models
{
    public class ImageHash
    {
        public Guid Id { get; set; }
        public string Md5Hash { get; set; }
        public string PerceptualHash { get; set; }
        public string[] FileNames { get; set; }
    }
}