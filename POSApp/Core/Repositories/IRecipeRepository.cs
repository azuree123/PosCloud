using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;

namespace POSApp.Core.Repositories
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> GetAllRecipes(int storeId, string productCode);
        IEnumerable<Recipe> GetRecipes(int storeId);
        Recipe GetRecipeById(string ProductCode, string ingredientcode);
        IEnumerable<RecipeListViewModel> GetRecipes(int storeId, string productCode);
        void AddRecipes(Recipe tep);
        void UpdateRecipes(string id, string ingredientcode, Recipe tep);
        void DeleteRecipes(int id, int storeId);
        Task<IEnumerable<RecipeListViewModel>> GetRecipesAsync(int storeId);
        Task<Recipe> GetRecipeByIdAsync(string ProductCode, string ingredientcode);
        Task AddRecipesAsync(Recipe tep);
        Recipe GetRecipeById(int id, int storeId);
    }
}