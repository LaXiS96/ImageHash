using LaXiS.ImageHash.WebApi.Domain.Models;
using System.Collections.Generic;

namespace LaXiS.ImageHash.WebApi.Domain.Repositories
{
    public interface IImageRepository
    {
        string Create(Image image);

        List<Image> Read();

        Image Read(string id);

        bool Update(Image image);

        bool Delete(string id);
    }
}
