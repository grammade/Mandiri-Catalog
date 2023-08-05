using AutoMapper;
using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog.MappingProfiles
{
    public class EntityMapping : Profile
    {
        public EntityMapping()
        {
            CreateMap<Food, FoodDto>().ReverseMap()
                .ForPath(d => d.Cuisine.Name, opt => opt.MapFrom(s => s.CuisineName));
            CreateMap<Food, FoodRec>().ReverseMap();

            CreateMap<Cuisine, CuisineDto>().ReverseMap();
            CreateMap<Cuisine, CuisineRec>().ReverseMap();

        }
    }
}
