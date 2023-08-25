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
using System.Web.Services.Description;

namespace KidsSchool.Areas.Admin.Controllers
{

    [Authorize(Roles = "Quản trị viên,Nhân viên")]
    public class CategoriesController : AdminController
    {
        // GET: Admin/Posts
        public ActionResult Index()
        {
            var cate = db.Categories;
            return View(db.Categories.Where(x=>!x.IsDelete).ToList());
        }

        #region ValidatePage
        public void ValidatePost(Category cate)
        {
            if (!string.IsNullOrEmpty(cate.Slug) && cate.Slug.hasSpecialCharUrl())
            {
                ModelState.AddModelError("Slug", "Url không được chứa ký tự |!#$%&/()=?»«@£§€{}.;'<>_,");
            }

            if (cate.Name == null)
            {
                ModelState.AddModelError("Title", "Vui lòng nhập tiêu đề.");
            }
        }
        #endregion

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.ParentId = new SelectList(db.Categories, "Id", "Name", "--Chọn danh mục--");
            return View();
        }


        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category cate)
        {
            ValidatePost(cate);
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(cate.Slug))
                {
                    cate.Slug = cate.Name.ToUniqueUrl(db);
                }
                else
                {
                    cate.Slug = cate.Slug.ToUniqueUrl(db);
                }
                cate.DateCreate = DateTime.Now;
                cate.DateUpdate = DateTime.Now;
                db.Categories.Add(cate);
                db.SaveChanges();
                #region seoUrl
                var seoUrl = new SeoUrlRecord
                {
                    controller = "Blogs",
                    action = "Category",
                    url = cate.Slug,
                    objectId = cate.Id.ToString()
                };
                db.SeoUrlRecords.Add(seoUrl);
                #endregion
                db.SaveChanges();
                CacheHelper.GetInstance().GetCate(true, db);
                Success("Thêm danh mục thành công: " + cate.Name, true);
                ViewBag.ParentId = new SelectList(db.Categories, "Id", "Name", cate.ParentId);
                return RedirectToAction("Index");
            }

            ViewBag.ParentId = new SelectList(db.Categories, "Id", "Name", "--Chọn danh mục--");
            return View(cate);
        }

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
               var oldEntity = CacheHelper.GetInstance().GetCate(false).FirstOrDefault(x => x.Id == category.Id);
                if (oldEntity!=null)
                {
                    if(oldEntity.Name != category.Name || string.IsNullOrEmpty(category.Slug))
                    {
                        category.Slug = category.Name.ToUniqueUrl(db);
                    }
                    else if(oldEntity.Slug != category.Slug)
                    {
                        category.Slug = category.Slug.ToUniqueUrl(db);
                    }    
                }    
                category.DateUpdate = DateTime.Now;
                db.Entry(category).State = EntityState.Modified;
                #region seoUrl
                var urlRecord = db.SeoUrlRecords.Where(u => u.objectId == category.Id.ToString() && u.controller == "Blogs" && u.action == "Category").ToList();
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
                CacheHelper.GetInstance().GetCate(true, db);
                return RedirectToAction("Index");
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
