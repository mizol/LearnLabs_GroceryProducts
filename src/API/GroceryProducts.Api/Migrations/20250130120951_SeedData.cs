﻿// <auto-generated />
using GroceryProducts.Api.Database;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GroceryProducts.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "grocery_products");

            migrationBuilder.InsertData(
                schema: "grocery_products",
                table: "products",
                columns: new[] { "id", "brand", "category", "description", "image_url", "name", "price", "quantity_in_stock", "slug", "unit" },
                values: new object[,]
                {
                    { 1, null, "Fruits", "Fresh, juicy apples from local farms.", null, "Organic Apples", 1.50m, 0, "organic-apples", null },
                    { 2, null, "Dairy", "1 gallon of fresh whole milk.", null, "Whole Milk", 3.99m, 0, "whole-milk", null },
                    { 3, null, "Dairy", "A dozen fresh, organic brown eggs.", null, "Brown Eggs - 12 count", 4.50m, 0, "brown-eggs-12-count", null },
                    { 4, null, "Fruits", "Ripe and ready to eat.", null, "Bananas", 0.50m, 0, "bananas", null },
                    { 5, null, "Vegetables", "Fresh, crunchy carrots.", null, "Carrots", 0.75m, 0, "carrots", null },
                    { 6, null, "Meat & Seafood", "Boneless, skinless chicken breast.", null, "Chicken Breast", 4.99m, 0, "chicken-breast", null },
                    { 7, null, "Meat & Seafood", "Fresh salmon fillet.", null, "Salmon Fillet", 9.99m, 0, "salmon-fillet", null },
                    { 8, null, "Dairy", "Sharp cheddar cheese.", null, "Cheddar Cheese", 3.50m, 0, "cheddar-cheese", null },
                    { 9, null, "Dairy", "Plain yogurt.", null, "Yogurt", 2.00m, 0, "yogurt", null },
                    { 10, null, "Bakery", "Freshly baked bread.", null, "Bread", 2.50m, 0, "bread", null },
                    { 11, null, "Pantry", "Spaghetti pasta.", null, "Pasta", 1.25m, 0, "pasta", null },
                    { 12, null, "Pantry", "Long grain rice.", null, "Rice", 1.75m, 0, "rice", null },
                    { 13, null, "Pantry", "Extra virgin olive oil.", null, "Olive Oil", 5.99m, 0, "olive-oil", null },
                    { 14, null, "Vegetables", "Fresh, ripe tomatoes.", null, "Tomatoes", 0.99m, 0, "tomatoes", null },
                    { 15, null, "Vegetables", "Crisp lettuce.", null, "Lettuce", 1.25m, 0, "lettuce", null },
                    { 16, null, "Meat & Seafood", "Tender beef steak.", null, "Beef Steak", 8.99m, 0, "beef-steak", null },
                    { 17, null, "Meat & Seafood", "Fresh shrimp.", null, "Shrimp", 7.99m, 0, "shrimp", null },
                    { 18, null, "Dairy", "2% milk.", null, "Milk", 3.00m, 0, "milk", null },
                    { 19, null, "Dairy", "Unsalted butter.", null, "Butter", 2.75m, 0, "butter", null },
                    { 20, null, "Bakery", "Freshly baked bagels.", null, "Bagels", 1.50m, 0, "bagels", null },
                    { 21, null, "Pantry", "Breakfast cereal.", null, "Cereal", 3.25m, 0, "cereal", null },
                    { 22, null, "Pantry", "Ground coffee.", null, "Coffee", 4.50m, 0, "coffee", null },
                    { 23, null, "Pantry", "Black tea bags.", null, "Tea", 2.00m, 0, "tea", null },
                    { 24, null, "Pantry", "Granulated sugar.", null, "Sugar", 1.75m, 0, "sugar", null },
                    { 25, null, "Bakery", "Freshly baked whole wheat bread.", null, "Whole Wheat Bread", 2.75m, 0, "whole-wheat-bread", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "grocery_products",
                table: "products",
                keyColumn: "id",
                keyValue: 25);

            migrationBuilder.RenameTable(
                name: "products",
                schema: "grocery_products",
                newName: "products");
        }
    }
}
