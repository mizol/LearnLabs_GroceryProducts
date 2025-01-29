using GroceryProducts.Api.Database;
using GroceryProducts.Api.Entities;
using GroceryProducts.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace GroceryProducts.Api.Endpoints
{
    public class ProductEndpoints
    {
        public static void MapEndpoints(IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/products", GetProducts)
                .WithName("GetProducts")
                .WithOpenApi() // Enable Swagger documentation
                .Produces<PagedResult<GroceryProduct>>(StatusCodes.Status200OK) // Specify success response type
                .ProducesProblem(StatusCodes.Status500InternalServerError); // Specify error response

            routes.MapGet("/api/products/{id}", GetProductById)
                .WithName("GetProductById")
                .WithOpenApi()
                .Produces<GroceryProduct>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            routes.MapPost("/api/products", CreateProduct)
                .WithName("CreateProduct")
                .WithOpenApi()
                .Accepts<GroceryProduct>(MediaTypeNames.Application.Json) // Specify request body type for Swagger
                .Produces<GroceryProduct>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            routes.MapPut("/api/products/{id}", UpdateProduct)
                .WithName("UpdateProduct")
                .WithOpenApi()
                .Accepts<GroceryProduct>(MediaTypeNames.Application.Json)
                .Produces<GroceryProduct>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            routes.MapDelete("/api/products/{id}", DeleteProduct)
                .WithName("DeleteProduct")
                .WithOpenApi()
                .Produces(StatusCodes.Status204NoContent)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);
        }

        
        private static async Task<IResult> GetProducts(GroceryDbContext db, string? searchTerm = null, string? slug = null, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            IQueryable<GroceryProduct> query = db.Products;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                         p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(slug))
            {
                query = query.Where(p => p.Slug == slug);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var pagedResult = new PagedResult<GroceryProduct>(products, totalCount, page, pageSize);

            return Results.Ok(pagedResult);
        }

        private static async Task<IResult> GetProductById(GroceryDbContext db, int id, CancellationToken cancellationToken)
        {
            var product = await db.Products.FindAsync(new object[] { id }, cancellationToken); // Use FindAsync with object[] for composite keys if needed
            return product == null ? Results.NotFound() : Results.Ok(product);
        }

        private static async Task<IResult> CreateProduct(GroceryDbContext db, GroceryProduct product, CancellationToken cancellationToken)
        {
            if (!db.Products.Any(p => p.Slug == product.Slug)) // Check slug uniqueness
            {
                db.Products.Add(product);
                await db.SaveChangesAsync(cancellationToken);
                return Results.Created($"/api/products/{product.Id}", product); // Return 201 Created with location header
            }
            return Results.BadRequest(new { message = "Slug already exists" }); // Return 400 Bad Request for duplicate slug
        }


        private static async Task<IResult> UpdateProduct(GroceryDbContext db, int id, GroceryProduct product, CancellationToken cancellationToken)
        {
            if (id != product.Id)
            {
                return Results.BadRequest(new { message = "IDs do not match" });
            }

            var existingProduct = await db.Products.FindAsync(new object[] { id }, cancellationToken);
            if (existingProduct == null)
            {
                return Results.NotFound();
            }

            // Update only the properties that should be modified. This prevents overposting.
            existingProduct.Name = product.Name;
            existingProduct.Category = product.Category;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Slug = product.Slug;
            // ... Update other properties as needed

            try
            {
                await db.SaveChangesAsync(cancellationToken);
                return Results.Ok(existingProduct); // Return the updated product
            }
            catch (DbUpdateConcurrencyException) // Handle concurrency issues
            {
                return Results.Conflict(); // Or another appropriate status code
            }
        }

        private static async Task<IResult> DeleteProduct(GroceryDbContext db, int id, CancellationToken cancellationToken)
        {
            var product = await db.Products.FindAsync(new object[] { id }, cancellationToken);
            if (product == null)
            {
                return Results.NotFound();
            }

            db.Products.Remove(product);
            await db.SaveChangesAsync(cancellationToken);
            return Results.NoContent(); // 204 No Content
        }
    }
}
