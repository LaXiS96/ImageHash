using AutoMapper;
using LaXiS.ImageHash.Models.Domain;
using LaXiS.ImageHash.Models.Resources;
using LaXiS.ImageHash.WebApi.Services;
using LaXiS.ImageHash.WebApi.Services.Communication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LaXiS.ImageHash.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITagsService _tagsService;

        public TagsController(
            IMapper mapper,
            ITagsService tagsService)
        {
            _mapper = mapper;
            _tagsService = tagsService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Response<IEnumerable<TagDomainModel>> response = _tagsService.Get();

            if (!response.Successful)
                return BadRequest(response.Message);

            IEnumerable<TagReadResource> resources =
                _mapper.Map<IEnumerable<TagDomainModel>, IEnumerable<TagReadResource>>(
                    response.Value);

            return Ok(resources);
        }

        [HttpGet("{id}")]
        public IActionResult Get(
            [FromRoute] string id)
        {
            Response<TagDomainModel> response = _tagsService.Get(id);

            if (!response.Successful)
                return BadRequest(response.Message);

            if (response.Value == null)
                return NotFound();

            TagReadResource resource = _mapper.Map<TagDomainModel, TagReadResource>(response.Value);

            return Ok(resource);
        }

        [HttpPost]
        public IActionResult Post(
            [FromBody] TagWriteResource tagWriteResource)
        {
            TagDomainModel tag = _mapper.Map<TagWriteResource, TagDomainModel>(tagWriteResource);

            Response<string> response = _tagsService.Add(tag);

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
            [FromBody] TagWriteResource tagWriteResource)
        {
            TagDomainModel tag = _mapper.Map<TagWriteResource, TagDomainModel>(tagWriteResource);

            Response response = _tagsService.Update(id, tag);

            if (!response.Successful)
                return BadRequest(response.Message);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(
            [FromRoute] string id)
        {
            Response response = _tagsService.Remove(id);

            if (!response.Successful)
                return NotFound(response.Message);

            return NoContent();
        }
    }
}
