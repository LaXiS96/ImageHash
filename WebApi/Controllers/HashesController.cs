using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LaXiS.ImageHash.WebApi.Models;

namespace LaXiS.ImageHash.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HashesController : ControllerBase
    {
        private readonly ImageHashContext _context;

        public HashesController(ImageHashContext context)
        {
            _context = context;

            if (_context.ImageHashes.Count() == 0)
            {
                _context.ImageHashes.Add(new Models.ImageHash());
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.ImageHash>>> Get()
        {
            return await _context.ImageHashes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.ImageHash>> Get(Guid id)
        {
            var imageHash = await _context.ImageHashes.FindAsync(id);

            if (imageHash == null)
                return NotFound();

            return imageHash;
        }
    }
}