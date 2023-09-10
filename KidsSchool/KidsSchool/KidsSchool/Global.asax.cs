using KidsSchool.Controllers;
using System;
using System.Net.Http;
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
            if (Response.StatusCode == 401) // Kiểm tra xem có lỗi chưa xác thực không
            {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

                if (authCookie != null && authCookie.Expires < DateTime.Now) // Kiểm tra xem cookie có hết hạn hay không
                {
                    var authController = new BaseController();
                    authController.Logout();
                }
            }
            else if (Response.StatusCode == 403)
            {
                // Xóa tất cả cache trang
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
                Response.Cache.SetExpires(DateTime.MinValue);

                // Xóa cache cookie (nếu có)
                Response.Cookies.Clear();

                var authController = new BaseController();
                authController.Logout();
            }
        }
    }
}
