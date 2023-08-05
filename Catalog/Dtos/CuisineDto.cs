namespace Catalog.Dtos
{
    public class CuisineDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public record CuisineRec(string name);
}
