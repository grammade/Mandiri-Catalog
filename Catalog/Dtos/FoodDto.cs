using Newtonsoft.Json;

namespace Catalog.Dtos
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class FoodDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CuisineName { get; set; }
        public NutValues? NutritionalValues { get; set; }
    }

    public record FoodRec(string Name, int CuisineId);
    public record FoodRecUpdate(string? Name, int? CuisineId);
    public record FoodRecOrder(List<FoodRecOrderItems> foods, int userId);
    public record FoodRecOrderItems(int id, string name);
}
