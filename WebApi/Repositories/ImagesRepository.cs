using LaXiS.ImageHash.Models.Domain;
using LiteDB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace LaXiS.ImageHash.WebApi.Repositories
{
    public class ImagesRepository : IImagesRepository, IDisposable
    {
        private readonly ILogger<ImagesRepository> _logger;
        private readonly LiteDatabase _db;
        private readonly ILiteCollection<Image> _images;

        public ImagesRepository(
            ILogger<ImagesRepository> logger,
            ILiteDBSettings settings)
        {
            _logger = logger;
            _db = new LiteDatabase(settings.ConnectionString);
            _images = _db.GetCollection<Image>("Images");

            _images.EnsureIndex("Name", true);
        }

        public string Create(Image image)
        {
            image.Id = ObjectId.NewObjectId().ToString();
            image.CreatedAt = DateTime.UtcNow;

            try
            {
                _images.Insert(image);
            }
            catch (LiteException e) when (e.ErrorCode == LiteException.INDEX_DUPLICATE_KEY)
            {
                throw new Exception(e.Message);
            }

            return image.Id;
        }

        public IEnumerable<Image> Read()
        {
            return _images.FindAll();
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

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
