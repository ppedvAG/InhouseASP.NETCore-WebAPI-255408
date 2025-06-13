using BusinessLogic.Models;

namespace RecipeDbApi.Mappers;

/// <summary>
/// Mappers um DomainModels von sog. DTOs zu entkoppeln und zu transformieren
/// Alternativen: AutoMapper, Mapster
///     Kritikpunkte: 
///         1. Komplexitaet?
///         2. Performance weil Laufzeit
///         3. Verlust der "Kontrolle"
///         4. Linzenz fuer AutoMapper hat sich geaendert
/// Weitere Alternative: mapperly (https://github.com/riok/mapperly)
///     Mapping findet zur Compilationzeit statt
/// </summary>
public static class RecipeMappers
{
    public static RecipeDto ToDto(this Recipe recipe)
    {
        // Vorteil: Wir koennen hier Daten transformieren koennen, d. h. 
        // evtl. entspricht das Dto nicht dem DomainModel aus der DB
        // Hier koennten wir Currency oder DateTimes formatieren etc.
        return new RecipeDto
        {
            Id = recipe.Id,
            Name = recipe.Name,
            CaloriesPerServing = recipe.CaloriesPerServing,
            Servings = recipe.Servings,
            Instructions = string.Join(Environment.NewLine, recipe.Instructions),
            Ingredients = string.Join(Environment.NewLine, recipe.Ingredients),
            Tags = recipe.Tags,
            ImageUrl = recipe.ImageUrl,
            CookTimeMinutes = recipe.CookTimeMinutes,
            PrepTimeMinutes = recipe.PrepTimeMinutes,
            Cuisine = recipe.Cuisine,
            Difficulty = recipe.Difficulty,
            MealType = recipe.MealType.First(),
            Rating = recipe.Rating
        };
    }

    public static Recipe ToDomainModel(this CreateRecipeDto recipe)
    {
        // Vorteil: Wir koennen hier Daten transformieren koennen, d. h. 
        // evtl. entspricht das Dto nicht dem DomainModel aus der DB
        // Hier koennten wir Currency oder DateTimes formatieren etc.
        return new Recipe
        {
            Name = recipe.Name,
            CaloriesPerServing = recipe.CaloriesPerServing,
            Servings = recipe.Servings,
            Instructions = recipe.Instructions.Split(Environment.NewLine),
            Ingredients = recipe.Ingredients.Split(Environment.NewLine),
            Tags = recipe.Tags,
            ImageUrl = recipe.ImageUrl,
            CookTimeMinutes = recipe.CookTimeMinutes,
            PrepTimeMinutes = recipe.PrepTimeMinutes,
            Cuisine = recipe.Cuisine,
            Difficulty = recipe.Difficulty,
            MealType = [recipe.MealType],
            Rating = recipe.Rating
        };
    }
}
