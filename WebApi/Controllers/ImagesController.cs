using AutoMapper;
using LaXiS.ImageHash.Models.Domain;
using LaXiS.ImageHash.Models.Resources;
using LaXiS.ImageHash.WebApi.Services;
using LaXiS.ImageHash.WebApi.Services.Communication;
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
        private readonly IImagesService _imagesService;

        public ImagesController(
            ILogger<ImagesController> logger,
            IMapper mapper,
            IImagesService imagesService)
        {
            _logger = logger;
            _mapper = mapper;
            _imagesService = imagesService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ImageReadResource>> Get()
        {
            Response<IEnumerable<Image>> imagesResponse = _imagesService.Get();

            if (!imagesResponse.Success)
                return BadRequest(imagesResponse.Message);

            IEnumerable<ImageReadResource> resources = _mapper.Map<IEnumerable<Image>, IEnumerable<ImageReadResource>>(imagesResponse.Value);

            return resources.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<ImageReadResource> Get(
            [FromRoute] string id)
        {
            Response<Image> response = _imagesService.Get(id);

            if (!response.Success)
                return BadRequest(response.Message);

            if (response.Value == null)
                return NotFound();

            ImageReadResource resource = _mapper.Map<Image, ImageReadResource>(response.Value);

            return resource;
        }

        [HttpPost]
        public ActionResult Post(
            [FromBody] ImageWriteResource imageWriteResource)
        {
            Image image = _mapper.Map<ImageWriteResource, Image>(imageWriteResource);

            Response<string> response = _imagesService.Add(image);

            if (!response.Success)
                return BadRequest(response.Message);

            object ret = new
            {
                id = response.Value
            };
            return CreatedAtAction(nameof(Get), ret, ret);
        }

        [HttpPut("{id}")]
        public ActionResult<ImageReadResource> Put(
            [FromRoute] string id,
            [FromBody] ImageWriteResource imageWriteResource)
        {
            Image image = _mapper.Map<ImageWriteResource, Image>(imageWriteResource);

            Response response = _imagesService.Update(id, image);

            if (!response.Success)
                return BadRequest(response.Message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(
            [FromRoute] string id)
        {
            Response response = _imagesService.Remove(id);

            if (!response.Success)
                return NotFound(response.Message);

            return NoContent();
        }
    }
}
