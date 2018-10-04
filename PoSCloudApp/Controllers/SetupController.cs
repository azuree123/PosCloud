using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoSCloudApp.Core;

namespace PoSCloudApp.Controllers
{
    public class SetupController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public SetupController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Setup
        public ActionResult DepartmentList()
        {
            return View();
        }
        public ActionResult DepartmentAdd()
        {
            return View();
        }
        public ActionResult DepartmentUpdate()
        {
            return View();
        }
        public ActionResult DepartmentDelete()
        {
            return View();
        }
    }
}