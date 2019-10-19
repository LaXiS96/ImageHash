namespace LaXiS.ImageHash.WebApi.Domain.Models
{
    public class Image
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public HashInfo Hashes { get; set; }

        public string Url { get; set; }
    }
}
