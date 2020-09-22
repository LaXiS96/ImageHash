using AutoMapper;
using LaXiS.ImageHash.WebApi.Models;
using LaXiS.ImageHash.WebApi.Resources;

namespace LaXiS.ImageHash.WebApi.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Image, ImageReadResource>();
        }
    }
}
