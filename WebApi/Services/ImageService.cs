using System.Collections.Generic;
using LaXiS.ImageHash.WebApi.Domain.Models;
using LaXiS.ImageHash.WebApi.Domain.Repositories;
using LaXiS.ImageHash.WebApi.Domain.Services;
using LaXiS.ImageHash.WebApi.Domain.Services.Communication;

namespace LaXiS.ImageHash.WebApi.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public Response<Image> Add(Image image)
        {
            string id = _imageRepository.Create(image);

            if (!string.IsNullOrWhiteSpace(id))
            {
                image = _imageRepository.Read(id);

                return new Response<Image>(true, string.Empty, image);
            }
            else
            {
                return new Response<Image>(false, "Returned Id is empty", null);
            }

        }

        public Response<IEnumerable<Image>> GetAll()
        {
            return new Response<IEnumerable<Image>>(true, string.Empty, _imageRepository.Read());
        }

        public Response<Image> GetById(string id)
        {
            return new Response<Image>(true, string.Empty, _imageRepository.Read(id));
        }

        public Response RemoveById(string id)
        {
            bool result = _imageRepository.Delete(id);
            return new Response(result, result ? string.Empty : "Id not found");
        }
    }
}
