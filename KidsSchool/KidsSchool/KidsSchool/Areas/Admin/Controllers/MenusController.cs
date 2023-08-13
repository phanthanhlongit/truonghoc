using KidsSchool.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KidsSchool.Areas.Admin.Controllers
{

    [Authorize(Roles = "Quản trị viên")]
    public class MenusController : AdminController
    {

        // GET: Admin/Menus
        public ActionResult Index()
        {
            var menus = db.Menus.Where(x=>x.ParentId ==null).OrderBy(x => x.OrderBy).Include(m => m.MenuLocation);
            ViewBag.MenuChillDen = db.Menus.Where(x => x.ParentId != null).OrderBy(x => x.OrderBy).Include(m => m.Menu1).Include(m => m.MenuLocation);
            return View(menus.ToList());
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
            ViewBag.LocationId = new SelectList(db.MenuLocations, "Id", "Name");
            ViewBag.ParentId = new SelectList(db.Menus.Where(x=>x.ParentId==null), "Id", "Text");
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
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.Menus.Where(x => x.ParentId == null), "Id", "Text", menu.ParentId);
            ViewBag.LocationId = new SelectList(db.MenuLocations, "Id", "Name", menu.LocationId);
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
            ViewBag.LocationId = new SelectList(db.MenuLocations, "Id", "Name", menu.LocationId);
            ViewBag.ParentId = new SelectList(db.Menus.Where(x => x.ParentId == null), "Id", "Text", menu.ParentId);
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
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.Menus.Where(x => x.ParentId == null), "Id", "Text", menu.ParentId);
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
