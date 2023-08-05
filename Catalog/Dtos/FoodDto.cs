﻿namespace Catalog.Dtos
{
    public class FoodDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CuisineName { get; set; }
    }

    public record FoodRec(string Name, int CuisineId);
    public record FoodRecUpdate(string? Name, int? CuisineId);
}