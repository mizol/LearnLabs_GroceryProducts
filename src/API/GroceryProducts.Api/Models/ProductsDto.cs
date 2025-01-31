using System.Text.Json.Serialization;

namespace GroceryProducts.Api.Models;

internal record GroceryProductDto
{
    public string Name { get; set; } = string.Empty;
    public int Id { get; set; }
    public string? Brand { get; set; }
    public string Category { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public int QuantityInStock { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string? Unit { get; set; }

    [JsonIgnore]
    public int TotalCount { get; set; }
}
