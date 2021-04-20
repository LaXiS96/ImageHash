using System.Linq;

namespace LaXiS.ImageHash.WebApi.Repositories
{
    public interface IRepository<TEntity>
    {
        string Add(TEntity entity);

        IQueryable<TEntity> Get();

        TEntity Get(string id);

        bool Update(TEntity entity);

        bool Remove(string id);
    }
}
