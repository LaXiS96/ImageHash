using AutoMapper;
using AutoMapper.QueryableExtensions;
using LaXiS.ImageHash.Models.Domain;
using LaXiS.ImageHash.Models.Resources;
using LaXiS.ImageHash.WebApi.Repositories;
using LaXiS.ImageHash.WebApi.Services;
using LaXiS.ImageHash.WebApi.Services.Communication;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        [EnableQuery]
        public IQueryable<ImageReadResource> Get(
            [FromServices] IRepository<ImageDomainModel> imagesRepository)
        {
            return imagesRepository.Get()
                .ProjectTo<ImageReadResource>(_mapper.ConfigurationProvider);
        }

        [HttpGet("{id}")]
        public IActionResult Get(
            [FromRoute] string id)
        {
            Response<ImageDomainModel> response = _imagesService.Get(id);

            if (!response.Successful)
                return BadRequest(response.Message);

            if (response.Value == null)
                return NotFound();

            ImageReadResource resource = _mapper.Map<ImageDomainModel, ImageReadResource>(response.Value);

            return Ok(resource);
        }

        [HttpPost]
        public IActionResult Post(
            [FromBody] ImageWriteResource imageWriteResource)
        {
            ImageDomainModel image = _mapper.Map<ImageWriteResource, ImageDomainModel>(imageWriteResource);

            Response<string> response = _imagesService.Add(image);

            if (!response.Successful)
                return BadRequest(response.Message);

            object ret = new
            {
                id = response.Value
            };
            return CreatedAtAction(nameof(Get), ret, ret);
        }

        [HttpPut("{id}")]
        public IActionResult Put(
            [FromRoute] string id,
            [FromBody] ImageWriteResource imageWriteResource)
        {
            ImageDomainModel image = _mapper.Map<ImageWriteResource, ImageDomainModel>(imageWriteResource);

            Response response = _imagesService.Update(id, image);

            if (!response.Successful)
                return BadRequest(response.Message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(
            [FromRoute] string id)
        {
            Response response = _imagesService.Remove(id);

            if (!response.Successful)
                return NotFound(response.Message);

            return NoContent();
        }

        //[HttpGet("{id}/similar/")]
        //public IActionResult GetSimilar(
        //    [FromRoute] string id)
        //{
        //    Response<IEnumerable<ImageDomainModel>> response = _imagesService.GetSimilar(id);

        //    if (!response.Successful)
        //        return BadRequest(response.Message); // TODO

        //    IEnumerable<ImageReadResource> resources = _mapper.Map<IEnumerable<ImageDomainModel>, IEnumerable<ImageReadResource>>(response.Value);

        //    return Ok(resources);
        //}
    }
}
