using KidsSchool.Models.DB;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace KidsSchool.Areas.Admin.Controllers
{
    public class ContactGHelpsController : AdminController
    {

        [Authorize]
        // GET: Admin/ContactGHelps
        public ActionResult Index()
        {
            var contactGHelps = db.ContactGHelps.Where(x=>!x.IsDelete).Include(c => c.ContactGHelpGroup).OrderByDescending(x=>x.Upd_Date);
            return View(contactGHelps.ToList());
        }

        // GET: Admin/ContactGHelps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactGHelp contactGHelp = db.ContactGHelps.Find(id);
            if (contactGHelp == null)
            {
                return HttpNotFound();
            }
            return View(contactGHelp);
        }

        // GET: Admin/ContactGHelps/Create
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(db.ContactGHelpGroups, "Id", "Name");
            return View();
        }

        // POST: Admin/ContactGHelps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Phone,CusName,Address,GroupId,Upd_Date,IsDelete")] ContactGHelp contactGHelp)
        {
            if (ModelState.IsValid)
            {
                db.ContactGHelps.Add(contactGHelp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupId = new SelectList(db.ContactGHelpGroups, "Id", "Name", contactGHelp.GroupId);
            return View(contactGHelp);
        }

        // GET: Admin/ContactGHelps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactGHelp contactGHelp = db.ContactGHelps.Find(id);
            if (contactGHelp == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupId = new SelectList(db.ContactGHelpGroups, "Id", "Name", contactGHelp.GroupId);
            return View(contactGHelp);
        }

        // POST: Admin/ContactGHelps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactGHelp contactGHelp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactGHelp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(db.ContactGHelpGroups, "Id", "Name", contactGHelp.GroupId);
            return View(contactGHelp);
        }

        // GET: Admin/ContactGHelps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactGHelp contactGHelp = db.ContactGHelps.Find(id);
            if (contactGHelp == null)
            {
                return HttpNotFound();
            }
            return View(contactGHelp);
        }

        // POST: Admin/ContactGHelps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactGHelp contactGHelp = db.ContactGHelps.Find(id);
            db.ContactGHelps.Remove(contactGHelp);
            db.SaveChanges();
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
