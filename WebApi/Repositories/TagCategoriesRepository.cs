using LaXiS.ImageHash.Models.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace LaXiS.ImageHash.WebApi.Repositories
{
    public class TagCategoriesRepository : IRepository<TagCategoryDomainModel>
    {
        private readonly MongoClient _client;
        private readonly IMongoCollection<TagCategoryDomainModel> _tagCategories;

        public TagCategoriesRepository(
            IOptions<MongoDbSettings> settings)
        {
            _client = new MongoClient(settings.Value.ConnectionString);
            _tagCategories = _client
                .GetDatabase(settings.Value.Database)
                .GetCollection<TagCategoryDomainModel>("TagCategories");

            var index = new CreateIndexModel<TagCategoryDomainModel>(
                Builders<TagCategoryDomainModel>.IndexKeys.Ascending(tagCategory => tagCategory.Name),
                new CreateIndexOptions
                {
                    Unique = true
                });
            _tagCategories.Indexes.CreateOne(index);
        }

        public string Add(TagCategoryDomainModel tagCategory)
        {
            tagCategory.Id = ObjectId.GenerateNewId().ToString();

            _tagCategories.InsertOne(tagCategory);

            return tagCategory.Id;
        }

        public IQueryable<TagCategoryDomainModel> Get()
        {
            return _tagCategories.AsQueryable();
        }

        public TagCategoryDomainModel Get(string id)
        {
            return _tagCategories.Find(FilterById(id)).FirstOrDefault();
        }

        public bool Remove(string id)
        {
            var result = _tagCategories.DeleteOne(FilterById(id));

            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public bool Update(TagCategoryDomainModel tagCategory)
        {
            var result = _tagCategories.ReplaceOne(FilterById(tagCategory.Id), tagCategory);

            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        private FilterDefinition<TagCategoryDomainModel> FilterById(string id)
        {
            return Builders<TagCategoryDomainModel>.Filter.Eq(tagCategory => tagCategory.Id, id);
        }
    }
}
