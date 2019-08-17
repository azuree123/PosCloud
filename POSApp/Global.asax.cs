using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using POSApp.Services;

namespace POSApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //protected void Application_BeginRequest(Object sender, EventArgs e)
        //{
        //    HttpRuntimeSection runTime = (HttpRuntimeSection)WebConfigurationManager.GetSection("system.web/httpRuntime");
        //    //Approx 100 Kb(for page content) size has been deducted because the maxRequestLength property is the page size, not only the file upload size
        //    int maxRequestLength = (runTime.MaxRequestLength - 100) * 1024;

        //    //This code is used to check the request length of the page and if the request length is greater than 
        //    //MaxRequestLength then retrun to the same page with extra query string value action=exception

        //    HttpContext context = ((HttpApplication)sender).Context;
        //    if (context.Request.ContentLength > maxRequestLength)
        //    {
        //        IServiceProvider provider = (IServiceProvider)context;
        //        HttpWorkerRequest workerRequest = (HttpWorkerRequest)provider.GetService(typeof(HttpWorkerRequest));

        //        // Check if body contains data
        //        if (workerRequest.HasEntityBody())
        //        {
        //            // get the total body length
        //            int requestLength = workerRequest.GetTotalEntityBodyLength();
        //            // Get the initial bytes loaded
        //            int initialBytes = 0;
        //            if (workerRequest.GetPreloadedEntityBody() != null)
        //                initialBytes = workerRequest.GetPreloadedEntityBody().Length;
        //            if (!workerRequest.IsEntireEntityBodyIsPreloaded())
        //            {
        //                byte[] buffer = new byte[512000];
        //                // Set the received bytes to initial bytes before start reading
        //                int receivedBytes = initialBytes;
        //                while (requestLength - receivedBytes >= initialBytes)
        //                {
        //                    // Read another set of bytes
        //                    initialBytes = workerRequest.ReadEntityBody(buffer, buffer.Length);

        //                    // Update the received bytes
        //                    receivedBytes += initialBytes;
        //                }
        //                initialBytes = workerRequest.ReadEntityBody(buffer, requestLength - receivedBytes);
        //            }
        //        }
        //        // Redirect the user to the same page with querystring action=exception. 
        //        context.Response.Redirect(this.Request.Url.LocalPath + "?action=exception");
        //    }
        //}
       

        protected void Application_Start()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
          


            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings
                .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters
                .Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
           
        }

      




        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
        //    if (cookie != null && cookie.Value != null)
        //    {
        //        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie.Value);
        //        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie.Value);
        //    }
        //    else
        //    {
        //        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
        //        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
        //    }
        //}
    }
}
