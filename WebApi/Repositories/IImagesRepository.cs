using LaXiS.ImageHash.Models.Domain;
using System.Collections.Generic;

namespace LaXiS.ImageHash.WebApi.Repositories
{
    public interface IImagesRepository
    {
        string Create(Image image);

        IEnumerable<Image> Read();

        Image Read(string id);

        bool Update(Image image);

        bool Delete(string id);
    }
}
