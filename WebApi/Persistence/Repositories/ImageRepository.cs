using System.Collections.Generic;
using System.Linq;
using LaXiS.ImageHash.WebApi.Domain.Models;
using LaXiS.ImageHash.WebApi.Domain.Repositories;
using LiteDB;

namespace LaXiS.ImageHash.WebApi.Persistence.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly LiteDatabase _db;
        private readonly LiteCollection<Image> _images;

        // TODO db handling should be in another layer, a repository only represents a single collection, not the whole database
        // TODO LiteDBSettings should not be explicitly used, use options pattern
        public ImageRepository(ILiteDBSettings settings)
        {
            _db = new LiteDatabase(settings.ConnectionString);
            _images = _db.GetCollection<Image>("Images");

            _images.EnsureIndex("Name", true);
        }

        public string Create(Image image)
        {
            image.Id = ObjectId.NewObjectId().ToString();
            _images.Insert(image);

            return image.Id;
        }

        public List<Image> Read()
        {
            return _images.FindAll().ToList();
        }

        public Image Read(string id)
        {
            return _images.FindById(id);
        }

        public bool Update(Image image)
        {
            return _images.Update(image);
        }

        public bool Delete(string id)
        {
            return _images.Delete(id);
        }
    }
}
