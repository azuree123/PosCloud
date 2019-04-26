using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using POSApp.Services;

namespace POSApp.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string lang = null;
            HttpCookie langCookie = Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                var userLanguage = Request.UserLanguages;
                var userLang = userLanguage != null ? userLanguage[0] : "";
                if (userLang != "")
                {
                    lang = userLang;
                }
                else
                {
                    lang = MultiLanguage.GetDefaultLanguage();
                }
            }
            new MultiLanguage().SetLanguage(lang);
            return base.BeginExecuteCore(callback, state);
        }
    }
}