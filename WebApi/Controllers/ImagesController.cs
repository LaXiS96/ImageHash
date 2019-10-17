using LaXiS.ImageHash.WebApi.Models;
using LaXiS.ImageHash.WebApi.Services;
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
        private ImagesService _imagesService;

        public ImagesController(ILogger<ImagesController> logger, ImagesService imagesService)
        {
            _logger = logger;
            _imagesService = imagesService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ImageModel>> Get()
        {
            return _imagesService.Read().ToList();
        }

        [HttpGet("{id}", Name = "GetImage")]
        public ActionResult<ImageModel> Get(string id)
        {
            ImageModel image = _imagesService.Read(id);

            if (image == null)
                return NotFound();

            return image;
        }

        [HttpPost]
        public ActionResult<ImageModel> Post([FromBody] ImageModel image)
        {
            ActionResult<ImageModel> result;

            try
            {
                _imagesService.Create(image);
                result = CreatedAtRoute("GetImage", new { id = image.Id.ToString() }, image);
            }
            catch (LiteException e) when (e.ErrorCode == LiteException.INDEX_DUPLICATE_KEY)
            {
                result = Conflict(e.Message); // TODO give meaningful error response, should not expose db error
            }

            return result;
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ImageModel image)
        {
            // TODO implement put
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            ImageModel image = _imagesService.Read(id);

            if (image == null)
                return NotFound();

            _imagesService.Delete(id);

            return NoContent();
        }
    }
}
