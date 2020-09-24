using LaXiS.ImageHash.Models.Domain;
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

        public Response<string> Add(Image image)
        {
            string id;

            try
            {
                id = _imageRepository.Create(image);
            }
            catch (Exception e)
            {
                return new Response<string>(false, $"Could not create Image: {e.Message}", string.Empty);
            }

            return new Response<string>(true, string.Empty, id);
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

        public Response Update(string id, Image image)
        {
            Image originalImage = _imageRepository.Read(id);

            if (originalImage == null)
                return new Response(false, $"Image with Id \"{id}\" not found");

            image.Id = originalImage.Id;
            image.CreatedAt = originalImage.CreatedAt;

            if (!_imageRepository.Update(image))
                return new Response(false, $"Update unsuccessful");

            return new Response(true, string.Empty);
        }

        public Response Remove(string id)
        {
            bool result = _imageRepository.Delete(id);
            return new Response(result, result ? string.Empty : $"Image with id \"{id}\" not found");
        }

        public Response<IEnumerable<Image>> GetSimilar(string id)
        {
            // TODO
            throw new NotImplementedException();
        }
    }
}
