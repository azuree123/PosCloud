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
    public class ProductSubsController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public ProductSubsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/ProductSubs
        public async Task<IHttpActionResult> GetProductSubs(int storeId)
        {
            return Ok(Mapper.Map<ProductSubViewModel[]>(await _unitOfWork.ProductsSubRepository.GetProductsSubsAsync(storeId)));
        }

        // GET: api/ProductSubs/5
        public async Task<IHttpActionResult> GetProductSub(string id, string comboProductId, int storeId)
        {
            return Ok(await _unitOfWork.ProductsSubRepository.GetProductsSubByIdAsync(id, comboProductId, storeId));
        }

        // POST: api/ProductSubs
        public async Task<IHttpActionResult> AddProductSub([FromBody]SyncObject sync)
        {
            try
            {
                List<ProductsSub> productSubs = System.Web.Helpers.Json.Decode<List<ProductsSub>>(sync.Object);
                foreach (var productSub in productSubs)
                {
                    productSub.Synced = true;
                    productSub.SyncedOn = DateTime.Now;
                    await _unitOfWork.ProductsSubRepository.AddProductsSubAsync(productSub);
                }
                if (!await _unitOfWork.CompleteAsync())
                {
                    throw new Exception("Error Occured While Adding");
                }
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/ProductSubs/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProductSubs/5
        public void Delete(int id)
        {
        }
    }
}
