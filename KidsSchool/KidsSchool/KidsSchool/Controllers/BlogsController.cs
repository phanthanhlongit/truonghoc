using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using KidsSchool.Models.Dao;
using KidsSchool.Controllers;

namespace WebStoreShop.Controllers
{
    public class BlogsController : BaseController
    {
        // GET: Blogs
        //[OutputCache(VaryByParam = "page", Duration = 60)]
        public ActionResult Index(int? page)
        {
            return Redirect("/404.html");
            int pageNumber = (page ?? 1);
            var pageSize = 8;
            try
            {
                var cate = DataPuplic.Cate.Where(x => x.Slug.Contains("trang")).FirstOrDefault();
                if (cate != null)
                {
                    var model = DataPuplic.post.Where(x => x.CatId != cate.Id).OrderByDescending(p => p.createDate).ToList();
                    return View(model.ToPagedList(pageNumber, pageSize));
                }
            }
            catch { }
            var model2 = DataPuplic.post.OrderByDescending(p => p.createDate).ToList();
            return View(model2.ToPagedList(pageNumber, pageSize));
        }


        // GET: Category
        [OutputCache(VaryByParam = "Slug;page", Duration = 300)]
        public ActionResult Category(string Slug, int? page)
        {
            int pageNumber = (page ?? 1);
            var pageSize = 9;

            var Cat = db.Categories.Where(c => c.Slug == Slug).FirstOrDefault();
            if (Cat == null|| Slug.ToLower().Trim()=="trang" || Slug.ToLower().Trim() == "blog")
            {
                return Redirect("/404.html");
            }
            var model = Cat.Posts.Where(p => p.Active == true && (p.AutoPostDate == null || p.AutoPostDate<DateTime.Now)).OrderByDescending(p => p.createDate).ToList();
            ViewBag.Cat = Cat;
            return View(model.ToPagedList(pageNumber, pageSize));
        }

        //[OutputCache(VaryByParam = "Slug", Duration = 3600)]
        public ActionResult ShowPost(string Slug)
        {
            var Post = new Post();
          
            if (Post!=null && !string .IsNullOrWhiteSpace(User.Identity.Name))
            {
                Post = db.Posts.SingleOrDefault(p => p.Slug == Slug);
            }else
            {
                Post = db.Posts.SingleOrDefault(p => p.Slug == Slug && (p.AutoPostDate == null || p.AutoPostDate < DateTime.Now)); 
            }

            if (Post == null)
            {
                return Redirect("/404.html");
            }


            ViewBag.post = Post;
            ViewBag.Cat = Post.Category;
            Post.Views++;
            db.SaveChanges();
            return View(Post);
        }

        [OutputCache(VaryByParam = "Slug", Duration = 300)]
        public ActionResult _Sidebar()
        {

            ViewBag.Cats = DataPuplic.Cate.Where(x => x.Name.ToLower() != "trang" && x.ParentId!=null).ToList();
            ViewBag.CatSidebar = DataPuplic.Cate.Where(x => x.ParentId==10).ToList();
            try
            {
                var cate = DataPuplic.Cate.FirstOrDefault(x => x.Slug.Contains("trang"));
                if (cate != null)
                {
                    var model = DataPuplic.post.Where(x => x.CatId != cate.Id && (x.AutoPostDate == null || x.AutoPostDate < DateTime.Now)).OrderByDescending(p => p.createDate).Take(50).ToList();
                    return PartialView(model);
                }
            }
            catch { }
            var model2 = DataPuplic.post.OrderByDescending(p => p.createDate).Take(50).ToList();
            return PartialView(model2);
        }

        [OutputCache(VaryByParam = "Slug", Duration = 300)]
        public ActionResult _LastNews()
        {
            var model = DataPuplic.post.Where(p => (p.AutoPostDate == null || p.AutoPostDate < DateTime.Now)).OrderByDescending(p => p.Id).Take(4);
            return PartialView(model);
        }
    }
}
