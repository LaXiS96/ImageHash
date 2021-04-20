using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace LaXiS.ImageHash.Models.Domain
{
    public class ImageDomainModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Tags { get; set; }

        [BsonIgnore]
        [Navigation(nameof(Tags))]
        public IEnumerable<TagDomainModel> TagsEntities { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
