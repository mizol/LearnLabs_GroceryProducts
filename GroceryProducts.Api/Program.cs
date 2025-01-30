using GroceryProducts.Api.Database;
using GroceryProducts.Api.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

// In Program.cs (ASP.NET Core example)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GroceryDbContext>();

    if (dbContext.Database.GetPendingMigrations().Any())
    {
        Console.WriteLine("Pending migrations found. Applying...");
        dbContext.Database.Migrate(); // Apply pending migrations
        Console.WriteLine("Migrations applied.");
    }
    else
    {
        Console.WriteLine("No pending migrations found.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroceryProductEndpoints();

await app.RunAsync();


