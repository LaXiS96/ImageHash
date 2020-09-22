using System.ComponentModel.DataAnnotations;

namespace LaXiS.ImageHash.WebApi.Resources
{
    public class ImageWriteResource
    {
        [Required]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Url { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Md5 { get; set; }
    }
}
