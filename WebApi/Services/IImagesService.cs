using LaXiS.ImageHash.WebApi.Models;
using LaXiS.ImageHash.WebApi.Services.Communication;
using System.Collections.Generic;

namespace LaXiS.ImageHash.WebApi.Services
{
    public interface IImagesService
    {
        Response<IEnumerable<Image>> Get();

        Response<Image> Get(string id);

        Response<Image> Add(Image image);

        Response<Image> Update(string id, Image image);

        Response Remove(string id);
    }
}
