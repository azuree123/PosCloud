using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Dtos;

namespace POSApp.Controllers
{
    [Authorize]
    public class PointOfSaleController : Controller
    {
        private ApplicationUserManager _userManager;
        private IUnitOfWork _unitOfWork;

        public PointOfSaleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: PointOfSale
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProducts(int id = 0, string group = "")
        {
            var userid = User.Identity.GetUserId();
            var user = UserManager.FindById(userid);
            var products=new List<PosProducts>();
            if (id == 0 && string.IsNullOrEmpty(group))
            {
                products = Mapper.Map<List<PosProducts>>(
                    _unitOfWork.ProductRepository.GetAllProducts(Convert.ToInt32(user.StoreId)));
            }
            else
            {
                if (string.IsNullOrEmpty(group)) { } else { }
                if (id == 0) { }
                else
                {

                }
            }
            return View(products);
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