﻿using System;

namespace LaXiS.ImageHash.Models.Domain
{
    public class Image
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Md5 { get; set; }
        public string DifferenceHash { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}