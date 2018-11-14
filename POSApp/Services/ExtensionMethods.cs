using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace POSApp.Services
{
    public static class ExtensionMethod
    {
        //public static bool UserHasAccess(this Config.Permission permission)
        //{
        //    string data = new AuthHelper().UserData(HttpContext.Current.Request.RequestContext.HttpContext);
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
        //public static bool InventoryAccess(this Config.Inventory permission)
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
        //public static bool SalesAccess(this Config.Sales permission)
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