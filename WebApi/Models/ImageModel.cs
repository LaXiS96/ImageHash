using System.ComponentModel.DataAnnotations;

namespace LaXiS.ImageHash.WebApi.Models
{
    public class ImageModel
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public HashInfo Hashes { get; set; }

        public string Url { get; set; }
    }
}
