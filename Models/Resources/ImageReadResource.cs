﻿using System;
using System.Collections.Generic;

namespace LaXiS.ImageHash.Models.Resources
{
    public class ImageReadResource
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [Expandable(typeof(IEnumerable<TagReadResource>))]
        public IEnumerable<string> Tags { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
