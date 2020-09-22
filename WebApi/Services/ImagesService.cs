using LaXiS.ImageHash.WebApi.Models;
using LaXiS.ImageHash.WebApi.Repositories;
using LaXiS.ImageHash.WebApi.Services.Communication;
using System;
using System.Collections.Generic;

namespace LaXiS.ImageHash.WebApi.Services
{
    public class ImagesService : IImagesService
    {
        private readonly IImagesRepository _imageRepository;

        public ImagesService(IImagesRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public Response<Image> Add(Image image)
        {
            string id;

            try
            {
                id = _imageRepository.Create(image);
            }
            catch (Exception e)
            {
                return new Response<Image>(false, $"Could not create Image: {e.Message}", null);
            }

            image = _imageRepository.Read(id);

            return new Response<Image>(true, string.Empty, image);
        }

        public Response<IEnumerable<Image>> Get()
        {
            return new Response<IEnumerable<Image>>(true, string.Empty, _imageRepository.Read());
        }

        public Response<Image> Get(string id)
        {
            // TODO test, this could fail
            return new Response<Image>(true, string.Empty, _imageRepository.Read(id));
        }

        public Response<Image> Update(string id, Image image)
        {
            Image originalImage = _imageRepository.Read(id);

            if (originalImage == null)
                return new Response<Image>(false, $"Image with Id \"{id}\" not found", null);

            image.Id = originalImage.Id;
            image.CreatedAt = originalImage.CreatedAt;

            if (!_imageRepository.Update(image))
                return new Response<Image>(false, $"Update unsuccessful", null);

            image = _imageRepository.Read(id);

            return new Response<Image>(true, string.Empty, image);
        }

        public Response Remove(string id)
        {
            bool result = _imageRepository.Delete(id);
            return new Response(result, result ? string.Empty : $"Image with id \"{id}\" not found");
        }
    }
}
