using KidsSchool.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidsSchool.Controllers
{
    public class MenusController : BaseController
    {

        public ActionResult _MainMenu()
        {
            var menus = db.Menus.ToList();
            if (menus.Count == 0)
            {
                menus = db.Menus.ToList();
            }
             ViewBag.MenuChil = menus.Where(m => m.LocationId == 8 && m.ParentId != null).OrderBy(x => x.OrderBy).ToList();

            return PartialView(menus.Where(m => m.LocationId == 8 && m.ParentId == null).OrderBy(x => x.OrderBy).ToList());
        }

        public ActionResult _MobileMenu()
        {
            var menus = db.Menus.ToList();
            if (menus.Count == 0)
            {
                menus = db.Menus.ToList();
            }
            ViewBag.MenuChil = menus.Where(m => m.LocationId == 8 && m.ParentId != null).OrderBy(x => x.OrderBy).ToList();

            return PartialView(menus.Where(m => m.LocationId == 8 && m.ParentId == null).OrderBy(x => x.OrderBy).ToList());

        }
    }
}
