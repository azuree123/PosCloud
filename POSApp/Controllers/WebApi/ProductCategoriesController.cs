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
    public class ProductCategoriesController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public ProductCategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetProductCategories(int storeId, bool forceFull, int deviceId)
        {
            var data = new object();
            if (forceFull)
            {
                data = await _unitOfWork.ProductCategoryRepository.GetProductCategoriesAsync(storeId);


                return Ok(Mapper.Map<ProductCategoryViewModel[]>(data));

            }
            else
            {

                var lastSync =
                    await _unitOfWork.IncrementalSyncronizationRepository.GetLastIncrementalSyncronization(storeId,
                        deviceId, "ProductCategories");
                if (lastSync == null)
                {
                    data = await _unitOfWork.ProductCategoryRepository.GetProductCategoriesAsync(storeId);
                }
                else
                {
                    data = await _unitOfWork.ProductCategoryRepository.GetAllProductCategoryAsyncIncremental(storeId,
                        lastSync.LastSynced);
                }
                _unitOfWork.IncrementalSyncronizationRepository.AddIncrementalSyncronization(new IncrementalSyncronization()
                {
                    StoreId = storeId,
                    DeviceId = deviceId,
                    LastSynced = DateTime.Now,
                    TableName = "ProductCategories"

                });
                _unitOfWork.Complete();
                return Ok(Mapper.Map<ProductCategoryViewModel[]>(data));


            }


        }

        // GET: api/ProductCategoryCategoriesSync/5
        public async Task<IHttpActionResult> GetProductCategory(int id, int storeId)
        {
            return Ok(await _unitOfWork.ProductCategoryRepository.GetProductCategoryByIdAsync(id, storeId));
        }

        // POST: api/ProductCategoryCategoriesSync
        public async Task<IHttpActionResult> AddProductCategories([FromBody]SyncObject sync)
        {
            try
            {
                List<ProductCategory> productCategories = System.Web.Helpers.Json.Decode<List<ProductCategory>>(sync.Object);
                foreach (var productCategory in productCategories)
                {
                    productCategory.Code = productCategory.Id.ToString();
                    productCategory.Synced = true;
                    productCategory.SyncedOn = DateTime.Now;
                    await _unitOfWork.ProductCategoryRepository.AddProductCategoryAsync(productCategory);
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

        // PUT: api/ProductCategoryCategoriesSync/5
        public void UpdateProductCategory(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProductCategoryCategoriesSync/5
        public void DeleteProductCategory(int id)
        {
        }
    }
}