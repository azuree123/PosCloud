using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.Shared;
using POSApp.Core.ViewModels;

namespace POSApp.Controllers
{
    public class OpeningStockController : Controller
    {
        // GET: OpeningStock
        private IUnitOfWork _unitOfWork;
        private ApplicationUserManager _userManager;
        public OpeningStockController()
        {

        }

        public OpeningStockController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: PurchaseOrders
        public ActionResult OpeningStockList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId).OrderByDescending(a => a.Id).Where(a => a.Type == "OPS")));
        }
        
        public ActionResult PreviewOpeningStock(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)user.StoreId));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)user.StoreId);
            foreach (var tempTransDetailViewModel in temp.TransDetailViewModels)
            {
                tempTransDetailViewModel.ProductName = _unitOfWork.ProductRepository
                    .GetProductByCode(tempTransDetailViewModel.ProductCode, tempTransDetailViewModel.StoreId).Name;
            }
          
            temp.TotalAmount = (from a in temp.TransDetailViewModels
                                select a.Quantity * a.UnitPrice).Sum();
            TempData["po"] = temp;
            return RedirectToAction("GenerateOpeningStockReceipt", "OpeningStock");

        }

        

        public ActionResult AddOpeningStock()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "OPS";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)user.StoreId).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            
            if (OpeningHelper.temptTransDetail != null)
            {

                OpeningHelper.EmptyTemptTransDetail(user.Id, (int)user.StoreId);
            }
            return View(po);
        }
        [HttpPost]
        public ActionResult AddOpeningStock(TransMasterViewModel po)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            po.Type = "OPS";
            if (!ModelState.IsValid)
            {
                
                
                return View(po);
            }
            else
            {

                int TransId = _unitOfWork.AppCountersRepository.GetId("OpeningStock");
                po.TransCode = "OPS-" + "C-" + TransId.ToString() + "-" + user.StoreId;
                po.StoreId = user.StoreId;

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = OpeningHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == user.StoreId);

                savePo.TotalPrice = (from a in poItems
                                     select a.Quantity * a.UnitPrice).Sum();
                _unitOfWork.TransMasterRepository.AddTransMaster(savePo);
                _unitOfWork.Complete();

                foreach (var transDetailViewModel in poItems)
                {
                    transDetailViewModel.TransMasterId = savePo.Id;
                    _unitOfWork.TransDetailRepository.AddTransDetail(Mapper.Map<TransDetail>(transDetailViewModel));
                }
                _unitOfWork.Complete();
                temp.TransMasterViewModel = po;
                temp.TransMasterViewModel.TransDate = Convert.ToDateTime(savePo.TransDate).ToString("dd-MMM-yyyy");
                temp.TransMasterViewModel.TransTime = Convert.ToDateTime(savePo.TransDate).ToShortTimeString();
               
                
                temp.TransDetailViewModels = poItems;
                temp.TotalAmount = (from a in temp.TransDetailViewModels
                                    select a.Quantity * a.UnitPrice).Sum();
                TempData["po"] = temp;
            }
            return RedirectToAction("GenerateOpeningStockReceipt", "OpeningStock");


        }

        public ActionResult AddTransactionItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)user.StoreId).Where(a => (a.PurchaseItem || a.InventoryItem)).OrderBy(a => a.Name).Select(a => new SelectListItem { Text = a.Name + "(" + a.ProductCode + ")", Value = a.Id.ToString() }));
        }
        [HttpPost]
        public ActionResult AddTransactionItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (OpeningHelper.temptTransDetail == null)
            {
                OpeningHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)user.StoreId);
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            OpeningHelper.AddToTemptTransDetail(product, quantity, cost, user.Id);
            return View("PoTable", OpeningHelper.temptTransDetail);
        }
        public ActionResult RemoveTransactionItem(string productId)
        {
            if (OpeningHelper.temptTransDetail == null)
            {
                OpeningHelper.temptTransDetail = new List<TransDetailViewModel>();
                return View("PoTable", OpeningHelper.temptTransDetail);
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            OpeningHelper.RemoveFromTemptTransDetail(productId, (int)user.StoreId, user.Id);
            return View("PoTable", OpeningHelper.temptTransDetail);
        }

        public ActionResult DeleteOpeningStock(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.TransMasterRepository.DeleteTransMaster(id, Convert.ToInt32(user.StoreId));
                _unitOfWork.Complete();
                TempData["Alert"] = new AlertModel("The Opening Stock deleted successfully", AlertType.Success);
                return RedirectToAction("OpeningStockList", "OpeningStock");
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationError in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationError.ValidationErrors)
                    {
                        TempData["Alert"] = new AlertModel(validationError.PropertyName + " Error :" + validationError.ErrorMessage, AlertType.Error);

                    }
                }


            }
            catch (Exception e)
            {
                TempData["Alert"] = new AlertModel("Exception Error", AlertType.Error);
                if (e.InnerException != null)
                    if (!string.IsNullOrWhiteSpace(e.InnerException.Message))
                    {
                        if (e.InnerException.InnerException != null)
                            if (!string.IsNullOrWhiteSpace(e.InnerException.InnerException.Message))
                            {
                                TempData["Alert"] = new AlertModel(e.InnerException.InnerException.Message, AlertType.Error);
                            }
                    }
                    else
                    {

                        TempData["Alert"] = new AlertModel(e.InnerException.Message, AlertType.Error);
                    }
                else
                {
                    TempData["Alert"] = new AlertModel(e.Message, AlertType.Error);
                }
            }

            return RedirectToAction("OpeningStockList");

        }
        public ActionResult GenerateOpeningStockReceipt()
        {
            GeneratePurchaseOrderViewModel po = (GeneratePurchaseOrderViewModel)TempData["po"];
            if (po == null)
            {
                return RedirectToAction("OpeningStockList");
            }
            return View(po);
        }
        public JsonResult GetProductInfo(int id)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                ProductDdlViewModel product =
                    Mapper.Map<ProductDdlViewModel>(
                        _unitOfWork.ProductRepository.GetProductById(id, (int)user.StoreId));
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

    }
}
