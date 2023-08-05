namespace Catalog.Entities
{
    public class Cuisine
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public ICollection<Food>? Foods { get; set; }
    }
}
