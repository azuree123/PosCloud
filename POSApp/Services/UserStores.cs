using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using POSApp.Core.ViewModels;
using POSApp.SecurityFilters;

namespace POSApp.Services
{
    public static class UserStores
    {
        public static string CookieName = "store";
        
        public static void GenerateStoreCookie(int storeId,HttpContext context)
        {
            var checkCookie = context.Request.Cookies.AllKeys.Where(a => a==CookieName);
            if (!checkCookie.Any())
            {
                HttpCookie myCookie = new HttpCookie(CookieName);
                myCookie.Value = JsonConvert.SerializeObject(storeId);
                myCookie.Expires = DateTime.Today.AddDays(2);
                context.Response.Cookies.Add(myCookie);
            }
            else
            {
                HttpCookie myCookie = new HttpCookie(CookieName);
                myCookie.Value = storeId.ToString();
                myCookie.Expires = DateTime.Today.AddDays(2);
                context.Response.Cookies.Set(myCookie);
            }
          
        }
        public static int GetStoreCookie(HttpContext context)
        {
            HttpCookie myCookie = context.Request.Cookies["Store"];
            if (myCookie != null)
            {
                return Convert.ToInt32(JsonConvert.DeserializeObject(AuthHelper.Decrypt(myCookie.Value)));
            }
            else
            {
                return 0;
            }
        }

    }
}