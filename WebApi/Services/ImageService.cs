using System.Collections.Generic;
using LaXiS.ImageHash.WebApi.Domain.Models;
using LaXiS.ImageHash.WebApi.Domain.Repositories;
using LaXiS.ImageHash.WebApi.Domain.Services;

namespace LaXiS.ImageHash.WebApi.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public string Add(Image image)
        {
            return _imageRepository.Create(image);
        }

        public List<Image> GetAll()
        {
            return _imageRepository.Read();
        }

        public Image GetById(string id)
        {
            return _imageRepository.Read(id);
        }

        public bool RemoveById(string id)
        {
            return _imageRepository.Delete(id);
        }
    }
}
