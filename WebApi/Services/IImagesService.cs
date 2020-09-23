using LaXiS.ImageHash.Models.Domain;
using LaXiS.ImageHash.WebApi.Services.Communication;
using System.Collections.Generic;

namespace LaXiS.ImageHash.WebApi.Services
{
    public interface IImagesService
    {
        Response<IEnumerable<Image>> Get();

        Response<Image> Get(string id);

        Response<string> Add(Image image);

        Response Update(string id, Image image);

        Response Remove(string id);
    }
}
