using KidsSchool.Models.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KidsSchool.Models.Dao;

namespace KidsSchool.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị viên")]
    public class ConfigsController : AdminController
    {
        // GET: Admin/Configs/Edit/5
        public ActionResult index()
        {
            var config = CacheHelper.GetInstance().GetConfig(false);
            ViewBag.CatId = new SelectList(db.Categories, "Id", "Name");
            if (config == null)
            {
                return HttpNotFound();
            }
            return View(config);
        }

        // POST: Admin/Configs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult index(Config config)
        {
            if (ModelState.IsValid)
            {
                db.Entry(config).State = EntityState.Modified;
                db.SaveChanges();
                CacheHelper.GetInstance().GetConfig(true, db);
                Success("Sửa thành công thông cấu hình web", true);
            }
            ViewBag.CatId = new SelectList(db.Categories, "Id", "Name");
            return View(config);
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
