using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.EntityFrameworkCore;
using Catalog.Dtos;
using AutoMapper.QueryableExtensions;

namespace Catalog.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CuisineController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly Context _context;

        public CuisineController(
            Context context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Cuisine>>> GetCuisines() => await _context.Cuisines.ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Cuisine>> GetCuisineById(int id)
        {
            var r = await _context.Cuisines.FirstOrDefaultAsync(f => f.Id.Equals(id));
            if (r is null)
                return BadRequest("Cuisine Not Found");
            return Ok(r);
        }

        [HttpPost("")]
        public async Task<ActionResult<Cuisine>> CreateCuisine(string name)
        {
            if (await _context.Cuisines.FirstOrDefaultAsync(c => c.Name.ToLower().Equals(name.ToLower())) is not null)
                return BadRequest($"{name} is already exists");

            await _context.AddAsync(new Cuisine { Name = name});
            await _context.SaveChangesAsync();
            var res = await _context.Cuisines.OrderByDescending(c => c.Created).ProjectTo<CuisineDto>(_mapper.ConfigurationProvider).ToListAsync();
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cuisine>> UpdateCuisine([FromRoute]int id, [FromBody] CuisineRec model)
        {
            var Cuisine = await _context.Cuisines.FirstOrDefaultAsync(f => f.Id.Equals(id));
            if (Cuisine is null)
                return BadRequest("Cuisine Not Found");

            if(model.name is not null or "")
                Cuisine.Name = model.name;

            await _context.SaveChangesAsync();
            return Ok(Cuisine);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Cuisine>> DeleteCuisine([FromRoute] int id)
        {
            var Cuisine = await _context.Cuisines.FirstOrDefaultAsync(f => f.Id.Equals(id));
            if (Cuisine is null)
                return BadRequest("Cuisine Not Found");

            _context.Remove(Cuisine);

            await _context.SaveChangesAsync();
            return Ok($"{Cuisine.Name} has been deleted");
        }
    }
}
