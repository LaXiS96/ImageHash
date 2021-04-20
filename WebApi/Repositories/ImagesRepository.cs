using LaXiS.ImageHash.Models.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;

namespace LaXiS.ImageHash.WebApi.Repositories
{
    public class ImagesRepository : IRepository<ImageDomainModel>
    {
        private readonly MongoClient _client;
        private readonly IMongoCollection<ImageDomainModel> _images;

        public ImagesRepository(
            IOptions<MongoDbSettings> settings)
        {
            _client = new MongoClient(settings.Value.ConnectionString);
            _images = _client
                .GetDatabase(settings.Value.Database)
                .GetCollection<ImageDomainModel>("Images");

            var index = new CreateIndexModel<ImageDomainModel>(
                Builders<ImageDomainModel>.IndexKeys.Ascending(image => image.Name),
                new CreateIndexOptions
                {
                    Unique = true
                });
            _images.Indexes.CreateOne(index);
        }

        public string Add(ImageDomainModel image)
        {
            image.Id = ObjectId.GenerateNewId().ToString();
            image.CreatedAt = DateTime.UtcNow;
            image.UpdatedAt = image.CreatedAt;

            _images.InsertOne(image);

            return image.Id;
        }

        public IQueryable<ImageDomainModel> Get()
        {
            return _images.AsQueryable();
        }

        public ImageDomainModel Get(string id)
        {
            return _images.Find(FilterById(id)).FirstOrDefault();
        }

        public bool Remove(string id)
        {
            var result = _images.DeleteOne(FilterById(id));

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public bool Update(ImageDomainModel image)
        {
            image.UpdatedAt = DateTime.UtcNow;

            var result = _images.ReplaceOne(FilterById(image.Id), image);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        private FilterDefinition<ImageDomainModel> FilterById(string id)
        {
            return Builders<ImageDomainModel>.Filter.Eq(image => image.Id, id);
        }
    }
}
