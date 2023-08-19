using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KidsSchool.Controllers
{
    public class PagesController : BaseController
    {
        // GET: Pages
        //slug.html
        //[OutputCache(VaryByParam = "Slug", Duration = 3600)]
        public ActionResult Index(string Slug)
        {
            var page = db.Pages.SingleOrDefault(p => p.slug == Slug);
            if (page != null)
            {
                return View(page);
            }
            return Redirect("/404.html");
        }



        //[OutputCache(Duration = 86400)]
        public ActionResult SpecialPage(string FriendlyUrl)
        {
            #region mobile
            if (Request.Browser.IsMobileDevice)
            {
            }
            #endregion

            #region destop
            if (FriendlyUrl.Contains("phuong-phap-giao-duc"))
            {
                return View("~/Views/Pages/Partial/Educational.cshtml");
            }
            #endregion

            return View();
        }
    }
}
