using BusinessLogic.Models;

namespace BusinessLogic.Contracts
{
    public interface IStaticRecipeService
    {
        long Add(Recipe recipe);
        bool Delete(long id);
        IEnumerable<Recipe> GetAll();
        Recipe? GetById(long id);
        bool Update(Recipe recipe);
    }
}