using System.Text.Json;

namespace GroceryProducts.Api.Database;

internal static class SeedDataLoader
{
    public static List<T> LoadSeedData<T>(string fileName, string pathToSeeds = "Database/Seeds")
    {
        List<T>? data = new List<T>();

        var filePath = Path.Combine(AppContext.BaseDirectory, pathToSeeds, fileName);

        try
        {
            var json = File.ReadAllText(filePath);
            data = JsonSerializer.Deserialize<List<T>>(json);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Seed file not found at '{filePath}'.");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error deserializing seed data: {ex.Message}");
        }

        return data!;
    }
}
