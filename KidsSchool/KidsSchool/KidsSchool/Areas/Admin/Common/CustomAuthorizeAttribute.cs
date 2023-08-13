using KidsSchool.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KidsSchool.Areas.Admin.Common
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            var userMail = HttpContext.Current.User.Identity.Name;
            if (!string.IsNullOrEmpty(userMail))
                using (var context = new Entities())
                {
                    var userRole = context.AspNetRoles.Where(x=>x.AspNetUsers.Select(y=>y.Email).Contains(userMail)).FirstOrDefault();

                    foreach (var role in allowedroles)
                    {
                        if (role == userRole.Name) return true;
                    }
                }


            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Home" },
                    { "UnAuthorized", "Authorized" }
               });
        }
    }
}