using System.ComponentModel.DataAnnotations;

namespace LaXiS.ImageHash.Models.Resources
{
    public class ImageWriteResource
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string Md5 { get; set; }

        public string DifferenceHash { get; set; }
    }
}
