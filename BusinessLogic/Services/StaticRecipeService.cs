using BusinessLogic.Contracts;
using BusinessLogic.Data;
using BusinessLogic.Models;

namespace BusinessLogic.Services;

/// <summary>
/// Service welcher den Datenbankzugriff abbilden soll, vgl. <see cref="https://de.wikipedia.org/wiki/Repository_(Entwurfsmuster)"/>
/// Dieser Service bildet CRUD Operationen auf die Rezepte ab, <see cref="https://de.wikipedia.org/wiki/CRUD"/>.
/// </summary>
public class StaticRecipeService : IStaticRecipeService
{
    private readonly List<Recipe> _recipes = RecipeReader.FromJsonFile() ?? new List<Recipe>();

    public IEnumerable<Recipe> GetAll() => _recipes.AsEnumerable();

    public Recipe? GetById(long id) => _recipes.SingleOrDefault(x => x.Id == id);

    public long Add(Recipe recipe)
    {
        recipe.Id = _recipes.Max(x => x.Id) + 1;
        _recipes.Add(recipe);
        return recipe.Id;
    }

    public bool Update(Recipe recipe)
    {
        var existing = GetById(recipe.Id);
        if (existing is null)
        {
            return false;
        }

        var i = _recipes.IndexOf(existing);
        _recipes[i] = recipe;
        return true;
    }

    public bool Delete(long id)
    {
        var existing = GetById(id);
        return existing is not null
            && _recipes.Remove(existing);
    }
}
