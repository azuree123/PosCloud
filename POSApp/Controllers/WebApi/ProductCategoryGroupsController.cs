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

        public async Task<IHttpActionResult> GetSection(int id, int storeId)
        {
            return Ok(await _unitOfWork.SectionRepository.GetSectionByIdAsync(id, storeId));
        }
        public async Task<IHttpActionResult> GetProductCategoryGroups(int storeId, bool forceFull, int deviceId)
        {
            var data = new object();
            if (forceFull)
            {
                data = await _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroupsAsync(storeId);


                return Ok(Mapper.Map<ProductCategoryGroupViewModel[]>(data));

            }
            else
            {

                var lastSync =
                    await _unitOfWork.IncrementalSyncronizationRepository.GetLastIncrementalSyncronization(storeId,
                        deviceId, "ProductCategoryGroups");
                if (lastSync == null)
                {
                    data = await _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroupsAsync(storeId);
                }
                else
                {
                    data = await _unitOfWork.ProductCategoryGroupRepository.GetAllProductCategoryGroupsAsyncIncremental(storeId,
                        lastSync.LastSynced);
                }
                _unitOfWork.IncrementalSyncronizationRepository.AddIncrementalSyncronization(new IncrementalSyncronization()
                {
                    StoreId = storeId,
                    DeviceId = deviceId,
                    LastSynced = DateTime.Now,
                    TableName = "ProductCategoryGroups"

                });
                _unitOfWork.Complete();
                return Ok(Mapper.Map<ProductCategoryGroupViewModel[]>(data));


            }


        }
        //public async Task<IHttpActionResult> GetProductCategoryGroups(int storeId)
        //{
        //    return Ok(Mapper.Map<ProductCategoryGroupViewModel[]>(await _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroupsAsync(storeId))); 
        //}

        // GET: api/ProductCategoryGroupCategoriesSync/5
        public async Task<IHttpActionResult> GetProductCategoryGroup(int id, int storeId)
        {
            return Ok(await _unitOfWork.ProductCategoryGroupRepository.GetProductCategoryGroupAsync(id, storeId));
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
                    await _unitOfWork.ProductCategoryGroupRepository.AddProductCategoryGroupAsync(productCategoryGroup);
                }
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new Exception("Error Occured While Adding");
                }
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
