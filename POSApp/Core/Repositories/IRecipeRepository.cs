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
        IEnumerable<Recipe> GetRecipes(int storeId);
        Recipe GetRecipeById(string ProductCode, string ingredientcode);
        IEnumerable<RecipeListViewModel> GetRecipes(int storeId, string productCode);
        void AddRecipes(Recipe tep);
        void UpdateRecipes(string id, string ingredientcode, Recipe tep);
        void DeleteRecipes(int id, int storeId);
    }
}