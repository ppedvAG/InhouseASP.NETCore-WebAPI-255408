using BusinessLogic.Models.Enums;
using RecipeDbApi.Validation;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models;

public class CreateRecipeDto
{
    [Required(ErrorMessage = "Name is required")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
    [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
    public required string Name { get; set; }

    [ValidImage]
    public string? ImageUrl { get; set; }

    public string? Ingredients { get; set; }

    public string? Instructions { get; set; }

    public int PrepTimeMinutes { get; set; }

    public int CookTimeMinutes { get; set; }

    public int Servings { get; set; }

    public Difficulty Difficulty { get; set; }

    public string? Cuisine { get; set; }

    public string? MealType { get; set; }

    public float Rating { get; set; }

    public int CaloriesPerServing { get; set; }

    public int UserId { get; set; }

    public string[] Tags { get; set; }
}
