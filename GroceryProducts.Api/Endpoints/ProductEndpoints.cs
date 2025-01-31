using GroceryProducts.Api.Database;
using GroceryProducts.Api.Entities;
using GroceryProducts.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace GroceryProducts.Api.Endpoints
{
    public static class GroceryProductEndpoints
    {
        public static void MapGroceryProductEndpoints(this IEndpointRouteBuilder routes)
        {
            routes.MapGet("/api/products", GetProducts)
                .WithName("GetProducts")
                .WithOpenApi()
                .Produces<PagedResult<GroceryProduct>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            routes.MapGet("/api/products/function-search", GetProductsByFunction)
                .WithName("GetProductsByFunctionSearch")
                .WithOpenApi()
                .Produces<PagedResult<GroceryProduct>>(StatusCodes.Status200OK) 
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            routes.MapGet("/api/products/fulltext", GetProductsFullTextSearch)
                .WithName("GetProductsByFullTextSearch")
                .WithOpenApi()
                .Produces<PagedResult<GroceryProduct>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            routes.MapGet("/api/products/{id}", GetProductById)
                .WithName("GetProductById")
                .WithOpenApi()
                .Produces<GroceryProduct>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);

            routes.MapPost("/api/products", CreateProduct)
                .WithName("CreateProduct")
                .WithOpenApi()
                .Accepts<GroceryProduct>(MediaTypeNames.Application.Json)
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
        
        private static async Task<IResult> GetProducts(
            GroceryDbContext db, 
            string? searchTerm = null,
            int page = 1, 
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            IQueryable<GroceryProduct> query = db.Products;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();

                query = query.Where(p => EF.Functions.Like(p.Name.ToLower(), $"%{searchTerm}%") ||
                             EF.Functions.Like(p.Category.ToLower(), $"%{searchTerm}%") ||
                             (p.Description != null && EF.Functions.Like(p.Description.ToLower(), $"%{searchTerm}%")) ||
                             EF.Functions.Like(p.Slug.ToLower(), $"%{searchTerm}%"));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var pagedResult = new PagedResult<GroceryProduct>(products, totalCount, page, pageSize);

            return Results.Ok(pagedResult);
        }

        private static async Task<IResult> GetProductsByFunction(
            GroceryDbContext db,
            string? searchTerm = null,
            int page = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            object searchParam = string.IsNullOrEmpty(searchTerm) ? DBNull.Value : searchTerm;

            var productsDto = await db.Database
                .SqlQueryRaw<GroceryProductDto>(@"
                    SELECT name, id, brand, category, description, image_url, price, quantity_in_stock, slug, unit, total_count
                    FROM GetPagedSearchResults({0}, {1}, {2})",
                    searchParam, page, pageSize)
                .ToListAsync(cancellationToken);

            if (!productsDto.Any())
            {
                return Results.Ok(
                    new PagedResult<GroceryProductDto>(new List<GroceryProductDto>(), 0, page, pageSize));
            }

            int totalCount = productsDto[0].TotalCount;
            var pagedResult = new PagedResult<GroceryProductDto>(productsDto, totalCount, page, pageSize);

            return Results.Ok(pagedResult);
        }

        private static async Task<IResult> GetProductsFullTextSearch(
            GroceryDbContext db,
            string searchTerm,
            int page = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            IQueryable<GroceryProduct> query = db.Products;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p =>
                    EF.Functions
                        .ToTsVector("english", p.Name + " " + p.Category + " " + p.Description + " " + p.Slug)
                        .Matches(EF.Functions.PhraseToTsQuery("english", searchTerm)))
                    .AsNoTracking();
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
            var product = await db.Products.FindAsync(new object[] { id }, cancellationToken);
            return product == null ? Results.NotFound() : Results.Ok(product);
        }

        private static async Task<IResult> CreateProduct(GroceryDbContext db, GroceryProduct product, CancellationToken cancellationToken)
        {
            if (!db.Products.Any(p => p.Slug == product.Slug)) 
            {
                db.Products.Add(product);
                await db.SaveChangesAsync(cancellationToken);
                return Results.Created($"/api/products/{product.Id}", product); 
            }
            return Results.BadRequest(new { message = "Slug already exists" }); 
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

            existingProduct.Name = product.Name;
            existingProduct.Category = product.Category;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Slug = product.Slug;

            try
            {
                await db.SaveChangesAsync(cancellationToken);
                return Results.Ok(existingProduct);
            }
            catch (DbUpdateConcurrencyException)
            {
                // Logging...
                return Results.Conflict();
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
            return Results.NoContent();
        }
    }
}
