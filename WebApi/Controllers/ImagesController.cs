using LaXiS.ImageHash.WebApi.Domain.Models;
using LaXiS.ImageHash.WebApi.Domain.Services;
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
        private ILogger<ImagesController> _logger;
        private IImageService _imageService;

        public ImagesController(ILogger<ImagesController> logger, IImageService imagesService)
        {
            _logger = logger;
            _imageService = imagesService;
        }

        [HttpGet]
        public ActionResult<List<Image>> Get()
        {
            return _imageService.GetAll();
        }

        [HttpGet("{id}", Name = "GetImage")]
        public ActionResult<Image> Get(string id)
        {
            Image image = _imageService.GetById(id);

            if (image == null)
                return NotFound();

            return image;
        }

        [HttpPost]
        public ActionResult<Image> Post([FromBody] Image image)
        {
            ActionResult<Image> result;

            try
            {
                string id = _imageService.Add(image);
                result = CreatedAtRoute("GetImage", new { id }, image);
            }
            catch (LiteException e) when (e.ErrorCode == LiteException.INDEX_DUPLICATE_KEY)
            {
                result = Conflict(e.Message); // TODO give meaningful error response, should not expose db error
            }

            return result;
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Image image)
        {
            // TODO implement put
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (!_imageService.RemoveById(id))
                return NotFound();

            return NoContent();
        }
    }
}
