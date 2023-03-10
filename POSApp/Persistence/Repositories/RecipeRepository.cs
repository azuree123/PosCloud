using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using POSApp.Core.Models;
using POSApp.Core.Repositories;
using POSApp.Core.ViewModels;

namespace POSApp.Persistence.Repositories
{
    public class RecipeRepository: IRecipeRepository
    {

        private PosDbContext _context;

        public RecipeRepository(PosDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Recipe>> GetAllRecipesAsyncIncremental(int storeId, DateTime date)
        {
            return await _context.Recipes.Where(a => a.StoreId == storeId && a.Product.InventoryItem && !a.IsDisabled && (a.UpdatedOn >= date || a.CreatedOn >= date)).ToListAsync();
        }

        public IEnumerable<Recipe> GetRecipes(int storeId)
        {
            return _context.Recipes.Where(a=>a.StoreId == storeId && a.Product.InventoryItem).ToList();
        }
        public IEnumerable<Recipe> GetAllRecipes(int storeId,string productCode)
        {
            return _context.Recipes.Include(a=>a.Ingredient).Where(a => a.StoreId == storeId && a.ProductCode==productCode).ToList();
        }
        public async Task<IEnumerable<RecipeListViewModel>> GetRecipesAsync(int storeId)
        {
            return await _context.Recipes.Where(a => a.StoreId == storeId).Select(a => new RecipeListViewModel
            {
                Id = a.Id,
                StoreId = a.StoreId,
                Quantity = a.Quantity
                ,
                Calories = a.Calories,
                IngredientName = a.Ingredient.Name,
                
                ExpiryDate = a.ExpiryDate,
                ProductCode = a.ProductCode
            }).ToListAsync(); 
        }
        public IEnumerable<RecipeListViewModel> GetRecipes(int storeId,string productCode)
        {
            return _context.Recipes.Include(a=>a.Ingredient).Where(a => a.StoreId == storeId && a.ProductCode==productCode).Select(a=>new RecipeListViewModel
            {
                Id = a.Id,
                StoreId = a.StoreId,
                Quantity = a.Quantity
               ,Calories = a.Calories,
                IngredientName =a.Ingredient.Name,
               
                ExpiryDate = a.ExpiryDate,
                ProductCode = a.ProductCode
            }).ToList();
        }
        public Recipe GetRecipeById(int id, int storeId)
        {
            return _context.Recipes.FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
        }
        public Recipe GetRecipeById(string ProductCode, string ingredientcode)
        {
            return _context.Recipes.FirstOrDefault(a => a.ProductCode == ProductCode && a.IngredientCode == ingredientcode);
        }
        public async Task<Recipe> GetRecipeByIdAsync(string ProductCode, string ingredientcode)
        {
            return await _context.Recipes.FirstOrDefaultAsync(a => a.ProductCode == ProductCode && a.IngredientCode == ingredientcode);
        }
        public void AddRecipes(Recipe tep)
        {
            var inDb = _context.Recipes.FirstOrDefault(a =>
                a.ProductCode == tep.ProductCode && a.IngredientCode == tep.IngredientCode);
            if (inDb != null)
            {
                inDb.Calories += tep.Calories;
                inDb.Quantity += tep.Quantity;
                
            }
            else
            {
                _context.Recipes.Add(tep);
            }
            //if (!_context.Recipes.Where(a => a.ProductCode == tep.ProductCode && a.IngredientCode == tep.IngredientCode).Any())
            //{
                
            //}
        }
        public async Task AddRecipesAsync(Recipe tep)
        {

            if (!await _context.Recipes.Where(a => a.ProductCode == tep.ProductCode && a.IngredientCode == tep.IngredientCode).AnyAsync())
            {
                 _context.Recipes.Add(tep);
            }
        }
        public void UpdateRecipes(string id, string ingredientcode, Recipe tep)
        {
            if (tep.ProductCode != id)
            {
                tep.ProductCode = id;
            }
            else { }
            if (tep.IngredientCode != ingredientcode)
            {
                tep.IngredientCode = ingredientcode;
            }
            else { }

            //  tep.TimedEventStoreId = storeId;

            _context.Recipes.Attach(tep);
            _context.Entry(tep).State = EntityState.Modified;
        }
        public void DeleteRecipes(int id, int storeId)
        {
            Recipe product = _context.Recipes
                .FirstOrDefault(a => a.Id == id && a.StoreId == storeId);
                _context.Recipes.Remove(product);
            
        }
    }
}