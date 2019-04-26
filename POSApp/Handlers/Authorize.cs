using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using POSApp.Handlers;


namespace POSApp.Handlers
{
    public class Authorise : AuthorizeAttribute
    {
         protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!AuthHelper.IsAlreadyLoggedIn(httpContext))
                return false;

            //UserLoged data = null;

             return true;
        }
        /// <summary>
        /// Default Constructor for Authorising multi roles for an Action or Controller
        /// </summary>
        /// <param name="roles">List of multiple user roles</param>
        public Authorise()
        {
           
        }
        /// <summary>
        /// When AutorizeCore rejects the request then it will redirect it to Login page
        /// </summary>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary (new { controller = "PayrollRegistration", action = "PayrollLogin" }));
           
        }
    }
}