namespace LaXiS.ImageHash.WebApi.Resources
{
    public class ImageReadResource
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public HashInfoReadResource Hashes { get; set; }

        public string Url { get; set; }
    }
}
