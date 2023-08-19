using KidsSchool.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KidsSchool.Models.Dao;

namespace KidsSchool.Controllers
{
    public class MenusController : BaseController
    {

        public ActionResult _MainMenu()
        {
            var menus = DataPuplic.GetInstance().GetMenu(false);
            return PartialView(menus.Where(m => m.LocationId == 8 && m.ParentId == null).OrderBy(x => x.OrderBy).ToList());
        }

        public ActionResult _MobileMenu()
        {
            var menus = DataPuplic.GetInstance().GetMenu(false);
            return PartialView(menus.Where(m => m.LocationId == 8 && m.ParentId == null).OrderBy(x => x.OrderBy).ToList());

        }
    }
}
