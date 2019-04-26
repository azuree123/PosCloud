using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AAPackages.Handlers;
using POSApp.Handlers;


namespace POSApp.Handlers
{
   
        public class Permit : AuthorizeAttribute
        {

            /// <summary>
            /// Validates user for controller/action and check its role for running action
            /// </summary>
            /// <param name="httpContext">HttpRequest from client</param>
            /// <returns>True if users role matches</returns>
            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                if (!AuthHelper.IsAlreadyLoggedIn(httpContext))
                    return false;

                string data = string.Empty;
                //Retrieve user data from cookies
                try
                {
                    data = new AuthHelper().UserData(httpContext);
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
                    check = this.Roles.Split(',').Any(definedRole => definedRole.Equals(s));
                    if (check == true)
                    {
                        return check;
                    }
                }
                   
                   
                
                return check;
                //If role collection contains current users role than it will allow user to continue            

            }
            /// <summary>
            /// Default Constructor for Authorising multi roles for an Action or Controller
            /// </summary>
            /// <param name="roles">List of multiple user roles</param>
            public Permit(params Config.Permission[] roles)
            {
                if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
                    throw new ArgumentException("roles");
                //Joining comma seprated roles for AuthorizeCore in Roles collection
                this.Roles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
            }
            /// <summary>
            /// When AutorizeCore rejects the request then it will redirect it to Login page
            /// </summary>
            protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
            {
                
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new{controller="Home",action="Error"}));
                //base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
