using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.ViewModels;

namespace POSApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            DateTime today=DateTime.Now.Date;
            DashBoardViewModel model=new DashBoardViewModel();

            model.Orders = _unitOfWork.TransMasterRepository.GetTransMasters((int) user.StoreId)
                .Where(a => a.Type == "INV" && a.TransDate == today).ToList().Count();
            model.Sales = _unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId)
                .Where(a => a.Type == "INV" && a.TransDate == today).Select(a=>a.TotalPrice).Sum();
            model.Expenses = _unitOfWork.ExpenseRepository.GetExpenses((int)user.StoreId)
                .Where(a =>  a.Date == today).Select(a => a.Amount).Sum();
            return View(model);
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

        public JsonResult GetGraphData()
        {
            try
            {
                var userid = User.Identity.GetUserId();
                var user = UserManager.FindById(userid);
                GraphViewModel graph=new GraphViewModel();
                graph.Morris=new List<MorrisGraphViewModel>();
                graph.Line=new LineGraphViewModel();
                int year = DateTime.Now.Date.Year;
                int month = DateTime.Now.Date.Month;
                for (int i = 0; i < 6; i++)
                {
                    MorrisGraphViewModel morrisGraph=new MorrisGraphViewModel();
                    morrisGraph.y = (year - i).ToString();
                    DateTime dateFrom=new DateTime(year-i,1,1);
                    DateTime dateTo = dateFrom.AddYears(1);
                    morrisGraph.a = _unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId)
                        .Where(a => a.Type == "INV" && a.TransDate >= dateFrom && a.TransDate < dateTo).Select(a => a.TotalPrice).Sum();
                    morrisGraph.b = _unitOfWork.ExpenseRepository.GetExpenses((int) user.StoreId)
                        .Where(a => a.Date >= dateFrom && a.Date < dateTo).Select(a => a.Amount).Sum();
                    graph.Morris.Add(morrisGraph);
                }
                graph.Line.data=new List<List<decimal>>();
                List<decimal> list=new List<decimal>();
                for (int i = 0; i < month; i++)
                {
                    DateTime dateFrom = new DateTime(year, (month-i), 1);
                    string x = dateFrom.Date.ToString("MMM");
                    DateTime dateTo = dateFrom.AddMonths(1);
                    decimal y = _unitOfWork.TransMasterRepository.GetTransMasters((int)user.StoreId)
                        .Where(a => a.Type == "INV" && a.TransDate >= dateFrom && a.TransDate < dateTo).Select(a => a.TotalPrice).Sum();
                    list.Add(y);
                }
                    graph.Line.color = "#d2322d";
                
                return Json(graph, JsonRequestBehavior.AllowGet);
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