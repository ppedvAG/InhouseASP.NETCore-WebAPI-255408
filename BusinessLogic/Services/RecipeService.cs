using BusinessLogic.Contracts;
using BusinessLogic.Data;
using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services;

public class RecipeService : IRecipeService
{
    private readonly ApplicationDbContext _context;

    public RecipeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Recipe>> GetAll()
    {
        return await _context.Recipes.ToListAsync();
    }

    public async Task<Recipe?> GetById(long id)
    {
        return await _context.Recipes.FindAsync(id);
    }

    public async Task<long> Add(Recipe recipe)
    {
        await _context.Recipes.AddAsync(recipe);
        await _context.SaveChangesAsync();
        return recipe.Id;
    }

    public async Task<bool> Update(Recipe recipe)
    {
        var existingRecipe = await _context.Recipes.FindAsync(recipe.Id);
        if (existingRecipe != null)
        {
            _context.Entry(existingRecipe).CurrentValues.SetValues(recipe);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> Delete(long id)
    {
        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe != null)
        {
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
