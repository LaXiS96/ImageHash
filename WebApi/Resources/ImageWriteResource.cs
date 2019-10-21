using System.ComponentModel.DataAnnotations;

namespace LaXiS.ImageHash.WebApi.Resources
{
    public class ImageWriteResource
    {
        [Required]
        public string Name { get; set; }

        public HashInfoWriteResource Hashes { get; set; }

        public string Url { get; set; }
    }
}
