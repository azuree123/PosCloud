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
using POSApp.SecurityFilters;
using POSApp.Services;

namespace POSApp.Controllers
{
    public class OpeningStockController : LanguageController
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
        [View(Config.OpeningStock.OpeningStock)]

        public ActionResult OpeningStockList()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(Mapper.Map<TransMasterViewModel[]>(_unitOfWork.TransMasterRepository.GetTransMasters((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).OrderByDescending(a => a.Id).Where(a => a.Type == "OPS")));
        }
        [View(Config.OpeningStock.OpeningStock)]

        public ActionResult PreviewOpeningStock(int id)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            GeneratePurchaseOrderViewModel temp = new GeneratePurchaseOrderViewModel();
            temp.TransMasterViewModel = Mapper.Map<TransMasterViewModel>(_unitOfWork.TransMasterRepository.GetTransMaster(id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
            temp.TransDetailViewModels =
                _unitOfWork.TransDetailRepository.GetTransDetails(temp.TransMasterViewModel.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
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


        [Manage(Config.OpeningStock.OpeningStock)]

        public ActionResult AddOpeningStock()
        {
            TransMasterViewModel po = new TransMasterViewModel();
            po.Type = "OPS";
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            po.SupplierDdl = _unitOfWork.BusinessPartnerRepository.GetBusinessPartners("S", (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name });
            
            if (OpeningHelper.temptTransDetail != null)
            {

                OpeningHelper.EmptyTemptTransDetail(user.Id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            }
            return View(po);
        }
        [HttpPost]
        [Manage(Config.OpeningStock.OpeningStock)]

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
                po.TransCode = "OPS-" + "C-" + TransId.ToString() + "-" + UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                po.StoreId = UserStores.GetStoreCookie(System.Web.HttpContext.Current);

                var savePo = Mapper.Map<TransMaster>(po);

                IEnumerable<TransDetailViewModel> poItems = OpeningHelper.temptTransDetail.Where(a => a.CreatedByUserId == userid && a.StoreId == UserStores.GetStoreCookie(System.Web.HttpContext.Current));

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



        [Manage(Config.OpeningStock.TransactionItems)]

        public ActionResult AddTransactionItem()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            return View(_unitOfWork.ProductRepository.GetAllProducts((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)).Where(a => (a.PurchaseItem || a.InventoryItem)).OrderBy(a => a.Name).Select(a => new SelectListItem { Text = a.Name + "(" + a.ProductCode + ")", Value = a.Id.ToString() }));
        }
        [HttpPost]
        [Manage(Config.OpeningStock.TransactionItems)]

        public ActionResult AddTransactionItem(int productId, int purchaseQuantity, int storageQuantity, int ingredientQuantity, decimal cost)
        {
            if (OpeningHelper.temptTransDetail == null)
            {
                OpeningHelper.temptTransDetail = new List<TransDetailViewModel>();
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var product = _unitOfWork.ProductRepository.GetProductById(productId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            ;
            decimal quantity = 0;
            quantity += purchaseQuantity;
            quantity += storageQuantity / Convert.ToDecimal(product.PtoSFactor);
            quantity += (ingredientQuantity / Convert.ToDecimal(product.StoIFactor)) / Convert.ToDecimal(product.PtoSFactor);
            OpeningHelper.AddToTemptTransDetail(product, quantity, cost, user.Id);
            return View("PoTable", OpeningHelper.temptTransDetail);
        }

        [Manage(Config.OpeningStock.TransactionItems)]

        public ActionResult RemoveTransactionItem(string productId)
        {
            if (OpeningHelper.temptTransDetail == null)
            {
                OpeningHelper.temptTransDetail = new List<TransDetailViewModel>();
                return View("PoTable", OpeningHelper.temptTransDetail);
            }
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            OpeningHelper.RemoveFromTemptTransDetail(productId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current), user.Id);
            return View("PoTable", OpeningHelper.temptTransDetail);
        }
        [Manage(Config.OpeningStock.TransactionItems)]

        public ActionResult DeleteOpeningStock(int id, int storeid)
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                _unitOfWork.TransMasterRepository.DeleteTransMaster(id, Convert.ToInt32(UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
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
        [Manage(Config.OpeningStock.OpeningStock)]

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
                        _unitOfWork.ProductRepository.GetProductById(id, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current)));
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
