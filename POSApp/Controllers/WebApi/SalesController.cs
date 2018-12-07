using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels.Sync;

namespace POSApp.Controllers.WebApi
{
    public class SalesController : ApiController
    {
        private IUnitOfWork _unitOfWork;
        public SalesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Sync
        public SalesViewModel Get()
        {
            return null;
        }

        // GET: api/Sync/5
        public SalesViewModel Get(int id)
        {
            return null;
        }

        // POST: api/Sync
        public async Task<IHttpActionResult> AddSaleOrder([FromBody]SyncObject sync)
       {
            try
            {

                List<SalesViewModel> salesView = System.Web.Helpers.Json.Decode<List<SalesViewModel>>(sync.Object) ;
                foreach (var salesViewModel in salesView)
                {
                    TransMaster saleOrder = salesViewModel.TransMaster;
                    int bId = _unitOfWork.BusinessPartnerRepository
                        .GetBusinessPartners("C", saleOrder.StoreId).Select(a=>a.Id).FirstOrDefault();
                    saleOrder.BusinessPartnerId = bId;
                    saleOrder.Code = saleOrder.Id.ToString();
                    var saleOrderAdd = saleOrder;
                    _unitOfWork.TransMasterRepository.AddTransMaster(saleOrder);
                    _unitOfWork.Complete();
                    foreach (var saleOrderDetail in salesViewModel.TransDetailsList)
                    {
                        saleOrderDetail.Code = salesViewModel.TransMaster.Id.ToString();
                        saleOrderDetail.TransMasterId = saleOrderAdd.Id;
                        _unitOfWork.TransDetailRepository.AddTransDetail(saleOrderDetail);
                    }
                    foreach (var method in salesViewModel.TransMasterPaymentMethods)
                    {
                        method.Code = salesViewModel.TransMaster.Id.ToString();
                        method.TransMasterId = saleOrderAdd.Id;
                        method.StoreId = salesViewModel.TransMaster.StoreId;
                        _unitOfWork.TransMasterPaymentMethodRepository.AddTransMasterPaymentMethod(method);
                    }
                }
              
                _unitOfWork.Complete();
                return Ok("Success");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
                throw;
            }
        }

        // PUT: api/Sync/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sync/5
        public void Delete(int id)
        {
        }
    }


}
