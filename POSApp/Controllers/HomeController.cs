using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Core.ViewModels;
using POSApp.Services;

namespace POSApp.Controllers
{
    [Authorize]
    public class HomeController : LanguageController
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public JsonResult ChangeLanguage(string lang)
        {
            new MultiLanguage().SetLanguage(lang);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                DateTime today = DateTime.Now.Date;
                DashBoardViewModel model = new DashBoardViewModel();
                if (_unitOfWork.TransMasterRepository.GetTransMasters((int) user.StoreId)
                    .Where(a => a.Type == "INV" && (a.TransStatus == "Paid" || a.TransStatus == "Complete") && a.TransDate >= today).Any())
                {
                    model.Sales = _unitOfWork.TransMasterRepository.GetTransMasters((int) user.StoreId)
                        .Where(a => a.Type == "INV" && (a.TransStatus == "Paid" || a.TransStatus == "Complete") && a.TransDate >= today).Select(a => a.TotalPrice).Sum();
                }
                if (_unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId)
                    .Where(a => a.Type == "PRI" && a.TransDate >= today).Any())
                {
                    model.PurchaseOrders = _unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId)
                        .Where(a => a.Type == "PRI" && a.TransDate >= today).Select(a => a.TotalPrice).Sum();
                }
                if (_unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId)
                    .Where(a => a.Type == "REF" && a.TransDate >= today).Any())
                {
                    model.Refunds = _unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId)
                        .Where(a => a.Type == "REF" && a.TransDate >= today).Select(a => a.TotalPrice).Sum();
                }
                model.Expenses = _unitOfWork.ExpenseRepository.GetExpenses((int) user.StoreId)
                    .Where(a => a.Date == today).Select(a => a.Amount).Sum();
                model.StoreDatas = _unitOfWork.StoreRepository.GetStores().Select(a => new StoreData
                {
                    StoreId = a.Id, StoreName = a.Name
                }).ToList();
                return View(model);
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
            }

            return View(new DashBoardViewModel());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AllStores()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            

            var userStores = _unitOfWork.ClientRepository.GetUserStore(user.Id);

            
            return View(userStores);
        }
        public JsonResult GetGraphData()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                GraphViewModel graph=new GraphViewModel();
                graph.Morris=new List<MorrisGraphViewModel>();
                graph.Line=new List<LineGraphViewModel>();
                int year = DateTime.Now.Date.Year;
                int month = DateTime.Now.Date.Month;
                for (int i = 0; i < 6; i++)
                {
                    MorrisGraphViewModel morrisGraph=new MorrisGraphViewModel();
                    morrisGraph.y = (year - i).ToString();
                    DateTime dateFrom=new DateTime(year-i,1,1);
                    DateTime dateTo = dateFrom.AddYears(1);
                    morrisGraph.a = _unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId)
                        .Where(a => a.Type == "INV" && (a.TransStatus == "Paid" || a.TransStatus == "Complete") && a.TransDate >= dateFrom && a.TransDate < dateTo).Select(a => a.TotalPrice - a.Discount).Sum();
                    morrisGraph.b = _unitOfWork.ExpenseRepository.GetExpenses((int) user.StoreId)
                        .Where(a => a.Date >= dateFrom && a.Date < dateTo).Select(a => a.Amount).Sum();
                    graph.Morris.Add(morrisGraph);
                }

                LineGraphViewModel data = new LineGraphViewModel();
                data.data=new List<LineData>();
                for (int i = 1; i <= 12; i++)
                {
                    DateTime dateFrom =  new DateTime(year, i, 1);
                    string x = dateFrom.Date.ToString("MMM");
                    DateTime dateTo = dateFrom.AddMonths(1);
                    decimal y = _unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId)
                        .Where(a => a.Type == "INV" && (a.TransStatus == "Paid" || a.TransStatus == "Complete") && a.TransDate >= dateFrom && a.TransDate < dateTo).Select(a => a.TotalPrice).Sum();
                    data.data.Add(new LineData{Month = x,Value = y});
                }
                graph.Line.Add(data);

                 data = new LineGraphViewModel();
                data.data = new List<LineData>();
                 month = DateTime.Now.Date.Month;
                for (int i = 1; i <= 12; i++)
                {
                    DateTime dateFrom = new DateTime(year, i, 1);
                    string x = dateFrom.Date.ToString("MMM");
                    DateTime dateTo = dateFrom.AddMonths(1);
                    decimal y = _unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId)
                        .Where(a => a.Type == "PRI" && a.TransDate >= dateFrom && a.TransDate < dateTo).Select(a => a.TotalPrice).Sum();
                    data.data.Add(new LineData { Month = x, Value = y });
                }
                graph.Line.Add(data);

                data = new LineGraphViewModel();
                data.data = new List<LineData>();
                 month = DateTime.Now.Date.Month;
                for (int i = 1; i <= 12; i++)
                {
                    DateTime dateFrom = new DateTime(year, i, 1);
                    string x = dateFrom.Date.ToString("MMM");
                    DateTime dateTo = dateFrom.AddMonths(1);
                    decimal y = (decimal)_unitOfWork.ExpenseRepository.GetExpenses((int)user.StoreId)
                        .Where(a =>  a.Date >= dateFrom && a.Date < dateTo).Select(a => a.Amount).Sum();
                    data.data.Add(new LineData { Month = x, Value = y });
                }
                graph.Line.Add(data);

                return Json(graph, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ActionResult Error(AuthorizationContext filterContext)
        {
            return View();
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