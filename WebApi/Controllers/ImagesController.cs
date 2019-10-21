using AutoMapper;
using LaXiS.ImageHash.WebApi.Domain.Models;
using LaXiS.ImageHash.WebApi.Domain.Services;
using LaXiS.ImageHash.WebApi.Domain.Services.Communication;
using LaXiS.ImageHash.WebApi.Resources;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace LaXiS.ImageHash.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly ILogger<ImagesController> _logger;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public ImagesController(ILogger<ImagesController> logger, IMapper mapper, IImageService imagesService)
        {
            _logger = logger;
            _mapper = mapper;
            _imageService = imagesService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ImageReadResource>> Get()
        {
            Response<IEnumerable<Image>> imagesResponse = _imageService.GetAll();

            if (!imagesResponse.Success)
                return BadRequest(imagesResponse.Message);

            IEnumerable<ImageReadResource> resources = _mapper.Map<IEnumerable<Image>, IEnumerable<ImageReadResource>>(imagesResponse.Value);

            return resources.ToList();
        }

        [HttpGet("{id}", Name = "GetById")]
        public ActionResult<ImageReadResource> Get(string id)
        {
            Response<Image> imageResponse = _imageService.GetById(id);

            if (!imageResponse.Success)
                return BadRequest(imageResponse.Message);

            Image image = imageResponse.Value;

            if (image == null)
                return NotFound();

            ImageReadResource resource = _mapper.Map<Image, ImageReadResource>(image);

            return resource;
        }

        [HttpPost]
        public ActionResult<ImageReadResource> Post([FromBody] ImageWriteResource imageWriteResource)
        {
            Image image = _mapper.Map<ImageWriteResource, Image>(imageWriteResource);

            //try
            //{
            Response<Image> imageResponse = _imageService.Add(image);

            if (!imageResponse.Success)
                return BadRequest(imageResponse.Message);

            //}
            //catch (LiteException e) when (e.ErrorCode == LiteException.INDEX_DUPLICATE_KEY) // TODO no reference to LiteDB classes
            //{
            //    result = Conflict(e.Message); // TODO give meaningful error response, should not expose db error
            //}

            return CreatedAtRoute("GetById", new { id = imageResponse.Value.Id }, imageResponse.Value);
        }

        //[HttpPut("{id}")]
        //public void Put(string id, [FromBody] Image image)
        //{
        //    // TODO implement put
        //}

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Response response = _imageService.RemoveById(id);

            if (!response.Success)
                return NotFound(response.Message);

            return NoContent();
        }
    }
}
