﻿namespace Catalog.Entities
{
    public class Food
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public int? CuisineId { get; set; }
        public Cuisine? Cuisine { get; set; }
    }
}
