using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;
using KidsSchool.Models.DB;
using System.Text.RegularExpressions;
using static System.Data.Entity.Infrastructure.Design.Executor;
using KidsSchool.Areas.Admin.Controllers;
using KidsSchool.Controllers;

namespace KidsSchool.Models.Dao
{
    public class DataPuplic: BaseController
    {
        public static List<Page> page = new List<Page>();
        public static List<Post> post = new List<Post>();
        public static List<Menu> menu = new List<Menu>();
        public static List<Category> Cate = new List<Category>();
        public static List<Banner> banner = new List<Banner>();
        public static Config config = new Config();

        private static DataPuplic instance = new DataPuplic();
        public static DataPuplic GetInstance()
        {
            return instance;
        }

        public void RegisterDataPuplic(Entities db)
        {
            GetPage(false);
            GetPost(false);
            GetCate(false);
            GetMenu(false);
            GetConfig(false);
        }   

        public Page SpecialPage(string slug)
        {
            var obj = new List<Page>();
            try
            {
                if (page.Count <= 0)
                {
                    page = obj = db.Pages.ToList();
                }
                else
                {
                    obj = page;
                }
            }
            catch
            {
            }
            return obj.Where(x=>x.slug.Contains(slug)).FirstOrDefault();
        }

        public List<Page> GetPage(bool isUpdate)
        {
            var obj = new List<Page>();
            try
            {
                if (page.Count <= 0 || isUpdate)
                {
                    var newpage = db.Pages.ToList();
                    page = obj = newpage;
                }
                else
                {
                    obj = page;
                }
            }
            catch
            {
            }
            return obj.OrderBy(x => x.DateUpdate).ToList();
        }
        public List<Page> GetPage(bool isUpdate, Entities db)
        {
            var obj = new List<Page>();
            try
            {
                if (page.Count <= 0 || isUpdate)
                {
                    var newpage = db.Pages.ToList();
                    page = obj = newpage;
                }
                else
                {
                    obj = page;
                }
            }
            catch
            {
            }
            return obj.OrderByDescending(x => x.DateUpdate).ToList();
        }
        public List<Post> GetPost(bool isUpdate)
        {
            var obj = new List<Post>();
            try
            {
                if (post.Count <= 0 || isUpdate)
                {
                    post = obj = db.Posts.Where(x => x.Active == true).ToList();
                }
                else
                {
                    obj = post;
                }
            }
            catch
            {
            }
            return obj.OrderByDescending(x => x.DateUpdate).ToList();
        }
        public List<Post> GetPost(bool isUpdate, Entities db)
        {
            var obj = new List<Post>();
            try
            {
                if (post.Count <= 0 || isUpdate)
                {
                    post = obj = db.Posts.Where(x => x.Active == true).ToList();
                }
                else
                {
                    obj = post;
                }
            }
            catch
            {
            }
            return obj.OrderByDescending(x => x.DateUpdate).ToList();
        }
        public List<Category> GetCate(bool isUpdate)
        {
            var obj = new List<Category>();
            try
            {
                if (Cate.Count <= 0 || isUpdate)
                {
                    Cate = obj = db.Categories.ToList();
                }
                else
                {
                    obj = Cate;
                }
            }
            catch
            {
            }
            return obj.Where(x => !x.IsDelete).OrderBy(x => x.DisplayOrder).ToList();
        }
        public List<Category> GetCate(bool isUpdate, Entities db)
        {
            var obj = new List<Category>();
            try
            {
                if (Cate.Count <= 0 || isUpdate)
                {
                    Cate = obj = db.Categories.ToList();
                }
                else
                {
                    obj = Cate;
                }
            }
            catch
            {
            }
            return obj.Where(x => !x.IsDelete).OrderBy(x => x.DisplayOrder).ToList();
        }
        public List<Menu> GetMenu(bool isUpdate)
        
        {
            var obj = new List<Menu>();
            try
            {
                if (menu.Count <= 0 || isUpdate)
                {
                    menu = obj = db.Menus.ToList();
                }
                else
                {
                    obj = menu;
                }
            }
            catch
            {
            }
            return obj.OrderBy(x => x.OrderBy).ToList();
        }
        public List<Menu> GetMenu(bool isUpdate, Entities db)

        {
            var obj = new List<Menu>();
            try
            {
                if (menu.Count <= 0 || isUpdate)
                {
                    menu = obj = db.Menus.ToList();
                }
                else
                {
                    obj = menu;
                }
            }
            catch
            {
            }
            return obj.OrderBy(x => x.OrderBy).ToList();
        }
        public List<Banner> GetBanner(bool isUpdate)

        {
            var obj = new List<Banner>();
            try
            {
                if (banner.Count <= 0 || isUpdate)
                {
                    banner = obj = db.Banners.ToList();
                }
                else
                {
                    obj = banner;
                }
            }
            catch
            {
            }
            return obj.OrderByDescending(x => x.DateUpdate).ToList();

        }
        public List<Banner> GetBanner(bool isUpdate, Entities db)
        {
            var obj = new List<Banner>();
            try
            {
                if (banner.Count <= 0 || isUpdate)
                {
                    banner = obj = db.Banners.ToList();
                }
                else
                {
                    obj = banner;
                }
            }
            catch
            {
            }

            return obj.OrderByDescending(x => x.DateUpdate).ToList();
        }
        public Config GetConfig(bool isUpdate)
        {
            var obj = new Config();
            try
            {
                if (config.id == 0 || isUpdate)
                {
                    config = obj = db.Configs.FirstOrDefault();
                }
                else
                {
                    obj = config;
                }
            }
            catch
            {
            }
            return obj;
        }
        public Config GetConfig(bool isUpdate, Entities db)
        {
            var obj = new Config();
            try
            {
                if (config == null || isUpdate)
                {
                    config = obj = db.Configs.FirstOrDefault();
                }
                else
                {
                    obj = config;
                }
            }
            catch
            {
            }
            return obj;
        }

        public string GetSlugFromUrl(string url)
        {
            // Remove protocol and domain
            string path = new Uri(url).AbsolutePath;

            // Remove leading and trailing slashes
            path = path.Trim('/');

            // Extract slug from the path
            string slug = path.Substring(path.LastIndexOf('/') + 1);

            // Optionally, remove file extensions if present
            slug = Regex.Replace(slug, @"\.[^.]*$", "");

            return slug;
        }

    }
}