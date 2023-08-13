using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KidsSchool
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "FriendlyRouteRss",
            url: "{FriendlyUrl}.rss",
            namespaces: new[] { "KidsSchool.Controllers" }
            ).RouteHandler = new FriendlyRouteHandler();

            //Product
            routes.MapRoute(name: "ProductCategory", url: "danh-muc-san-pham-{slug}", defaults: new { controller = "Products", action = "ProductionMachining", id = UrlParameter.Optional },namespaces: new[] { "KidsSchool.Controllers" });
            //routes.MapRoute(name: "ProductDetails", url: "san-pham-{slug}.html", defaults: new { controller = "Products", action = "Detail", id = UrlParameter.Optional }, namespaces: new[] { "KidsSchool.Controllers" });
            routes.MapRoute(name: "SearchProduct", url: "tim-kiem-san-pham", defaults: new { controller = "Products", action = "Search", id = UrlParameter.Optional },namespaces: new[] { "KidsSchool.Controllers" } );
            //routes.MapRoute(name: "Checkout", url: "dat-hang", defaults: new { controller = "Order", action = "Checkout", Id = UrlParameter.Optional }, namespaces: new[] { "KidsSchool.Controllers" });

            routes.MapRoute(
               name: "FriendlyRoute",
               url: "{FriendlyUrl}.html",
               namespaces: new[] { "KidsSchool.Controllers" }
               ).RouteHandler = new FriendlyRouteHandler();

            routes.MapRoute(
              name: "FriendlyRouteCate",
              url: "{FriendlyUrl}",
              namespaces: new[] { "KidsSchool.Controllers" }
              ).RouteHandler = new FriendlyRouteHandler();

            //Blog
            routes.MapRoute(name: "Blogs", url: "blog", defaults: new { controller = "Blogs", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "KidsSchool.Controllers" });
            //routes.MapRoute(name: "BlogDetails", url: "{Slug}.html", defaults: new { controller = "Blogs", action = "ShowPost", id = UrlParameter.Optional }, namespaces: new[] { "KidsSchool.Controllers" });
            //routes.MapRoute(name: "BlogCategory", url: "{slug}", defaults: new { controller = "Blogs", action = "Category", id = UrlParameter.Optional }, namespaces: new[] { "KidsSchool.Controllers" });

            //Account
            routes.MapRoute(name: "AccountLogin", url: "Account/Login", defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }, namespaces: new[] { "KidsSchool.Controllers" });
            routes.MapRoute(name: "AccountRegister", url: "Account/Register", defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }, namespaces: new[] { "KidsSchool.Controllers" });
            routes.MapRoute(name: "ForgotPassword", url: "Account/Loss-password", defaults: new { controller = "Account", action = "ForgotPassword", id = UrlParameter.Optional }, namespaces: new[] { "KidsSchool.Controllers" });
            routes.MapRoute(name: "AccountManage", url: "Manage/{action}/{id}", defaults: new { controller = "Manage", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "KidsSchool.Controllers" });

            //Page
            //routes.MapRoute(name: "PageDetails", url: "{Slug}", defaults: new { controller = "Pages", action = "Index", id = UrlParameter.Optional },namespaces: new[] { "KidsSchool.Controllers" });

            routes.MapRoute(name: "Home", url: "", defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "KidsSchool.Controllers" });

            routes.MapRoute(name: "Default", url: "{controller}/{action}/{id}", defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }, namespaces: new[] { "KidsSchool.Controllers" });

            routes.MapRoute( name: "404", url: "{*url}", defaults: new { controller = "Home", action = "NotFound", id = UrlParameter.Optional }, namespaces: new[] { "KidsSchool.Controllers" });
        }
    }
}
