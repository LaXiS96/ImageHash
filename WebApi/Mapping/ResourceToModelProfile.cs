using AutoMapper;
using LaXiS.ImageHash.Models.Domain;
using LaXiS.ImageHash.Models.Resources;

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
