using GroceryProducts.Api.Database;
using GroceryProducts.Api.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GroceryDbContext>(options =>
    options
        .UseNpgsql(
            builder.Configuration.GetConnectionString("GroceryProductsDB")!,
            npgsqlOptions => npgsqlOptions
                .MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.GroceryProducts))
        .UseSnakeCaseNamingConvention());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<GroceryDbContext>();

    var pendingResult = await dbContext.Database.GetPendingMigrationsAsync();
    if (pendingResult.Any())
    {
        Console.WriteLine("Pending migrations found. Applying...");
        await dbContext.Database.MigrateAsync(); 
        Console.WriteLine("Migrations applied.");
    }
    else
    {
        Console.WriteLine("No pending migrations found.");
    }
}

app.UseHttpsRedirection();

app.MapGroceryProductEndpoints();

await app.RunAsync();