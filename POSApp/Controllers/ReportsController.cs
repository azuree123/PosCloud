using System.Web.Mvc;

namespace POSApp.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
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
    }
}