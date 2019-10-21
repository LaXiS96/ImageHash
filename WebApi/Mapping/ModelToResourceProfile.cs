using AutoMapper;
using LaXiS.ImageHash.WebApi.Domain.Models;
using LaXiS.ImageHash.WebApi.Resources;

namespace LaXiS.ImageHash.WebApi.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Image, ImageReadResource>();
            CreateMap<HashInfo, HashInfoReadResource>();
        }
    }
}
