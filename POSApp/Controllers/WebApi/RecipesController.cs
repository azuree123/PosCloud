using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;
using POSApp.Core.ViewModels.Sync;

namespace POSApp.Controllers.WebApi
{
    public class RecipesController : ApiController
    {

        private IUnitOfWork _unitOfWork;
        public RecipesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IHttpActionResult> GetRecipes(int storeId, bool forceFull, int deviceId)
        {
            var data = new object();
            if (forceFull)
            {
                data = await _unitOfWork.RecipeRepository.GetRecipesAsync(storeId);


                return Ok(Mapper.Map<RecipeListViewModel[]>(data));

            }
            else
            {

                var lastSync =
                    await _unitOfWork.IncrementalSyncronizationRepository.GetLastIncrementalSyncronization(storeId,
                        deviceId, "Recipes");
                if (lastSync == null)
                {
                    data = await _unitOfWork.RecipeRepository.GetRecipesAsync(storeId);
                }
                else
                {
                    data = await _unitOfWork.RecipeRepository.GetAllRecipesAsyncIncremental(storeId,
                        lastSync.LastSynced);
                }
                _unitOfWork.IncrementalSyncronizationRepository.AddIncrementalSyncronization(new IncrementalSyncronization()
                {
                    StoreId = storeId,
                    DeviceId = deviceId,
                    LastSynced = DateTime.Now,
                    TableName = "Recipes"

                });
                _unitOfWork.Complete();
                return Ok(Mapper.Map<RecipeListViewModel[]>(data));


            }


        }
        //public async Task<IHttpActionResult> GetRecipes(int storeId)
        //{
        //    return Ok((await _unitOfWork.RecipeRepository.GetRecipesAsync(storeId)));
        //}

        // GET: api/RecipesSync/5
        public async Task<IHttpActionResult> GetRecipe(string productcode, string ingredientcode)
        {
            return Ok(await _unitOfWork.RecipeRepository.GetRecipeByIdAsync(productcode, ingredientcode));
        }

        // POST: api/RecipesSync
        public async Task<IHttpActionResult> AddRecipes([FromBody]SyncObject sync)
        {
            try
            {
                List<Recipe> Recipes = System.Web.Helpers.Json.Decode<List<Recipe>>(sync.Object);
                foreach (var Recipe in Recipes)
                {
                    Recipe.Code = Recipe.Id.ToString();
                    Recipe.Synced = true;
                    Recipe.SyncedOn = DateTime.Now;
                    await _unitOfWork.RecipeRepository.AddRecipesAsync(Recipe);
                }
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new Exception("Error Occured While Adding");
                }
                return Ok("Success");
            }
            catch (Exception e)
            {
                return Ok("Error");
                throw;
            }
        }

        // PUT: api/RecipesSync/5
        public void UpdateRecipe(int id, [FromBody]string value)
        {
        }

        // DELETE: api/RecipesSync/5
        public void DeleteRecipe(int id)
        {
        }
    }
}
