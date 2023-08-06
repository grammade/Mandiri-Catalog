using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Catalog.Entities;
using Catalog.Persistence;
using Microsoft.EntityFrameworkCore;
using Catalog.Dtos;
using AutoMapper.QueryableExtensions;
using PaginationHelper;
using Catalog.Services;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class FoodsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly Context _context;
        private readonly IPageHelper _pageHelper;
        private readonly ThirdPartyGateway _thirdPartyGateway;

        public FoodsController(
            Context context,
            IMapper mapper,
            IPageHelper pageHelper,
            ThirdPartyGateway thirdPartyGateway)
        {
            _context = context;
            _mapper = mapper;
            _pageHelper = pageHelper;
            _thirdPartyGateway = thirdPartyGateway;
        }

        [HttpGet("")]
        public async Task<ActionResult<Envelope<Food>>> GetFoods(
            [FromQuery] PaginationDto paginationDto,
            string? name,
            int? cuisineId
        )
        {
            var query = _context.Foods
                .Include(f => f.Cuisine)
                .AsNoTracking();
            if(!string.IsNullOrEmpty(name))
                query = query.Where(f => f.Name.Contains(name));
            if (cuisineId is not null)
                query = query.Where(f => f.CuisineId.Equals(cuisineId));
            //paging with pagingHelper
            return Ok(await _pageHelper.GetPageAsync(query.ProjectTo<FoodDto>(_mapper.ConfigurationProvider), paginationDto));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FoodDto>> GetFoodById(int id)
        {
            var food = await _context.Foods.FirstOrDefaultAsync(f => f.Id.Equals(id));
            if (food is null)
                return BadRequest("Food Not Found");
            var result = _mapper.Map<FoodDto>(food);
            result.NutritionalValues = await _thirdPartyGateway.GetNutValues(food.Name!);

            return Ok(result);
        }

        [HttpGet("cuisine/{name}")]
        public async Task<ActionResult<Food>> GetFoodByCuisineName(string name)
        {
            var r = await _context.Foods
                .Include(f => f.Cuisine)
                .Where(f => f.Cuisine.Name.Equals(name))
                .ToListAsync();
            if (r is null)
                return BadRequest("Food Not Found");
            return Ok(r);
        }

        [HttpPost("")]
        public async Task<ActionResult<Food>> CreateFood(FoodRec model)
        {
            var food = _mapper.Map<Food>(model);
            await _context.AddAsync(food);
            await _context.SaveChangesAsync();
            return Ok(food);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Food>> UpdateFood([FromRoute]int id, [FromBody] FoodRecUpdate model)
        {
            var food = await _context.Foods.FirstOrDefaultAsync(f => f.Id.Equals(id));
            if (food is null)
                return BadRequest("Food Not Found");

            if(model.Name is not null or "")
                food.Name = model.Name;
            if(model.CuisineId is not null)
                food.CuisineId = model.CuisineId;

            await _context.SaveChangesAsync();
            return Ok(food);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Food>> DeleteFood([FromRoute] int id)
        {
            var food = await _context.Foods.FirstOrDefaultAsync(f => f.Id.Equals(id));
            if (food is null)
                return BadRequest("Food Not Found");

            _context.Remove(food);

            await _context.SaveChangesAsync();
            return Ok($"{food.Name} has been deleted");
        }
    }
}
