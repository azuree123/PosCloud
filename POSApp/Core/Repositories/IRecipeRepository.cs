using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using POSApp.Core.Models;

namespace POSApp.Core.Repositories
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> GetRecipes(int storeid);
        Task<IEnumerable<Recipe>> GetRecipesAsync(int storeid);
        IEnumerable<Recipe> GetRecipes(string productCode, int storeid);
        Recipe GetRecipeById(string id, string comboProductId, int storeid);
        Task<Recipe> GetRecipeByIdAsync(string id, string comboProductId, int storeid);
        void AddRecipe(Recipe Recipes);
        Task AddRecipeAsync(Recipe Recipes);
        void UpdateRecipe(string id, string recipeProductId, Recipe Recipe, int storeid);
        void DeleteRecipe(string id, string recipeProductId, int storeid);
    }
}