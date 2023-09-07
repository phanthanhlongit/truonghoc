using KidsSchool.Models.DB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using KidsSchool.Models.Dao;
using System.Web.Services.Description;

namespace KidsSchool.Areas.Admin.Controllers
{

    [Authorize]
    public class PostsController : AdminController
    {
        // GET: Admin/Posts
        public ActionResult Index(int? catId)
        {
            var posts = db.Posts.Include(p => p.Category);

            if (catId != null)
            {
                posts = posts.Where(p=>p.CatId == catId);
            }
            ViewBag.Group= CacheHelper.GetInstance().GetCate(false).Where(x=>!x.IsDelete&& x.ParentId!=null && !x.Slug.Equals("trang"));
            return View(posts.ToList());
        }

        #region ValidatePage
        public void ValidatePost(Post post)
        {
            if (!string.IsNullOrEmpty(post.Slug) && post.Slug.hasSpecialCharUrl())
            {
                ModelState.AddModelError("Slug", "Url không được chứa ký tự |!#$%&/()=?»«@£§€{}.;'<>_,");
            }

            if (post.Title == null)
            {
                ModelState.AddModelError("Title", "Vui lòng nhập tiêu đề.");
            }

            if (post.CatId == null)
            {
                ModelState.AddModelError("CatId", "Vui lòng chọn nhóm danh mục.");
            }

            if (post.Content == null)
            {
                ModelState.AddModelError("Content", "Vui lòng nhập nội dung tin.");
            }
        }
        #endregion

        // GET: Admin/Posts/Create
        public ActionResult Create()
        {
            ViewBag.CatId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }


        // POST: Admin/Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            ValidatePost(post);
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(post.Slug))
                {
                    post.Slug = post.Title.ToUniqueUrl(db);
                }
                else
                {
                    post.Slug = post.Slug.ToUniqueUrl(db);
                }
                post.Slug = post.Slug.ToLower();
                post.Active = true;
                post.createDate = DateTime.Now;
                post.DateUpdate = DateTime.Now;
                db.Posts.Add(post);
                db.SaveChanges();
                #region seoUrl
                var seoUrl = new SeoUrlRecord
                {
                    controller = "Blogs",
                    action = "ShowPost",
                    url = post.Slug,
                    objectId = post.Id.ToString()
                };
                db.SeoUrlRecords.Add(seoUrl);
                #endregion
                db.SaveChanges();
                CacheHelper.GetInstance().GetPost(true,db);
                Success("Thêm trang thành công: " + post.Title, true);
                if (post.CatId != null && post.CatId != 0)
                {
                    return RedirectToAction("Index", new { catId = post.CatId });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.CatId = new SelectList(db.Categories, "Id", "Name", post.CatId);
            return View(post);
        }

        // GET: Admin/Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatId = new SelectList(db.Categories, "Id", "Name", post.CatId);
            return View(post);
        }

        // POST: Admin/Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            ValidatePost(post);
            if (ModelState.IsValid)
            {
                var oldEntity = CacheHelper.GetInstance().GetPost(false).FirstOrDefault(x => x.Id == post.Id);
                if (oldEntity != null)
                {
                    if (oldEntity.Title != post.Title || string.IsNullOrEmpty(post.Slug))
                    {
                        post.Slug = post.Title.ToUniqueUrl(db);
                    }
                    else if (oldEntity.Slug != post.Slug)
                    {
                        post.Slug = post.Slug.ToUniqueUrl(db);
                    }
                }

                post.DateUpdate = DateTime.Now;
                db.Entry(post).State = EntityState.Modified;
                #region seoUrl
                var urlRecord = db.SeoUrlRecords.Where(u => u.objectId == post.Id.ToString() && u.controller == "Blogs" && u.action == "ShowPost").ToList();
                if (urlRecord.Count == 0)
                {
                    var seoUrl = new SeoUrlRecord
                    {
                        controller = "Blogs",
                        action = "ShowPost",
                        url = post.Slug,
                        objectId = post.Id.ToString()
                    };
                    db.SeoUrlRecords.Add(seoUrl);
                }
                else
                {
                    urlRecord[0].controller = "Blogs";
                    urlRecord[0].action = "ShowPost";
                    urlRecord[0].url = post.Slug;
                    urlRecord[0].objectId = post.Id.ToString();
                }
                #endregion
                db.SaveChanges();
                Success("Thay đổi thông tin tin thành công: " + post.Title, true);
                CacheHelper.GetInstance().GetPost(true,db);
                if (post.CatId != null && post.CatId != 0)
                {
                    return RedirectToAction("Index",new { catId = post.CatId });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CatId = new SelectList(db.Categories, "Id", "Name", post.CatId);
            return View(post);
        }

        public ActionResult DeletePostAjax(string listid)
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
                        var pro = db.Posts.Find(Convert.ToInt32(id));
                        if (pro != null)
                        {
                            db.Posts.Remove(pro);
                            db.SaveChanges();
                            CacheHelper.GetInstance().GetPost(true);
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

        public ActionResult DeletePostCate(string listid)
        {

            var info = new
            {
                success = false,
                msg = ""
            };
            #region valiodate
           
            #endregion
            try
            {
                string[] lisid = listid.Split(',');
                foreach (var strid in lisid)
                {
                    int id = 0;
                    if (Int32.TryParse(strid, out id))
                    {
                        var pro = db.Categories.Find(Convert.ToInt32(id));
                        if (pro != null)
                        {
                            pro.DateUpdate = DateTime.Now;
                            pro.IsDelete = true;
                            db.SaveChanges();
                            CacheHelper.GetInstance().GetCate(true);
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

        public ActionResult AddPostCate(string groupName)
        {

            var info = new
            {
                success = false,
                msg = ""
            };

            try
            {
                var cate = new Category();
                //Id,Name,Slug,Description,ParentId,DisplayOrder,MetaTitle,MetaDescription,MetaKeyword,Icon,ImageIcon,IsDelete
                cate.ParentId = 9;
                cate.Name = groupName;
                if (string.IsNullOrEmpty(cate.Slug))
                {
                    cate.Slug = cate.Name.ToUniqueUrl(db);
                }
                else
                {
                    cate.Slug = cate.Slug.ToUniqueUrl(db);
                }
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
                CacheHelper.GetInstance().GetCate(true);
                info = new
                {
                    success = true,
                    msg = ""
                };
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

        public ActionResult ExportExcel()
        {
            try
            {
                var grid = new GridView();
                grid.DataSource = GetTable();
                grid.DataBind();

                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment; filename=MyExcelPost"+DateTime.Now.ToString("dd-MM-yyyy")+".xls");
                Response.ContentType = "application/ms-excel";

                Response.Charset = "";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                grid.RenderControl(htw);

                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
             
            }
            return PartialView();
        }

        public DataTable GetTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("URL", typeof(string));
            table.Columns.Add("Meta keyword", typeof(string));
            table.Columns.Add("Chuyên mục", typeof(string));

            var posts = db.Posts.Include(p => p.Category);

            foreach(var info in posts)
            {
                table.Rows.Add(info.Id, info.Title, "http://KidsSchool.vn/" + info.Slug, info.MetaKeyword, info.Category.Name);
            }    
            return table;
        }


        public class EntityExcel
        {
            public int id { get; set; }
            public string Title { get; set; }
            public string URL { get; set; }
            public string keyword { get; set; }
            public string Group { get; set; }
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
