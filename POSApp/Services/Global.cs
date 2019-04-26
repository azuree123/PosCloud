using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Services
{
    public static class Global
    {
        public static string GetLang()
        {
            string lang = null;
            HttpCookie langCookie = HttpContext.Current.Request.Cookies["culture"];
            if (langCookie != null)
            {
                lang = langCookie.Value;
            }
            else
            {
                var userLanguage = HttpContext.Current.Request.UserLanguages;
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

            return lang;
        }
    }
}