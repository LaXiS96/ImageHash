using Microsoft.EntityFrameworkCore;

namespace LaXiS.ImageHash.WebApi.Models
{
    public class ImageHashContext : DbContext
    {
        public ImageHashContext(DbContextOptions<ImageHashContext> options) : base(options) { }

        public DbSet<ImageHash> ImageHashes { get; set; }
    }
}