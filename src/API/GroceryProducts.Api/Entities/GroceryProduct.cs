using System.ComponentModel.DataAnnotations;
namespace GroceryProducts.Api.Entities;

internal sealed class GroceryProduct
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(255, ErrorMessage = "Product name cannot exceed 255 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Category is required.")]
    [StringLength(100, ErrorMessage = "Category cannot exceed 100 characters.")]
    public string Category { get; set; }

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative integer.")]
    public int QuantityInStock { get; set; } = 0; 

    [Url(ErrorMessage = "Invalid URL format.")]
    public string? ImageUrl { get; set; } 

    [StringLength(255, ErrorMessage = "Brand cannot exceed 255 characters.")]
    public string? Brand { get; set; } 

    [StringLength(50, ErrorMessage = "Unit cannot exceed 50 characters.")]
    public string? Unit { get; set; } // e.g., "kg", "liter", "piece" - Make nullable

    [Required]
    [StringLength(255)]
    public string Slug { get; set; } // The slug field

    public static string GenerateSlug(string productName)
    {
        if (string.IsNullOrWhiteSpace(productName))
        {
            return ""; // Or handle as you see fit
        }

        string slug = productName.ToLowerInvariant();
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\s-]", ""); // Remove invalid chars
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s+", "-").Trim(); // Replace spaces with hyphens
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"-+", "-"); // Remove duplicate hyphens

        return slug;
    }
}
