using LaXiS.ImageHash.Models.Domain;
using LaXiS.ImageHash.WebApi.Services.Communication;
using System.Collections.Generic;

namespace LaXiS.ImageHash.WebApi.Services
{
    public interface ITagsService
    {
        Response<IEnumerable<TagDomainModel>> Get();

        Response<TagDomainModel> Get(string id);

        Response<string> Add(TagDomainModel tag);

        Response Update(string id, TagDomainModel tag);

        Response Remove(string id);
    }
}
