using KidsSchool.Models.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KidsSchool.Models.Dao;
using KidsSchool.Models;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace KidsSchool.Areas.Admin.Controllers
{

    [Authorize]
    public class MenusController : AdminController
    {

        // GET: Admin/Menus
        public ActionResult Index()
        {

            var obj = new List<RecursiveMenuView>();
            try
            {
                // Lấy dữ liệu từ cache
                var cachedValue = MemoryCacheManager.Get<List<RecursiveMenuView>>("RecursiveMenuView");
                if (cachedValue == null)
                {
                    obj = db.RecursiveMenuViews.OrderBy(x => x.Path).ToList();
                    // Lưu dữ liệu vào cache
                    MemoryCacheManager.Set("RecursiveMenuView", obj, DateTimeOffset.Now.AddHours(1));
                    // Xóa dữ liệu khỏi cache
                    //MemoryCacheManager.Remove("cacheKey");
                }
                else
                {
                    obj = cachedValue;
                }
            }
            catch(Exception ex)
            {
                throw (ex);
            }

            return View(obj.ToList());
        }

        // GET: Admin/Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: Admin/Menus/Create
        public ActionResult Create()
        {
            ViewBag.ListMenu = new SelectList(db.SeoUrlRecords.Where(x => x.objectId != null), "url", "url");
            ViewBag.LocationId = new SelectList(db.MenuLocations, "Id", "Name");
            ViewBag.ParentId = new SelectList(db.RecursiveMenuViews.Where(x=>x.ParentId==null), "Id", "Path");
            return View();
        }

        // POST: Admin/Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(menu);
                db.SaveChanges();
                Success("Tạo thành công Menu: " + menu.Text, true);
                CacheHelper.GetInstance().GetMenu(true, db);
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.RecursiveMenuViews.Where(x => x.ParentId == null), "Id", "Path", menu.ParentId);
            ViewBag.LocationId = new SelectList(db.MenuLocations, "Id", "Name", menu.LocationId);
            ViewBag.ListMenu = new SelectList(db.SeoUrlRecords.Where(x => x.objectId != null), "url", "url");
            return View(menu);
        }

        // GET: Admin/Menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListMenu = new SelectList(db.SeoUrlRecords.Where(x => x.objectId != null), "url", "url");
            ViewBag.LocationId = new SelectList(db.MenuLocations, "Id", "Name", menu.LocationId);
            ViewBag.ParentId = new SelectList(db.RecursiveMenuViews.Where(x => x.ParentId == null), "Id", "Path", menu.ParentId);
            return View(menu);
        }

        // POST: Admin/Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                Success("Sửa thành công Menu: " + menu.Text, true);
                CacheHelper.GetInstance().GetMenu(true, db);
                return RedirectToAction("Index");
            }
            ViewBag.ListMenu = new SelectList(db.SeoUrlRecords.Where(x => x.objectId != null), "url", "url");
            ViewBag.ParentId = new SelectList(db.RecursiveMenuViews.Where(x => x.ParentId == null), "Id", "Path", menu.ParentId);
            ViewBag.LocationId = new SelectList(db.MenuLocations, "Id", "Name", menu.LocationId);
            return View(menu);
        }

        // GET: Admin/Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Admin/Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menus.Find(id);
            db.Menus.Remove(menu);
            db.SaveChanges();
            CacheHelper.GetInstance().GetMenu(true);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeleteMenuAjax(string listid)
        {

            var info = new
            {
                success = false,
                msg = ""
            };

            try
            {
                string[] lisid = listid.Split(',');
                foreach (var strid in lisid)
                {
                    int id = 0;
                    if (Int32.TryParse(strid, out id))
                    {
                        var pro = db.Menus.Find(Convert.ToInt32(id));
                        if (pro != null)
                        {
                            db.Menus.Remove(pro);
                            db.SaveChanges();
                            CacheHelper.GetInstance().GetMenu(true);
                            info = new
                            {
                                success = true,
                                msg = ""
                            };
                        }
                    }
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
            return  Json(info, JsonRequestBehavior.AllowGet);
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
