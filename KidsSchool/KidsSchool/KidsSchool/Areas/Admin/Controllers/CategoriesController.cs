using KidsSchool.Models.DB;
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

    [Authorize(Roles = "Quản trị viên,Nhân viên")]
    public class CategoriesController : AdminController
    {
        // GET: Admin/Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.Categories.Where(x=>x.ParentId ==null), "Id", "Name", category.ParentId);
            return View(category);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.Name.ToAscii();
                //category.DateUpdate = DateTime.Now;
                db.Entry(category).State = EntityState.Modified;
                #region seoUrl
                var urlRecord = db.SeoUrlRecords.Where(u => u.objectId == category.Id.ToString() && u.controller == "Blogs").ToList();
                if (urlRecord.Count == 0)
                {
                    var seoUrl = new SeoUrlRecord
                    {
                        controller = "Blogs",
                        action = "Category",
                        url = category.Slug,
                        objectId = category.Id.ToString()
                    };
                    db.SeoUrlRecords.Add(seoUrl);
                }
                else
                {
                    urlRecord[0].controller = "Blogs";
                    urlRecord[0].action = "Category";
                    urlRecord[0].url = category.Slug;
                    urlRecord[0].objectId = category.Id.ToString();
                }
                #endregion
                db.SaveChanges();
                Success("Sửa thành công thông tin danh mục: " + category.Name, true);
                //DataPuplic.UpdateCate(db, true);
                return RedirectToAction("Index", "Posts");
            }
            ViewBag.ParentId = new SelectList(db.Categories, "Id", "Name", category.ParentId);
            return View(category);
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
