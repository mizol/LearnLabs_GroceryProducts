using GroceryProducts.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GroceryProducts.Api.Database;

internal class GroceryDbContext : DbContext
{
    public GroceryDbContext(DbContextOptions<GroceryDbContext> options) : base(options) { }

    public DbSet<GroceryProduct> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.GroceryProducts);

        modelBuilder.ApplyConfiguration(new GroceryProductConfiguration());
        modelBuilder.ApplyConfiguration(new GroceryProductVectorConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
