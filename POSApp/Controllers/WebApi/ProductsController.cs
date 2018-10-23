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
    public class ProductsController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetProducts()
        {
            return Ok(Mapper.Map<ProductCreateViewModel[]>(_unitOfWork.ProductRepository.GetApiProducts()));
        }

        // GET: api/ProductsSync/5
        public async Task<IHttpActionResult> GetProduct(int id, int storeId)
        {
            return Ok(_unitOfWork.ProductRepository.GetProductById(id,storeId));
        }

        // POST: api/ProductsSync
        public async Task<IHttpActionResult> AddProducts([FromBody]SyncObject sync)
        {
            try
            {
                List<Product> products = System.Web.Helpers.Json.Decode<List<Product>>(sync.Object);
                foreach (var product in products)
                {
                    product.Code = product.Id.ToString();
                    product.Synced = true;
                    product.SyncedOn = DateTime.Now;
                    _unitOfWork.ProductRepository.AddProduct(product);
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

        // PUT: api/ProductsSync/5
        public void UpdateProduct(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ProductsSync/5
        public void DeleteProduct(int id)
        {
        }
    }
}