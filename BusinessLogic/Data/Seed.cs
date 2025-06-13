using BusinessLogic.Models;
using BusinessLogic.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Data;

public class Seed
{
    private const string DEFAULT_USER_NAME = "Guest";
    private const int FIRST_ORDER_ID = 1;
    private const int FIRST_RECIPE_ID = 1;
    private const int SECOND_RECIPE_ID = 2;

    internal static void SeedData(ModelBuilder modelBuilder)
    {
        var recipes = RecipeReader.FromJsonFile("Data\\RecipeData.json");
        if (recipes.Count == 0)
        {
            throw new InvalidDataException("No recipes found.");
        }

        var orderItems = new List<OrderItem>
        {
            new OrderItem
            {
                Id = 1,
                OrderId = FIRST_ORDER_ID,
                RecipeId = FIRST_RECIPE_ID,
                Quantity = 2
            },
            new OrderItem
            {
                Id = 2,
                OrderId = FIRST_ORDER_ID,
                RecipeId = SECOND_RECIPE_ID,
                Quantity = 1
            }
        };

        var orders = new List<Order>
        {
            new Order
            {
                Id = FIRST_ORDER_ID,
                // Nicht befuellen da sonst Fehler bei Migration
                //OrderItems = orderItems
                UserName = DEFAULT_USER_NAME,
                OrderDate = new DateTime(2023, 01, 01),
                Rating = 4.8f,
                Status = OrderStatus.Pending
            }
        };

        modelBuilder.Entity<Recipe>().HasData(recipes);
        modelBuilder.Entity<OrderItem>().HasData(orderItems);
        modelBuilder.Entity<Order>().HasData(orders);
    }
}
