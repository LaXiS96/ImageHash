using AutoMapper;
using LaXiS.ImageHash.WebApi.Domain.Models;
using LaXiS.ImageHash.WebApi.Resources;

namespace WebApi.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<ImageWriteResource, Image>();
            CreateMap<HashInfoWriteResource, HashInfo>();
        }
    }
}
