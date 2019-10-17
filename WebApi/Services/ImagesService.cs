using LaXiS.ImageHash.WebApi.Models;
using LiteDB;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace LaXiS.ImageHash.WebApi.Services
{
    public class ImagesService
    {
        private ILogger<ImagesService> _logger;
        private LiteDatabase _db;
        private LiteCollection<ImageModel> _images;

        public ImagesService(ILogger<ImagesService> logger, ILiteDBSettings settings)
        {
            _logger = logger;

            _db = new LiteDatabase(settings.ConnectionString);
            _images = _db.GetCollection<ImageModel>("Images");

            _images.EnsureIndex("Name", true);
        }

        public ImageModel Create(ImageModel image)
        {
            image.Id = ObjectId.NewObjectId().ToString();
            _images.Insert(image);

            return image;
        }

        public IEnumerable<ImageModel> Read()
        {
            return _images.FindAll();
        }

        public ImageModel Read(string id)
        {
            return _images.FindById(id);
        }

        public bool Update(ImageModel image)
        {
            return _images.Update(image);
        }

        public bool Delete(string id)
        {
            return _images.Delete(id);
        }
    }
}
