using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POSApp.Controllers
{
    [Authorize]
    public class SettingController : LanguageController
    {
        // GET: Setting
        public ActionResult BusinessSetting()
        {
            return View();
        }
    }
}