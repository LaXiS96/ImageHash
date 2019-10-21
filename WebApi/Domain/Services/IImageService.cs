using LaXiS.ImageHash.WebApi.Domain.Models;
using LaXiS.ImageHash.WebApi.Domain.Services.Communication;
using System.Collections.Generic;

namespace LaXiS.ImageHash.WebApi.Domain.Services
{
    public interface IImageService
    {
        Response<IEnumerable<Image>> GetAll();

        Response<Image> GetById(string id);

        Response<Image> Add(Image image);

        Response RemoveById(string id);
    }
}
