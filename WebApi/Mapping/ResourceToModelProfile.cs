using AutoMapper;
using LaXiS.ImageHash.WebApi.Models;
using LaXiS.ImageHash.WebApi.Resources;

namespace LaXiS.ImageHash.WebApi.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<ImageWriteResource, Image>();
        }
    }
}
