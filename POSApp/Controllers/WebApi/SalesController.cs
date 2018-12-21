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
            return _unitOfWork.TransMasterRepository.GetInvoiceById(4,3);
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
                    //foreach (var saleOrderDetail in saleOrder.TransDetails)
                    //{
                    //    saleOrderDetail.Code = salesViewModel.TransMaster.Id.ToString();
                    //    saleOrderDetail.TransMasterId = saleOrderAdd.Id;
                    //    saleOrderDetail.StoreId= salesViewModel.TransMaster.StoreId;
                    //    _unitOfWork.TransDetailRepository.AddTransDetail(saleOrderDetail);
                    //    _unitOfWork.Complete();
                    //    foreach (var modifierTransDetail in saleOrderDetail.ModifierTransDetail)
                    //    {
                    //        modifierTransDetail.Code = modifierTransDetail.Id.ToString();
                    //        modifierTransDetail.TransDetailId = saleOrderDetail.Id;
                    //        modifierTransDetail.StoreId = salesViewModel.TransMaster.StoreId;
                    //        _unitOfWork.ModifierTransDetailRepository.AddModifierTransDetail(modifierTransDetail);
                    //    }
                    //}
                    //foreach (var method in saleOrder.TransMasterPaymentMethods)
                    //{
                    //    method.Code = salesViewModel.TransMaster.Id.ToString();
                    //    method.TransMasterId = saleOrderAdd.Id;
                    //    method.StoreId = salesViewModel.TransMaster.StoreId;
                    //    _unitOfWork.TransMasterPaymentMethodRepository.AddTransMasterPaymentMethod(method);
                    //}
                }
              
                _unitOfWork.Complete();
                return Ok(1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(0);
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
