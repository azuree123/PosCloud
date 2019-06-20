using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using POSApp.Core.ViewModels;

namespace POSApp.Services
{
    public static class UserStores
    {
        public static string CookieName = "store";
        
        public static void GenerateStoreCookie(int storeId, int cookieExpireDate = 30)
        {
            HttpCookie myCookie = new HttpCookie(CookieName);
            myCookie.Value = storeId.ToString();
            myCookie.Expires = DateTime.Now.AddDays(cookieExpireDate);
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }
        public static int GetStoreCookie()
        {
            HttpCookie myCookie = HttpContext.Current.Response.Cookies[CookieName];
            if (myCookie != null)
            {
                return Convert.ToInt32(myCookie.Value);
            }
            else
            {
                return 0;
            }
        }

    }
}