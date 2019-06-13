using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using POSApp.Models;
using POSApp.SecurityFilters;

namespace POSApp.Services
{
    public static class ExtensionMethod
    {
        
        public static bool CheckAccess(this object permission)
        {
            UserRoleDataViewModel user = new AuthHelper().UserAccessData();
            string data = user!=null?user.ViewData:"";
            string data1 = user != null ? user.ManageData : "";
            if (data == null) return false;
            string[] str = new[] { "" };
            if (string.IsNullOrEmpty(data))
            {

            }
            else
            {
                str = data.Split(',');
            }

            foreach (var s in str)
            {
                if (s.Trim() == permission.ToString())
                {
                    return true;
                }
                else { }
            }
            if (data1 == null) return false;
            string[] str1 = new[] { "" };
            if (string.IsNullOrEmpty(data1))
            {

            }
            else
            {
                str1 = data1.Split(',');
            }

            foreach (var s in str1)
            {
                if (s.Trim() == permission.ToString())
                {
                    return true;
                }
                else { }
            }

            return false;
        }



        public static bool ManageAccess(this object permission)
        {
            UserRoleDataViewModel user = new AuthHelper().UserAccessData();
            string data1 = user!=null?user.ManageData:"";
            string[] str = new[] { "" };




            if (data1 == null) return false;
            string[] str1 = new[] { "" };
            if (string.IsNullOrEmpty(data1))
            {

            }
            else
            {
                str1 = data1.Split(',');
            }

            foreach (var s in str1)
            {
                if (s.Trim() == permission.ToString())
                {
                    return true;
                }
                else { }
            }

            return false;
        }


        public static bool ViewAccess(this object permission)
        {
            UserRoleDataViewModel user = new AuthHelper().UserAccessData();
            string data = user!=null?user.ViewData:"";
            if (data == null) return false;
            string[] str = new[] { "" };
            if (string.IsNullOrEmpty(data))
            {

            }
            else
            {
                str = data.Split(',');
            }

            foreach (var s in str)
            {
                if (s.Trim() == permission.ToString())
                {
                    return true;
                }

            }



            return false;
        }





        //public static bool PurchasesAccess(this Config.Purchases permission)
        //{
        //    string data = new AuthHelper().UserFunctionalitiesData(HttpContext.Current.Request.RequestContext.HttpContext);
        //    if (data == null) return false;
        //    string[] str = new[] { "" };
        //    if (string.IsNullOrEmpty(data))
        //    {

        //    }
        //    else
        //    {
        //        str = data.Split(',');
        //    }

        //    foreach (var s in str)
        //    {
        //        if (s == permission.ToString())
        //        {
        //            return true;
        //        }
        //        else { }
        //    }

        //    return false;
        //}
        //public static bool ReportsAccess(this Config.Reports permission)
        //{
        //    string data = new UserRoleDataViewModel().ManageData;
        //    if (data == null) return false;
        //    string[] str = new[] { "" };
        //    if (string.IsNullOrEmpty(data))
        //    {

        //    }
        //    else
        //    {
        //        str = data.Split(',');
        //    }

        //    foreach (var s in str)
        //    {
        //        if (s == permission.ToString())
        //        {
        //            return true;
        //        }
        //        else { }
        //    }

        //    return false;
        //}
        //public static bool CMSAccess(this Config.CMS permission)
        //{
        //    string data = new AuthHelper().UserFunctionalitiesData(HttpContext.Current.Request.RequestContext.HttpContext);
        //    if (data == null) return false;
        //    string[] str = new[] { "" };
        //    if (string.IsNullOrEmpty(data))
        //    {

        //    }
        //    else
        //    {
        //        str = data.Split(',');
        //    }

        //    foreach (var s in str)
        //    {
        //        if (s == permission.ToString())
        //        {
        //            return true;
        //        }
        //        else { }
        //    }

        //    return false;
        //}
        //public static bool PayrollAccess(this Config.Payroll permission)
        //{
        //    string data = new AuthHelper().UserFunctionalitiesData(HttpContext.Current.Request.RequestContext.HttpContext);
        //    if (data == null) return false;
        //    string[] str = new[] { "" };
        //    if (string.IsNullOrEmpty(data))
        //    {

        //    }
        //    else
        //    {
        //        str = data.Split(',');
        //    }

        //    foreach (var s in str)
        //    {
        //        if (s == permission.ToString())
        //        {
        //            return true;
        //        }
        //        else { }
        //    }

        //    return false;
        //}
        //public static bool AccountsAccess(this Config.Accounts permission)
        //{
        //    string data = new AuthHelper().UserFunctionalitiesData(HttpContext.Current.Request.RequestContext.HttpContext);
        //    if (data == null) return false;
        //    string[] str = new[] { "" };
        //    if (string.IsNullOrEmpty(data))
        //    {

        //    }
        //    else
        //    {
        //        str = data.Split(',');
        //    }

        //    foreach (var s in str)
        //    {
        //        if (s == permission.ToString())
        //        {
        //            return true;
        //        }
        //        else { }
        //    }

        //    return false;
        //}
        //public static bool ReportsAccess(this Config.Reports permission)
        //{
        //    string data = new AuthHelper().UserFunctionalitiesData(HttpContext.Current.Request.RequestContext.HttpContext);
        //    if (data == null) return false;
        //    string[] str = new[] { "" };
        //    if (string.IsNullOrEmpty(data))
        //    {

        //    }
        //    else
        //    {
        //        str = data.Split(',');
        //    }

        //    foreach (var s in str)
        //    {
        //        if (s == permission.ToString())
        //        {
        //            return true;
        //        }
        //        else { }
        //    }

        //    return false;
        //}
        //public static bool SetupAccess(this Config.Setup permission)
        //{
        //    string data = new AuthHelper().UserFunctionalitiesData(HttpContext.Current.Request.RequestContext.HttpContext);
        //    if (data == null) return false;
        //    string[] str = new[] { "" };
        //    if (string.IsNullOrEmpty(data))
        //    {

        //    }
        //    else
        //    {
        //        str = data.Split(',');
        //    }

        //    foreach (var s in str)
        //    {
        //        if (s == permission.ToString())
        //        {
        //            return true;
        //        }
        //        else { }
        //    }

        //    return false;
        //}
        //public static bool SecurityAccess(this Config.Security permission)
        //{
        //    string data = new AuthHelper().UserFunctionalitiesData(HttpContext.Current.Request.RequestContext.HttpContext);
        //    if (data == null) return false;
        //    string[] str = new[] { "" };
        //    if (string.IsNullOrEmpty(data))
        //    {

        //    }
        //    else
        //    {
        //        str = data.Split(',');
        //    }

        //    foreach (var s in str)
        //    {
        //        if (s == permission.ToString())
        //        {
        //            return true;
        //        }
        //        else { }
        //    }

        //    return false;
        //}
    }
}