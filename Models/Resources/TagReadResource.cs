namespace LaXiS.ImageHash.Models.Resources
{
    public class TagReadResource
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [Expandable(typeof(TagCategoryReadResource))]
        public string Category { get; set; }
    }
}
