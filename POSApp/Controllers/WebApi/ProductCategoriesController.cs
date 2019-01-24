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
        public async Task<IHttpActionResult> GetProductCategories(int storeId)
        {
            return Ok(Mapper.Map<ProductCategoryViewModel[]>(await _unitOfWork.ProductCategoryRepository.GetProductCategoriesAsync(storeId)));
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