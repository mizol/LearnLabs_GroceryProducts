using GroceryProducts.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GroceryProducts.Api.Database;

internal class GroceryProductConfiguration : IEntityTypeConfiguration<GroceryProduct>
{
    public void Configure(EntityTypeBuilder<GroceryProduct> builder)
    {
        builder.HasKey(p => p.Id);

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
            .HasPrecision(18,2);

        builder.Property(p => p.QuantityInStock)
            .IsRequired();

        builder.Property(p => p.ImageUrl)
            .HasMaxLength(2048);

        builder.Property(p => p.Brand)
            .HasMaxLength(255);

        builder.Property(p => p.Unit)
            .HasMaxLength(50);

        builder.Property(p => p.Slug)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(p => p.Name); 
        builder.HasIndex(p => p.Slug).IsUnique();

        // Seed data from JSON file
        var seedData = LoadSeedData();
        if (seedData != null)
        {
            builder.HasData(seedData);
        }
    }

    private List<GroceryProduct> LoadSeedData()
    {
        return SeedDataLoader.LoadSeedData<GroceryProduct>("grocery_products_seeds.json");
    }
}
