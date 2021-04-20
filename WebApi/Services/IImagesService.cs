using LaXiS.ImageHash.Models.Domain;
using LaXiS.ImageHash.WebApi.Services.Communication;
using System.Collections.Generic;

namespace LaXiS.ImageHash.WebApi.Services
{
    public interface IImagesService
    {
        Response<IEnumerable<ImageDomainModel>> Get();

        Response<ImageDomainModel> Get(string id);

        Response<string> Add(ImageDomainModel image);

        Response Update(string id, ImageDomainModel image);

        Response Remove(string id);

        Response<IEnumerable<ImageDomainModel>> GetSimilar(string id);
    }
}
