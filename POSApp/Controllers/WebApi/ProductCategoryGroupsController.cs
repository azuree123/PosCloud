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
    public class ProductCategoryGroupsController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public ProductCategoryGroupsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/ProductCategoryGroups
        public async Task<IHttpActionResult> GetProductCategoryGroups(int storeId)
        {
            return Ok(Mapper.Map<ProductCategoryGroupViewModel[]>(_unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroups(storeId)));
        }

        // GET: api/ProductCategoryGroupCategoriesSync/5
        public async Task<IHttpActionResult> GetProductCategoryGroup(int id, int storeId)
        {
            return Ok(_unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroup(id, storeId));
        }

        // POST: api/ProductCategoryGroupCategoriesSync
        public async Task<IHttpActionResult> AddProductCategoryGroups([FromBody]SyncObject sync)
        {
            try
            {
                List<ProductCategoryGroup> productCategoryGroups = System.Web.Helpers.Json.Decode<List<ProductCategoryGroup>>(sync.Object);
                foreach (var productCategoryGroup in productCategoryGroups)
                {
                    productCategoryGroup.Code = productCategoryGroup.Id.ToString();
                    productCategoryGroup.Synced = true;
                    productCategoryGroup.SyncedOn = DateTime.Now;
                    _unitOfWork.ProductCategoryGroupRepository.AddProductCategoryGroup(productCategoryGroup);
                }
                _unitOfWork.Complete();
                //int s = 1;
                //sync.Status = "Success";
                return Ok(1);
            }
            catch (Exception e)
            {
                //sync.Status = "Error";
                return Ok(0);
            }
        }

        // PUT: api/ProductCategoryGroupCategoriesSync/5
        public void UpdateProductCategoryGroup(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProductCategoryGroupCategoriesSync/5
        public void DeleteProductCategoryGroup(int id)
        {
        }
    }
}
