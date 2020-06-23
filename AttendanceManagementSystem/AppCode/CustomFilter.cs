using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Routing;

namespace AttendanceManagementSystem.AppCode
{
    public class CustomFilterAttribute : AuthorizeAttribute
    {
        private string[] allowedroles;
        public CustomFilterAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var userId = Convert.ToString(httpContext.Session["usercode"]);
            if (!string.IsNullOrEmpty(userId))
                                   
                    foreach (var role in allowedroles)
                    {
                        if (role == httpContext.Session["usertype"].ToString()) return true;
                    }
                 return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Login" },
                    { "action", "Login" }
               });
        }
    }
}