using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Catalog.Entities;
using Catalog.Persistence;
using Microsoft.EntityFrameworkCore;
using Catalog.Dtos;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using PaginationHelper;
using Catalog.Services;

namespace Catalog.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class CuisineController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly Context _context;
        private readonly IPageHelper _pageHelper;
        private readonly UserAuthorizationService _auth;
        protected string IP => (string)HttpContext.Connection.RemoteIpAddress.ToString() ?? "";

        public CuisineController(
            Context context,
            IMapper mapper,
            IPageHelper pageHelper,
            UserAuthorizationService auth)
        {
            _context = context;
            _mapper = mapper;
            _pageHelper = pageHelper;
            _auth = auth;
        }

        [HttpGet("")]
        public async Task<ActionResult<Envelope<Cuisine>>> GetCuisines([FromQuery] PaginationDto paginationDto) 
            => Ok(await _pageHelper.GetPageAsync(_context.Cuisines.AsNoTracking(), paginationDto));

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
            _auth.setIP(IP);
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
            _auth.setIP(IP);
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
            _auth.setIP(IP);
            var Cuisine = await _context.Cuisines.FirstOrDefaultAsync(f => f.Id.Equals(id));
            if (Cuisine is null)
                return BadRequest("Cuisine Not Found");

            _context.Remove(Cuisine);

            await _context.SaveChangesAsync();
            return Ok($"{Cuisine.Name} has been deleted");
        }
    }
}
