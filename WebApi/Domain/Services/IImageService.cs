using LaXiS.ImageHash.WebApi.Domain.Models;
using System.Collections.Generic;

namespace LaXiS.ImageHash.WebApi.Domain.Services
{
    public interface IImageService
    {
        List<Image> GetAll();

        Image GetById(string id);

        string Add(Image image);

        bool RemoveById(string id);
    }
}
