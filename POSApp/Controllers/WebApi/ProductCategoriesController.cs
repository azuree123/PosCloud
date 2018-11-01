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
            return Ok(Mapper.Map<ProductCategoryViewModel[]>(_unitOfWork.ProductCategoryRepository.GetProductCategories(storeId)));
        }

        // GET: api/ProductCategoryCategoriesSync/5
        public async Task<IHttpActionResult> GetProductCategory(int id, int storeId)
        {
            return Ok(_unitOfWork.ProductCategoryRepository.GetProductCategoryById(id, storeId));
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
                    _unitOfWork.ProductCategoryRepository.AddProductCategory(productCategory);
                }
                _unitOfWork.Complete();
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