using System.ComponentModel.DataAnnotations;

namespace LaXiS.ImageHash.Models.Resources
{
    public class TagCategoryWriteResource
    {
        [Required]
        public string Name { get; set; }
    }
}
