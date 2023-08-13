using KidsSchool.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KidsSchool.Areas.Admin.Controllers
{
    public class Noti_UserController : Controller
    {
        private Entities db = new Entities();

        public ActionResult UpdateNotifyAjax(string token, string websitreid)
        {
            var info = new
            {
                success = false,
                msg = ""
            };
            try
            {
                if (token != null)
                {
                    var checkorder = db.Noti_User.FirstOrDefault(x => x.Token == token);
                    if (checkorder == null)
                    {
                        var notify = new Noti_User();
                        notify.Token = token;
                        db.Noti_User.Add(notify);
                        db.SaveChanges();
                    }

                    info = new
                    {
                        success = true,
                        msg = ""
                    };
                }
            }
            catch (Exception ex)
            {
                info = new
                {
                    success = false,
                    msg = ex.Message
                };
            }
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
