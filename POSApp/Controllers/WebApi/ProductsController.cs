using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;
using POSApp.Core.ViewModels.Sync;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace POSApp.Controllers.WebApi
{
    public class ProductsController : ApiController
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IHttpActionResult> GetProducts(int storeId, int deviceId, bool forceFull=false, bool imageEnable=false)
        {
            var data=new object();
            try
            {

                if (forceFull)
                {
                    data = await _unitOfWork.ProductRepository.GetAllProductsAsync(storeId);



                    if (!imageEnable)
                    {
                    return Ok(Mapper.Map<ProductSyncViewModel[]>(data));
                    }
                    else
                    {
                        return Ok(Mapper.Map<ProductSyncWithImageViewModel[]>(data));
                    }

                }
                else
                {

                    var lastSync =
                        await _unitOfWork.IncrementalSyncronizationRepository.GetLastIncrementalSyncronization(storeId,
                            deviceId, "Products");
                    if (lastSync == null)
                    {
                        data = await _unitOfWork.ProductRepository.GetAllProductsAsync(storeId);
                    }
                    else
                    {
                        data = await _unitOfWork.ProductRepository.GetAllProductsAsyncIncremental(storeId,
                            lastSync.LastSynced);
                    }

                    _unitOfWork.IncrementalSyncronizationRepository.AddIncrementalSyncronization(
                        new IncrementalSyncronization
                        {
                            StoreId = storeId,
                            DeviceId = deviceId,
                            TableName = "Products",
                            LastSynced = DateTime.Now

                        });
                    _unitOfWork.Complete();
                    if (!imageEnable)
                    {
                        return Ok(Mapper.Map<ProductSyncViewModel[]>(data));
                    }
                    else
                    {
                        return Ok(Mapper.Map<ProductSyncWithImageViewModel[]>(data));
                    }
                }


            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }


        }

        public async Task<IHttpActionResult> GetProductsByLicenec(string licence,int storeId)
        {
            var data = await _unitOfWork.ProductRepository.GetAllProductsAsync(storeId);
            return Ok(Mapper.Map<ProductSyncViewModel[]>(data));
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