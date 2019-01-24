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
        public async Task<IHttpActionResult> GetProducts(int storeId)
        {
            return Ok(Mapper.Map<ProductSyncViewModel[]>(await _unitOfWork.ProductRepository.GetAllProductsAsync(storeId)));
        }

        // GET: api/ProductsSync/5
        public async Task<IHttpActionResult> GetProduct(int id, int storeId)
        {
            return Ok(await _unitOfWork.ProductRepository.GetProductByIdAsync(id,storeId));
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
                    var catInfo =
                         _unitOfWork.ProductCategoryRepository.GetProductCategoryByCode(product.CategoryId.ToString(),
                            product.StoreId);
                    var sectionInfo =
                        _unitOfWork.SectionRepository.GetSectionByCode(product.SectionId.ToString(),
                            product.StoreId);
                    var taxInfo =
                        _unitOfWork.TaxRepository.GetTaxByCode(product.TaxId.ToString(),
                            product.StoreId);
                    if (taxInfo != null)
                    {
                        product.TaxId = taxInfo.Id;
                    }
                    else
                    {
                        product.TaxId = null;
                    }
                    if (sectionInfo != null)
                    {
                        product.SectionId = sectionInfo.SectionId;
                    }
                    else
                    {
                        product.SectionId = null;
                    }
                    product.Synced = true;
                    product.SyncedOn = DateTime.Now;
                    if (catInfo != null)
                    {

                    product.CategoryId = catInfo.Id;
                    await _unitOfWork.ProductRepository.AddProductAsync(product);
                    }
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