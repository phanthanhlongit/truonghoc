using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidsSchool.Controllers
{
    public class BannerAdsController : Controller
    {
        public ActionResult _HomeBanner()
        {
            return PartialView();
        }
    }
}
