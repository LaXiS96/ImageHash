using AutoMapper;
using LaXiS.ImageHash.Models.Domain;
using LaXiS.ImageHash.Models.Resources;

namespace LaXiS.ImageHash.WebApi.Mapping
{
    public class DomainToResourceProfile : Profile
    {
        public DomainToResourceProfile()
        {
            CreateMap<ImageDomainModel, ImageReadResource>();
            CreateMap<TagDomainModel, TagReadResource>();
            CreateMap<TagCategoryDomainModel, TagCategoryReadResource>();
        }
    }
}
