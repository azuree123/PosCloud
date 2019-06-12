using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using POSApp.Core;
using POSApp.Core.Models;
using POSApp.Models;
using POSApp.Persistence;


namespace POSApp.SecurityFilters
{
    public class View : AuthorizeAttribute
        {
            private ApplicationUserManager _userManager;
            
        public ApplicationUserManager UserManager
            {
                get
                {
                    return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }
                private set
                {
                    _userManager = value;
                }
            }

        // <summary>
        // Validates user for controller/action and check its role for running action
        // </summary>
        //<param name="httpContext">HttpRequest from client</param>
        // <returns>True if users role matches</returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                if (!httpContext.User.Identity.IsAuthenticated)
                    return false;
            UserRoleDataViewModel user = new UserRoleDataViewModel();
                string data = string.Empty;
                //Retrieve user data from cookies
                try
                {
                    var userId = httpContext.User.Identity.GetUserId();
                    user = UserData.GetUserRoleData(httpContext);
                    data = user.ViewData;
                }
                catch (Exception)
                {

                    return false;
                }
                bool check = false;
                //For comparing user role name
                //First extracting from cookies and converting to String for role verfication
                if (string.IsNullOrEmpty(data))
                {
                    return false;
                }
                else { }
                
                string[] role = data.Split(',');
                foreach (var s in role)
                {
                    check = this.Roles.Split(',').Any(definedRole => definedRole.Equals(s.Trim()));
                    if (check == true)
                    {
                        return check;
                    }
                }
                   
                   
                
                return check;
                //If role collection contains current users role than it will allow user to continue            

            }
            // <summary>
            //Default Constructor for Authorising multi roles for an Action or Controller
            //</summary>
            //<param name="roles">List of multiple user roles</param>
            public View(params object[] roles)
            {

               
                if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
                   throw new ArgumentException("roles");
                //Joining comma seprated roles for AuthorizeCore in Roles collection
                this.Roles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
            }
            // <summary>
            // When AutorizeCore rejects the request then it will redirect it to Login page
            // </summary>
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
            //throw new  Exception("User Doesn't have Permission!");
            //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Error" }));
                filterContext.Result = new RedirectResult("~/Home/Error");
            base.HandleUnauthorizedRequest(filterContext);
        }




        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            // Redirect to the login page if necessary
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult(System.Web.Security.FormsAuthentication.LoginUrl + "?returnUrl=" + filterContext.HttpContext.Request.Url);
                return;
            }

            // Redirect to your "access denied" view here
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/Home/Error");
            }
        }
    }
    }
   
