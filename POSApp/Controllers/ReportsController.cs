using System;
using System.Linq;
using System.Web.Mvc;
using POSApp.Core;

namespace POSApp.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public ReportsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Reports
        public ActionResult SaleReport()
        {
            return View();
        }

        public ActionResult InventoryReport()
        {
            return View();
        }

        public ActionResult MiscellaneousReport()
        {
            return View();
        }
        public ActionResult ExportingReport()
        {
            return View();
        }
        public ActionResult MyReports()
        {
            return View();
        }

        public ActionResult GenerateReport(string target)
        {
            ViewBag.edit = target;
            ViewBag.branches = _unitOfWork.StoreRepository.GetStores().Select(a => new SelectListItem{Text = a.Name,Value = a.Id.ToString()});
            return View();
        }
        [HttpPost]
        public ActionResult GenerateProductSaleReport(DateTime dateFrom,DateTime dateTo,int branchId)
        {
            return View();
        }
    }
}