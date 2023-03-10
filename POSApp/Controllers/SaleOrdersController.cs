using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using System.Linq.Dynamic;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;
using POSApp.Services;

namespace POSApp.Controllers
{
    [Authorize]
    public class SaleOrdersController : LanguageController
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;

        public SaleOrdersController()
        {

        }

        public SaleOrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: SaleOrders
        public ActionResult SaleOrderList()
        {
            
            return View();
        }
        public ActionResult DailySales()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetSaleOrdersData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                //Find Order Column
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchColumn = Request.Form.GetValues("search[value]").FirstOrDefault();
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                int recordsFiltered = 0;
                var v=_unitOfWork.TransMasterRepository.GetTransMastersQuery((int) UserStores.GetStoreCookie(System.Web.HttpContext.Current));
                v = v.Where(a => a.Type == "INV");
                recordsTotal = v.Count();
                if (!(string.IsNullOrWhiteSpace(searchColumn)))
                {

                    v = v.Where(a => a.TransCode.ToLower().Contains(searchColumn) || a.TransDate.Contains(searchColumn) || a.TransTime.Contains(searchColumn) ||  a.BusinessPartnerName.ToLower().Contains(searchColumn) || a.TransStatus.ToLower().Contains(searchColumn) );
                }
                //SORT
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    v = v.OrderBy(sortColumn + " " + sortColumnDir);
                }
                recordsFiltered = v.Count();


                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public ActionResult RefundList()
        {

            return View();
        }
        public ActionResult DailyFunds()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetDailyRefundData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                //Find Order Column
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchColumn = Request.Form.GetValues("search[value]").FirstOrDefault();
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                int recordsFiltered = 0;
                var v = _unitOfWork.TransMasterRepository.GetDailyRefundsQuery((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
                v = v.Where(a => a.Type == "REF");
                recordsTotal = v.Count();
                if (!(string.IsNullOrWhiteSpace(searchColumn)))
                {

                    v = v.Where(a => a.TransCode.ToLower().Contains(searchColumn) || a.TransDate.Contains(searchColumn) || a.TransTime.Contains(searchColumn) || a.BusinessPartnerName.ToLower().Contains(searchColumn) || a.TransStatus.ToLower().Contains(searchColumn));
                }
                //SORT
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    v = v.OrderBy(sortColumn + " " + sortColumnDir);
                }
                recordsFiltered = v.Count();


                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpPost]
        public ActionResult GetRefundsData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                //Find Order Column
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchColumn = Request.Form.GetValues("search[value]").FirstOrDefault();
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                int recordsFiltered = 0;
                var v = _unitOfWork.TransMasterRepository.GetTransMastersQuery((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
                v = v.Where(a => a.Type == "REF");
                recordsTotal = v.Count();
                if (!(string.IsNullOrWhiteSpace(searchColumn)))
                {

                    v = v.Where(a => a.TransCode.ToLower().Contains(searchColumn) || a.TransDate.Contains(searchColumn) || a.TransTime.Contains(searchColumn) || a.BusinessPartnerName.ToLower().Contains(searchColumn) || a.TransStatus.ToLower().Contains(searchColumn));
                }
                //SORT
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    v = v.OrderBy(sortColumn + " " + sortColumnDir);
                }
                recordsFiltered = v.Count();


                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public ActionResult MIFDataDetailList(int miforderId)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var data = _unitOfWork.TransMasterRepository.GetTransMaster(miforderId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            return View(data);
        }
        public ActionResult MIFDataList()
        {

            return View();
        }
        [HttpPost]
        public ActionResult MIFDataListData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                //Find Order Column
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchColumn = Request.Form.GetValues("search[value]").FirstOrDefault();
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                int recordsFiltered = 0;
                var v = _unitOfWork.TransMasterRepository.GetTransMastersQuery((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
                v = v.Where(a => a.Type == "MIF");
                recordsTotal = v.Count();
                if (!(string.IsNullOrWhiteSpace(searchColumn)))
                {

                    v = v.Where(a => a.TransCode.ToLower().Contains(searchColumn) || a.TransDate.Contains(searchColumn) || a.TransTime.Contains(searchColumn) || a.BusinessPartnerName.ToLower().Contains(searchColumn) || a.TransStatus.ToLower().Contains(searchColumn));
                }
                //SORT
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    v = v.OrderBy(sortColumn + " " + sortColumnDir);
                }
                recordsFiltered = v.Count();


                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ActionResult MIFData()
        {

            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            try
            {
                var saleorders = _unitOfWork.TransMasterRepository.GetSaleInvoices((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
                List<int> transIds = new List<int>();
                for (int i = 0; i < saleorders.Count(); i++)
                {
                    transIds.Add(_unitOfWork.AppCountersRepository.GetId("MIF"));
                    _unitOfWork.Complete();
                }

                int index = 0;
                foreach (var saleorder in saleorders)
                {
                    TransMaster mif = new TransMaster();
                    mif.Type = "MIF";
                    mif.TransCode = "MIF-" + transIds[index].ToString() + "-" + UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                    index++;
                    mif.BusinessPartnerId = saleorder.BusinessPartnerId;
                    mif.TransDate = DateTime.Now;
                    mif.StoreId = (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current);
                    foreach (var item in saleorder.TransDetails)
                    {


                        List<Recipe> recipes = _unitOfWork.RecipeRepository.GetAllRecipes(item.StoreId, item.ProductCode
                           ).ToList();


                        foreach (var ing in recipes)
                        {
                            var mifdetail = new TransDetail();
                            if (!mif.TransDetails.Where(a => a.ProductCode == ing.IngredientCode).Any())
                            {
                                mifdetail = new TransDetail();
                                mifdetail.ProductCode = ing.IngredientCode;
                                mifdetail.StoreId = ing.StoreId;
                                var q = ing.Ingredient.PtoSFactor * ing.Ingredient.StoIFactor;
                                var p = q - ing.Quantity;
                                var t = q-p;
                                var quantity = Convert.ToDecimal(t) / (Convert.ToDecimal(ing.Ingredient.StoIFactor) * Convert.ToDecimal(ing.Ingredient.PtoSFactor));
                                
                                
                                mifdetail.Quantity = Convert.ToDecimal((quantity)*(q)) * item.Quantity;
                                mifdetail.UnitPrice = _unitOfWork.TransMasterRepository.AvgPrice(ing.IngredientCode, ing.StoreId, saleorder.TransDate);
                                mif.TransDetails.Add(mifdetail);
                            }
                            else
                            {
                                mifdetail = mif.TransDetails.FirstOrDefault(a => a.ProductCode == ing.IngredientCode);
                                var quantity = (ing.Quantity / Convert.ToDecimal(ing.Ingredient.StoIFactor)) / Convert.ToDecimal(ing.Ingredient.PtoSFactor);
                                mifdetail.Quantity += quantity * item.Quantity;
                            }
                        }
                    }
                    _unitOfWork.TransMasterRepository.AddTransMaster((mif));
                    saleorder.Issued = true;

                }
                _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("MIFDataList", "SaleOrders");
        }

        [HttpPost]
        public ActionResult GetDailySaleOrdersData()
        {
            try
            {
                var draw = Request.Form.GetValues("draw").FirstOrDefault();
                var start = Request.Form.GetValues("start").FirstOrDefault();
                var length = Request.Form.GetValues("length").FirstOrDefault();
                //Find Order Column
                var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                var searchColumn = Request.Form.GetValues("search[value]").FirstOrDefault();
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                int recordsFiltered = 0;
                var v = _unitOfWork.TransMasterRepository.GetDailyTransMastersQuery((int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
                v = v.Where(a => a.Type == "INV");
                recordsTotal = v.Count();
                if (!(string.IsNullOrWhiteSpace(searchColumn)))
                {

                    v = v.Where(a => a.TransCode.ToLower().Contains(searchColumn) || a.TransDate.Contains(searchColumn) || a.TransTime.Contains(searchColumn) || a.BusinessPartnerName.ToLower().Contains(searchColumn) || a.TransStatus.ToLower().Contains(searchColumn));
                }
                //SORT
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    v = v.OrderBy(sortColumn + " " + sortColumnDir);
                }
                recordsFiltered = v.Count();


                var data = v.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsFiltered, recordsTotal = recordsTotal, data = data }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        //public ActionResult DeleteSaleOrder(int id)
        //{
        //    _unitOfWork.SaleOrderRepository.DeleteSaleOrder(id);
        //    _unitOfWork.Complete();
        //    return RedirectToAction("SaleOrderList","SaleOrders");
        //}

        public ActionResult SaleOrderDetailList(int saleOrderId)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var data = _unitOfWork.TransMasterRepository.GetTransMaster(saleOrderId, (int) UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            return View(data);
        }
        public ActionResult RefundDetailList(int saleOrderId)
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var data = _unitOfWork.TransMasterRepository.GetTransMaster(saleOrderId, (int)UserStores.GetStoreCookie(System.Web.HttpContext.Current));
            return View(data);
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