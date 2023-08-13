using System.Web.Mvc;

namespace KidsSchool.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //context.MapRoute(
            //name: "Products",
            //url: "Admin/{controller}/{action}/{id}.html",
            //defaults: new { controller = "Products", action = "Edit", id = UrlParameter.Optional }
            //);
        }
    }
}