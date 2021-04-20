using LaXiS.ImageHash.Models.Domain;
using LaXiS.ImageHash.WebApi.Repositories;
using LaXiS.ImageHash.WebApi.Services.Communication;
using System;
using System.Collections.Generic;

namespace LaXiS.ImageHash.WebApi.Services
{
    public class ImagesService : IImagesService
    {
        private readonly IRepository<ImageDomainModel> _imagesRepository;
        private readonly IRepository<TagDomainModel> _tagsRepository;

        public ImagesService(
            IRepository<ImageDomainModel> imageRepository,
            IRepository<TagDomainModel> tagsRepository)
        {
            _imagesRepository = imageRepository;
            _tagsRepository = tagsRepository;
        }

        public Response<string> Add(ImageDomainModel image)
        {
            string id;

            // TODO check existence of tags

            try
            {
                id = _imagesRepository.Add(image);
            }
            catch (Exception e)
            {
                return Response<string>.Failure(e);
            }

            return Response<string>.Success(id);
        }

        public Response<IEnumerable<ImageDomainModel>> Get()
        {
            return Response<IEnumerable<ImageDomainModel>>.Success(_imagesRepository.Get());
        }

        public Response<ImageDomainModel> Get(string id)
        {
            return Response<ImageDomainModel>.Success(_imagesRepository.Get(id));
        }

        public Response Update(string id, ImageDomainModel image)
        {
            ImageDomainModel originalImage = _imagesRepository.Get(id);

            if (originalImage == null)
                return Response.Failure($"Image with Id \"{id}\" not found");

            image.Id = originalImage.Id;
            image.CreatedAt = originalImage.CreatedAt;

            if (!_imagesRepository.Update(image))
                return Response.Failure($"Update unsuccessful");

            return Response.Success();
        }

        public Response Remove(string id)
        {
            if (_imagesRepository.Remove(id))
                return Response.Success();
            else
                return Response.Failure($"Image with Id \"{id}\" not found");
        }

        public Response<IEnumerable<ImageDomainModel>> GetSimilar(string id)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
