using LaXiS.ImageHash.Models.Domain;
using LaXiS.ImageHash.WebApi.Repositories;
using LaXiS.ImageHash.WebApi.Services.Communication;
using System;
using System.Collections.Generic;

namespace LaXiS.ImageHash.WebApi.Services
{
    public class TagsService : ITagsService
    {
        private readonly IRepository<TagDomainModel> _tagsRepository;

        public TagsService(
            IRepository<TagDomainModel> tagsRepository)
        {
            _tagsRepository = tagsRepository;
        }

        public Response<string> Add(TagDomainModel tag)
        {
            string id;

            try
            {
                id = _tagsRepository.Add(tag);
            }
            catch (Exception e)
            {
                return Response<string>.Failure(e);
            }

            return Response<string>.Success(id);
        }

        public Response<IEnumerable<TagDomainModel>> Get()
        {
            // TODO implement filtering via query string
            // TODO implement paging? for Images too

            return Response<IEnumerable<TagDomainModel>>.Success(_tagsRepository.Get());
        }

        public Response<TagDomainModel> Get(string id)
        {
            return Response<TagDomainModel>.Success(_tagsRepository.Get(id));
        }

        public Response Update(string id, TagDomainModel tag)
        {
            TagDomainModel originalTag = _tagsRepository.Get(id);

            if (originalTag == null)
                return Response.Failure($"Tag with Id \"{id}\" not found");

            tag.Id = originalTag.Id;

            if (!_tagsRepository.Update(tag))
                return Response.Failure($"Update unsuccessful");

            return Response.Success();
        }

        public Response Remove(string id)
        {
            if (_tagsRepository.Remove(id))
                return Response.Success();
            else
                return Response.Failure($"Tag with Id \"{id}\" not found");
        }
    }
}
