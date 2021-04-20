using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LaXiS.ImageHash.Models.Resources
{
    public class TagWriteResource
    {
        [Required]
        public string Name { get; set; }

        public string Category { get; set; }
    }
}
