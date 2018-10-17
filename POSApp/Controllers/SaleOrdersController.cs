using System.Web.Mvc;
using POSApp.Core;

namespace POSApp.Controllers
{
    public class SaleOrdersController : Controller
    {
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
            return View(_unitOfWork.SaleOrderRepository.GetSaleOrders());
        }

        
        //public ActionResult DeleteSaleOrder(int id)
        //{
        //    _unitOfWork.SaleOrderRepository.DeleteSaleOrder(id);
        //    _unitOfWork.Complete();
        //    return RedirectToAction("SaleOrderList","SaleOrders");
        //}

        public ActionResult SaleOrderDetailList(int saleOrderId)
        {
            return View(_unitOfWork.SaleOrderDetailRepository.GetSaleOrderDetails(saleOrderId));
        }
      
    }
}