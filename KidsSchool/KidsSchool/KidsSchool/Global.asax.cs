using KidsSchool.Controllers;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.Services.Description;

namespace KidsSchool
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_EndRequest(object sender, EventArgs e)
        {
            if (Response.StatusCode == 401) // Ki?m tra xem có l?i chua xác th?c không
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie != null && authCookie.Expires < DateTime.Now) // Ki?m tra xem cookie có h?t h?n hay không
                {
                    var authController = new BaseController();
                    authController.Logout();
                }
            }
            else if (Response.StatusCode == 403)
            {
                var authController = new BaseController();
                authController.Logout();
            }
        }
    }
}
