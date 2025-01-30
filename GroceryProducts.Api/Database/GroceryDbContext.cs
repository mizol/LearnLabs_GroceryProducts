using GroceryProducts.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GroceryProducts.Api.Database
{
    public class GroceryDbContext : DbContext
    {
        public GroceryDbContext(DbContextOptions<GroceryDbContext> options) : base(options) { }

        public DbSet<GroceryProduct> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema(Schemas.GroceryProducts);

            modelBuilder.HasDefaultSchema(Schemas.GroceryProducts);

            modelBuilder.ApplyConfiguration(new GroceryProductConfiguration());

            // If you have other entities and configurations, apply them here as well:
            // modelBuilder.ApplyConfiguration(new AnotherEntityConfiguration()); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
