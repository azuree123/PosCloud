using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using System.Linq.Dynamic;

namespace POSApp.Controllers
{
    [Authorize]
    public class SaleOrdersController : Controller
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

        //public ActionResult DailySales(string _Year, string _Month)
        //{

        //    int year = 0;
        //    int month = 0;

        //    if (string.IsNullOrWhiteSpace(_Year) && _Month != null)
        //    {
        //        year = DateTime.Now.Date.Year;
        //        month = Convert.ToInt32(_Month.Trim());
        //    }
        //    else
        //    {
        //        year = Convert.ToInt32(_Year.Trim());
        //        month = Convert.ToInt32(_Month.Trim());
        //    }

        //    int daysInMonth = DateTime.DaysInMonth(year, month);
        //    var days = Enumerable.Range(1, daysInMonth);
        //    var sale = _unitOfWork.TransMasterRepository.GetTransMasters().Where(a => a.)
        //}
        // GET: SaleOrders
        public ActionResult SaleOrderList()
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
                var v=_unitOfWork.TransMasterRepository.GetTransMastersQuery((int) user.StoreId);
                v = v.Where(a => a.Type == "INV");
                recordsTotal = v.Count();
                if (!(string.IsNullOrWhiteSpace(searchColumn)))
                {

                    v = v.Where(a => a.TransCode.Contains(searchColumn) || a.TransDate.Contains(searchColumn) || a.TransTime.Contains(searchColumn) ||  a.BusinessPartnerName.Contains(searchColumn) || a.TransStatus.Contains(searchColumn) );
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
            var data = _unitOfWork.TransMasterRepository.GetTransMaster(saleOrderId, (int) user.StoreId);
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