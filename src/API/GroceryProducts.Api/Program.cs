using GroceryProducts.Api.Database;
using GroceryProducts.Api.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
});

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

var logger = app.Services.GetService<ILogger<Program>>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<GroceryDbContext>();
    var pendingResult = await dbContext.Database.GetPendingMigrationsAsync();

    if (pendingResult.Any())
    {
        logger?.LogInformation("Pending migrations found. Applying...");
        await dbContext.Database.MigrateAsync();
        logger?.LogInformation("Migrations applied.");
    }
    else
    {
        logger?.LogInformation("No pending migrations found.");
    }
}

app.UseHttpsRedirection();

app.MapGroceryProductEndpoints();

await app.RunAsync();
