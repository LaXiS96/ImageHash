﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LaXiS.ImageHash.Models.Resources
{
    public class ImageWriteResource
    {
        [Required]
        public string Name { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
