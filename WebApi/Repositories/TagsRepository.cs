using LaXiS.ImageHash.Models.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace LaXiS.ImageHash.WebApi.Repositories
{
    public class TagsRepository : IRepository<TagDomainModel>
    {
        private readonly MongoClient _client;
        private readonly IMongoCollection<TagDomainModel> _tags;

        public TagsRepository(
            IOptions<MongoDbSettings> settings)
        {
            _client = new MongoClient(settings.Value.ConnectionString);
            _tags = _client
                .GetDatabase(settings.Value.Database)
                .GetCollection<TagDomainModel>("Tags");

            var index = new CreateIndexModel<TagDomainModel>(
                Builders<TagDomainModel>.IndexKeys.Ascending(tag => tag.Name),
                new CreateIndexOptions
                {
                    Unique = true
                });
            _tags.Indexes.CreateOne(index);
        }

        public string Add(TagDomainModel tag)
        {
            tag.Id = ObjectId.GenerateNewId().ToString();

            _tags.InsertOne(tag);

            return tag.Id;
        }

        public IQueryable<TagDomainModel> Get()
        {
            return _tags.AsQueryable();
        }

        public TagDomainModel Get(string id)
        {
            return _tags.Find(FilterById(id)).FirstOrDefault();
        }

        public bool Remove(string id)
        {
            var result = _tags.DeleteOne(FilterById(id));

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public bool Update(TagDomainModel tag)
        {
            var result = _tags.ReplaceOne(FilterById(tag.Id), tag);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        private FilterDefinition<TagDomainModel> FilterById(string id)
        {
            return Builders<TagDomainModel>.Filter.Eq(tag => tag.Id, id);
        }
    }
}
