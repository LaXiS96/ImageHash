using LaXiS.ImageHash.Models.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace LaXiS.ImageHash.WebApi.Repositories
{
    public class ImagesMongoDbRepository : IImagesRepository
    {
        private readonly MongoClient _client;
        private readonly IMongoCollection<Image> _images;

        public ImagesMongoDbRepository(
            IOptions<MongoDbSettings> settings)
        {
            _client = new MongoClient(settings.Value.ConnectionString);
            _images = _client
                .GetDatabase(settings.Value.Database)
                .GetCollection<Image>("Images");

            var index = new CreateIndexModel<Image>(
                Builders<Image>.IndexKeys.Ascending(image => image.Name),
                new CreateIndexOptions
                {
                    Unique = true
                });
            _images.Indexes.CreateOne(index);
        }

        public string Create(Image image)
        {
            image.Id = ObjectId.GenerateNewId().ToString();
            image.CreatedAt = DateTime.UtcNow;

            _images.InsertOne(image);

            return image.Id;
        }

        public bool Delete(string id)
        {
            var result = _images.DeleteOne(FilterById(id));

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public IEnumerable<Image> Read()
        {
            return _images.Find(new BsonDocument()).ToEnumerable();
        }

        public Image Read(string id)
        {
            return _images.Find(FilterById(id)).First();
        }

        public bool Update(Image image)
        {
            var result = _images.ReplaceOne(FilterById(image.Id), image);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        private FilterDefinition<Image> FilterById(string id)
        {
            return Builders<Image>.Filter.Eq(image => image.Id, id);
        }
    }
}
