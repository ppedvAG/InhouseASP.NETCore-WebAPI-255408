using BusinessLogic.Models.Enums;

namespace BusinessLogic.Models;

public class RecipeSearchParams
{
    public Difficulty? Difficulty { get; set; }
    public string? Cuisine { get; set; }
    public string? MealType { get; set; }
}