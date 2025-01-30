using GroceryProducts.Api.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json;

namespace GroceryProducts.Api.Database
{
    public class GroceryProductConfiguration : IEntityTypeConfiguration<GroceryProduct>
    {
        public void Configure(EntityTypeBuilder<GroceryProduct> builder)
        {
            builder.HasKey(p => p.Id); // Primary key

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .IsRequired();

            builder.Property(p => p.Price)
                .IsRequired()
                .HasPrecision(18,2); // Important for decimal precision

            builder.Property(p => p.QuantityInStock)
                .IsRequired();

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(2048); // Adjust as needed

            builder.Property(p => p.Brand)
                .HasMaxLength(255);

            builder.Property(p => p.Unit)
                .HasMaxLength(50);

            builder.Property(p => p.Slug)
                .IsRequired()
                .HasMaxLength(255);
                //.IsUnique(); // Enforce slug uniqueness in the database


            // Example of Index for searching
            builder.HasIndex(p => p.Name); // Add index for name searching
            builder.HasIndex(p => p.Slug).IsUnique(); // Index and ensure uniqueness

            // If you have a relationship with another entity (e.g., Supplier):
            // builder.HasOne(p => p.Supplier)
            //        .WithMany(s => s.Products)
            //        .HasForeignKey(p => p.SupplierId);

            // Add other configurations as needed (e.g., indexes, relationships, etc.)

            // Seed data from JSON file
            var seedData = LoadSeedData();
            if (seedData != null)
            {
                builder.HasData(seedData);
            }
        }

        private List<GroceryProduct> LoadSeedData()
        {
            try
            {
                var filePath = Path.Combine(AppContext.BaseDirectory, 
                    "Database/Seeds",
                    "grocery_products_seeds.json");

                var json = File.ReadAllText(filePath);
                
                return JsonSerializer.Deserialize<List<GroceryProduct>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading seed data: {ex.Message}");

                throw;
            }
        }
    }
}
