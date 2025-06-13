using BusinessLogic.Models;

namespace BusinessLogic.Contracts
{
    public interface IRecipeService
    {
        Task<long> Add(Recipe recipe);
        Task<bool> Delete(long id);
        Task<List<Recipe>> GetAll();
        Task<Recipe?> GetById(long id);
        Task<bool> Update(Recipe recipe);
    }
}