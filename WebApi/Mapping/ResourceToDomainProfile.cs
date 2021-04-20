using AutoMapper;
using LaXiS.ImageHash.Models.Domain;
using LaXiS.ImageHash.Models.Resources;

namespace LaXiS.ImageHash.WebApi.Mapping
{
    public class ResourceToDomainProfile : Profile
    {
        public ResourceToDomainProfile()
        {
            CreateMap<ImageWriteResource, ImageDomainModel>();

            CreateMap<TagWriteResource, TagDomainModel>();

            CreateMap<TagCategoryWriteResource, TagCategoryDomainModel>();
        }
    }
}
