using System.ComponentModel.DataAnnotations;
using NpgsqlTypes;

namespace GroceryProducts.Api.Entities
{
    public class GroceryProductVector
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(255, ErrorMessage = "Product name cannot exceed 255 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(100, ErrorMessage = "Category cannot exceed 100 characters.")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a non-negative value.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative integer.")]
        public int QuantityInStock { get; set; } = 0; 

        [Url(ErrorMessage = "Invalid URL format.")]
        public string? ImageUrl { get; set; } 

        [StringLength(255, ErrorMessage = "Brand cannot exceed 255 characters.")]
        public string? Brand { get; set; } 

        [StringLength(50, ErrorMessage = "Unit cannot exceed 50 characters.")]
        public string? Unit { get; set; }

        [Required]
        [StringLength(255)]
        public string Slug { get; set; } = string.Empty;

        public NpgsqlTsVector SearchVector { get; set; }
    }
}
