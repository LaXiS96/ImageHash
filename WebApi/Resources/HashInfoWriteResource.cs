using System.ComponentModel.DataAnnotations;

namespace LaXiS.ImageHash.WebApi.Resources
{
    public class HashInfoWriteResource
    {
        [Required]
        public string Md5 { get; set; }
    }
}
