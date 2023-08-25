using KidsSchool.Controllers;
using KidsSchool.Models.Dao;
using KidsSchool.Models.DB;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace KidsSchool.Areas.Admin.Controllers
{

    [Authorize(Roles = "Quản trị viên,Nhân viên")]
    public class BannersController : AdminController
    {
        // GET: Admin/Banners
        public ActionResult Index()
        {
            var banners = db.Banners.Include(b => b.BannerPosition);
            return View(banners.ToList());
        }

        // GET: Admin/Banners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // GET: Admin/Banners/Create
        public ActionResult Create()
        {
            ViewBag.BannerPositionId = new SelectList(db.BannerPositions, "BannerPositionId", "Name");
            return View();
        }

        // POST: Admin/Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Banner banner)
        {
            banner.Description = "banner";
            banner.EndDate = banner.StartDate = DateTime.Now;
            banner.ItemFor = banner.ItemForId = 1;
            //if (ModelState.IsValid)
            {
                db.Banners.Add(banner);
                db.SaveChanges();
                Success("Thêm thành công : " + banner.Name, true);
                CacheHelper.GetInstance().GetBanner(true, db);
                return RedirectToAction("Index");
            }

            ViewBag.BannerPositionId = new SelectList(db.BannerPositions, "BannerPositionId", "Name", banner.BannerPositionId);
            return View(banner);
        }

        // GET: Admin/Banners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            ViewBag.BannerPositionId = new SelectList(db.BannerPositions, "BannerPositionId", "Name", banner.BannerPositionId);
            return View(banner);
        }

        // POST: Admin/Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Banner banner)
        {
            banner.Description = "banner";
            banner.EndDate = banner.StartDate = DateTime.Now;
            banner.ItemFor = banner.ItemForId = 1;
            //if (ModelState.IsValid)
            {
                db.Entry(banner).State = EntityState.Modified;
                db.SaveChanges();
                Success("Sửa thành công : " + banner.Name, true);
                CacheHelper.GetInstance().GetBanner(true, db);
                return RedirectToAction("Index");
            }
            ViewBag.BannerPositionId = new SelectList(db.BannerPositions, "BannerPositionId", "Name", banner.BannerPositionId);
            return View(banner);
        }

        // GET: Admin/Banners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            if (banner == null)
            {
                return HttpNotFound();
            }
            return View(banner);
        }

        // POST: Admin/Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Banner banner = db.Banners.Find(id);
            db.Banners.Remove(banner);
            db.SaveChanges();
            Success("Xóa thành công : " + banner.Name, true);
            return RedirectToAction("Index");
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
