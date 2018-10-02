using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PoSCloudApp.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult ServicesList()
        {
            return View();
        }
    }
}