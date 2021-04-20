using MongoDB.Bson.Serialization.Attributes;

namespace LaXiS.ImageHash.Models.Domain
{
    public class TagDomainModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        [BsonIgnore]
        [Navigation(nameof(Category))]
        public TagCategoryDomainModel CategoryEntity { get; set; }
    }
}
