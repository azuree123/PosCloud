using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;

namespace POSApp.Persistence.Repositories
{
    public class RecipeRepository: IRecipeRepository
    {

        private PosDbContext _context;

        public RecipeRepository(PosDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Recipe> GetRecipes(int storeid)
        {
            return _context.Recipes.Where(a => a.StoreId == storeid && !a.IsDisabled).ToList();
        }
        public async Task<IEnumerable<Recipe>> GetRecipesAsync(int storeid)
        {
            return await _context.Recipes.Where(a => a.StoreId == storeid && !a.IsDisabled).ToListAsync();
        }
        public IEnumerable<Recipe> GetRecipes(string productCode, int storeid)
        {
            return _context.Recipes.Where(a => a.StoreId == storeid && !a.IsDisabled && a.IngredientCode == productCode).ToList();
        }

        public Recipe GetRecipeById(string id, string comboProductId, int storeid)
        {
            return _context.Recipes.Find(id, comboProductId, storeid);
        }
        public async Task<Recipe> GetRecipeByIdAsync(string id, string comboProductId, int storeid)
        {
            return await _context.Recipes.FindAsync(id, comboProductId, storeid);
        }
        public void AddRecipe(Recipe Recipes)
        {
            var inDb = _context.Recipes.FirstOrDefault(a =>
                a.ProductCode == Recipes.ProductCode && a.IngredientCode == Recipes.IngredientCode &&
                a.StoreId == Recipes.StoreId);
            if (inDb == null)
            {
                _context.Recipes.Add(Recipes);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    Recipes.IngredientCode = inDb.IngredientCode;
                    _context.Entry(inDb).CurrentValues.SetValues(Recipes);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }

        }
        public async Task AddRecipeAsync(Recipe Recipes)
        {
            var inDb = await _context.Recipes.FirstOrDefaultAsync(a =>
                 a.ProductCode == Recipes.ProductCode && a.IngredientCode == Recipes.IngredientCode &&
                 a.StoreId == Recipes.StoreId);
            if (inDb == null)
            {
                _context.Recipes.Add(Recipes);
            }
            else
            {
                if (inDb.IsDisabled)
                {
                    Recipes.IngredientCode = inDb.IngredientCode;
                    _context.Entry(inDb).CurrentValues.SetValues(Recipes);
                    _context.Entry(inDb).State = EntityState.Modified;
                }
                else
                {
                    throw new Exception("Entity Already Exists!");
                }
            }

        }
        public void UpdateRecipe(string id, string recipeProductId, Recipe Recipe, int storeid)
        {
            if (Recipe.ProductCode != id)
            {
                Recipe.ProductCode = id;
            }
            else { }
            if (Recipe.IngredientCode != recipeProductId)
            {
                Recipe.IngredientCode = recipeProductId;
            }
            else { }

            Recipe.StoreId = storeid;
            _context.Recipes.Attach(Recipe);
            _context.Entry(Recipe).State = EntityState.Modified;
        }

        public void DeleteRecipe(string id, string recipeProductId, int storeid)
        {

            var recipe = _context.Recipes.FirstOrDefault(a => a.ProductCode == id && a.IngredientCode == recipeProductId && a.StoreId == storeid);
            recipe.IsDisabled = true;
            _context.Recipes.Attach(recipe);
            _context.Entry(recipe).State = EntityState.Modified;
        }
        public IEnumerable<Recipe> GetApiRecipes()
        {
            IEnumerable<Recipe> Recipes = _context.Recipes.Where(a => !a.Synced).ToList();
            foreach (var Recipe in Recipes)
            {
                Recipe.Synced = true;
                Recipe.SyncedOn = DateTime.Now;
            }

            _context.SaveChanges();
            return Recipes;
        }
    }
}