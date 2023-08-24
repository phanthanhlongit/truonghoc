using System;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using KidsSchool.Models.Dao;
using KidsSchool.Models.DB;

namespace KidsSchool.Controllers
{
    public class BlogsController : BaseController
    {
        // GET: Blogs
        //[OutputCache(VaryByParam = "page", Duration = 60)]
        public ActionResult Index(int? page)
        {
            return Redirect("/404.html");
        }


        // GET: Category
        //[OutputCache(VaryByParam = "Slug;page", Duration = 300)]
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

            ViewBag.Cats = DataPuplic.GetInstance().GetCate(false).Where(x => x.Name.ToLower() != "trang" && x.ParentId!=null).ToList();
            ViewBag.CatSidebar = DataPuplic.GetInstance().GetCate(false).Where(x => x.ParentId==10).ToList();
            try
            {
                var cate = DataPuplic.GetInstance().GetCate(false).FirstOrDefault(x => x.Slug.Contains("trang"));
                if (cate != null)
                {
                    var model = DataPuplic.GetInstance().GetPost(false).Where(x => x.CatId != cate.Id && (x.AutoPostDate == null || x.AutoPostDate < DateTime.Now)).OrderByDescending(p => p.createDate).Take(50).ToList();
                    return PartialView(model);
                }
            }
            catch { }
            var model2 = DataPuplic.GetInstance().GetPost(false).OrderByDescending(p => p.createDate).Take(50).ToList();
            return PartialView(model2);
        }

        [OutputCache(VaryByParam = "Slug", Duration = 300)]
        public ActionResult _LastNews()
        {
            var model = DataPuplic.GetInstance().GetPost(false).Where(p => (p.AutoPostDate == null || p.AutoPostDate < DateTime.Now)).OrderByDescending(p => p.Id).Take(4);
            return PartialView(model);
        }
    }
}
