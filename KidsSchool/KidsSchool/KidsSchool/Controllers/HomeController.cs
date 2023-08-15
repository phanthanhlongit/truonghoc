using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidsSchool.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }

        //[OutputCache(Duration = 86400)]
        public ActionResult Header()
        {
            return PartialView("_HeaderPartial");
        }

        //[OutputCache(Duration = 86400)]
        public ActionResult Footer()
        {
            return PartialView("_FooterPartial");
        }

        [OutputCache(Duration = 86400)]
        public ActionResult NotFound()
        {
            return PartialView();
        }

        //[OutputCache(Duration = 86400)]
        public ActionResult _SliderShow()
        {
            //var model = db.Sliders.OrderByDescending(s => s.Id).Take(8).ToList();
            return PartialView();
        }
    }
}