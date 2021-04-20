using AutoMapper;
using LaXiS.ImageHash.WebApi.Repositories;

namespace LaXiS.ImageHash.WebApi.Mapping
{
    public class ForeignKeyResolver<T> : ITypeConverter<string, T>
    {
        private readonly IRepository<T> _repository;

        public ForeignKeyResolver(
            IRepository<T> repository)
        {
            _repository = repository;
        }

        public T Convert(string source, T destination, ResolutionContext context)
        {
            // TODO raise error if no entity exists with source id

            if (source != null)
                return _repository.Get(source);
            else
                return default;
        }
    }
}
