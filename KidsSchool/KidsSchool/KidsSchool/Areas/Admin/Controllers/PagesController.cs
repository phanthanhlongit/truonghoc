using KidsSchool.Models.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace KidsSchool.Areas.Admin.Controllers
{

    [Authorize(Roles = "Quản trị viên")]
    public class PagesController : AdminController
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            return View(db.Pages.ToList());
        }
        
        // GET: Admin/Pages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // GET: Admin/Pages/Create
        public ActionResult Create()
        {
            return View();
        }


        #region ValidatePage
        public void ValidatePage(Page page)
        {
            if (page.title == null)
            {
                ModelState.AddModelError("title", "Vui lòng nhập tiêu đề.");
            }

            if (page.content == null)
            {
                ModelState.AddModelError("title", "Vui lòng nhập nội dung trang.");
            }
        }
        #endregion
        // POST: Admin/Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Page page)
        {
            ValidatePage(page);
            if (ModelState.IsValid)
            {
                if(string.IsNullOrEmpty(page.slug))
                {
                    page.slug = page.title.ToAscii();
                }
                page.slug = page.slug.ToAscii();
                page.DateUpdate = page.DateCreate = DateTime.Now;
                page.Type = 1;
                db.Pages.Add(page);
                db.SaveChanges();

                #region seoUrl
                var seoUrl = new SeoUrlRecord
                {
                    controller = "Pages",
                    action = "Index",
                    url = page.slug,
                    objectId = page.id.ToString()
                };
                db.SeoUrlRecords.Add(seoUrl);
                #endregion

                db.SaveChanges();
                Success("Thêm thành công trang: " + page.title, true);
                return RedirectToAction("Index");
            }

            return View(page);
        }

        // GET: Admin/Pages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = db.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Page page)
        {
            ValidatePage(page);
            if (ModelState.IsValid)
            {
                if (page.slug == null)
                {
                    page.slug = page.title.ToAscii();
                }
                page.slug = page.slug.ToAscii();
                page.DateUpdate = DateTime.Now;
                db.Entry(page).State = EntityState.Modified;
                #region seoUrl
                if (page.Type == 1)
                {
                    var urlRecord = db.SeoUrlRecords.Where(u => u.objectId == page.id.ToString() && u.controller == "Pages").ToList();
                    if (urlRecord.Count == 0)
                    {
                        var seoUrl = new SeoUrlRecord
                        {
                            controller = "Pages",
                            action = "Index",
                            url = page.slug,
                            objectId = page.id.ToString()
                        };
                        db.SeoUrlRecords.Add(seoUrl);
                    }
                    else
                    {
                        urlRecord[0].controller = "Pages";
                        urlRecord[0].action = "Index";
                        urlRecord[0].url = page.slug;
                        urlRecord[0].objectId = page.id.ToString();
                    }
                }
                #endregion
                db.SaveChanges();
                Success("Thay đổi thông tin trang thành công: " + page.title, true);
                return RedirectToAction("Index");
            }
            return View(page);
        }

        public ActionResult DeletePagesAjax(string listid)
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
                        var pro = db.Pages.Find(Convert.ToInt32(id));
                        if (pro != null)
                        {
                            pro.DateUpdate = DateTime.Now;
                            db.Pages.Remove(pro);
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
