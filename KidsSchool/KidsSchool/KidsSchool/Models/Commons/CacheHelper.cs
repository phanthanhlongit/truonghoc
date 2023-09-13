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
    public class CacheHelper: BaseController
    {
        public static CacheHelper GetInstance()
        {
            return new CacheHelper();
        }
        public Page SpecialPage(string slug)
        {
            var obj = new List<Page>();
            try
            {
                var cachedValue = MemoryCacheManager.Get<List<Page>>("GetPage");
                if (cachedValue==null)
                {
                    obj = db.Pages.ToList();
                    // Lưu dữ liệu vào cache
                    MemoryCacheManager.Set("GetPage", obj, DateTimeOffset.Now.AddMinutes(10));
                    // Xóa dữ liệu khỏi cache
                    //MemoryCacheManager.Remove("cacheKey");
                }
                else
                {
                    obj = cachedValue;
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
                var cachedValue = MemoryCacheManager.Get<List<Page>>("GetPage");
                if (cachedValue== null || isUpdate)
                {
                    obj = db.Pages.ToList();
                    // Lưu dữ liệu vào cache
                    MemoryCacheManager.Set("GetPage", obj, DateTimeOffset.Now.AddMinutes(10));
                    // Xóa dữ liệu khỏi cache
                    //MemoryCacheManager.Remove("cacheKey");
                }
                else
                {
                    obj = cachedValue;
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
                var cachedValue = MemoryCacheManager.Get<List<Page>>("GetPage");
                if (cachedValue==null || isUpdate)
                {
                    obj = db.Pages.ToList();
                    // Lưu dữ liệu vào cache
                    MemoryCacheManager.Set("GetPage", obj, DateTimeOffset.Now.AddMinutes(10));
                    // Xóa dữ liệu khỏi cache
                    //MemoryCacheManager.Remove("cacheKey");
                }
                else
                {
                    obj = cachedValue;
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
                var cachedValue = MemoryCacheManager.Get<List<Post>>("GetPost");
                if (cachedValue==null || isUpdate)
                {
                    obj = db.Posts.Where(x => x.Active == true).ToList();
                    // Lưu dữ liệu vào cache
                    MemoryCacheManager.Set("GetPost", obj, DateTimeOffset.Now.AddHours(1));
                    // Xóa dữ liệu khỏi cache
                    //MemoryCacheManager.Remove("cacheKey");
                }
                else
                {
                    obj = cachedValue;
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
                var cachedValue = MemoryCacheManager.Get<List<Post>>("GetPost");
                if (cachedValue==null || isUpdate)
                {
                    obj = db.Posts.Where(x => x.Active == true).ToList();
                    // Lưu dữ liệu vào cache
                    MemoryCacheManager.Set("GetPost", obj, DateTimeOffset.Now.AddHours(1));
                    // Xóa dữ liệu khỏi cache
                    //MemoryCacheManager.Remove("cacheKey");
                }
                else
                {
                    obj = cachedValue;
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
                // Lấy dữ liệu từ cache
                var cachedValue = MemoryCacheManager.Get<List<Category>>("GetCate");
                if (cachedValue==null || isUpdate)
                {
                     obj = db.Categories.ToList();
                    // Lưu dữ liệu vào cache
                    MemoryCacheManager.Set("GetCate", obj, DateTimeOffset.Now.AddHours(1));
                    // Xóa dữ liệu khỏi cache
                    //MemoryCacheManager.Remove("cacheKey");
                }
                else
                {
                    obj = cachedValue;
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
                // Lấy dữ liệu từ cache
                var cachedValue = MemoryCacheManager.Get<List<Category>>("GetCate");
                if (cachedValue==null || isUpdate)
                {
                    obj = db.Categories.ToList();
                    // Lưu dữ liệu vào cache
                    MemoryCacheManager.Set("GetCate", obj, DateTimeOffset.Now.AddHours(1));
                    // Xóa dữ liệu khỏi cache
                    //MemoryCacheManager.Remove("cacheKey");
                }
                else
                {
                    obj = cachedValue;
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
                var cachedValue = MemoryCacheManager.Get<List<Menu>>("GetMenu");
                if (cachedValue==null || isUpdate)
                {
                    obj = db.Menus.ToList();
                    // Lưu dữ liệu vào cache
                    MemoryCacheManager.Set("GetMenu", obj, DateTimeOffset.Now.AddHours(1));
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
            return obj.OrderBy(x => x.OrderBy).ToList();
        }
        public List<Menu> GetMenu(bool isUpdate, Entities db)

        {
            var obj = new List<Menu>();
            try
            {
                var cachedValue = MemoryCacheManager.Get<List<Menu>>("GetMenu");
                if (cachedValue==null || isUpdate)
                {
                    obj = db.Menus.ToList();
                    // Lưu dữ liệu vào cache
                    MemoryCacheManager.Set("GetMenu", obj, DateTimeOffset.Now.AddHours(1));
                    // Xóa dữ liệu khỏi cache
                    //MemoryCacheManager.Remove("cacheKey");
                }
                else
                {
                    obj = cachedValue;
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
                var cachedValue = MemoryCacheManager.Get<List<Banner>>("GetBanner");
                if (cachedValue==null || isUpdate)
                {
                    obj = db.Banners.ToList();
                    // Lưu dữ liệu vào cache
                    MemoryCacheManager.Set("GetBanner", obj, DateTimeOffset.Now.AddHours(1));
                    // Xóa dữ liệu khỏi cache
                    //MemoryCacheManager.Remove("cacheKey");
                }
                else
                {
                    obj = cachedValue;
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
                var cachedValue = MemoryCacheManager.Get<List<Banner>>("GetBanner");
                if (cachedValue==null || isUpdate)
                {
                    obj = db.Banners.ToList();
                    // Lưu dữ liệu vào cache
                    MemoryCacheManager.Set("GetBanner", obj, DateTimeOffset.Now.AddHours(1));
                    // Xóa dữ liệu khỏi cache
                    //MemoryCacheManager.Remove("cacheKey");
                }
                else
                {
                    obj = cachedValue;
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
                var cachedValue = MemoryCacheManager.Get<Config>("GetConfig");
                if (cachedValue == null || isUpdate)
                {
                    obj = db.Configs.FirstOrDefault();
                    // Lưu dữ liệu vào cache
                    MemoryCacheManager.Set("GetConfig", obj, DateTimeOffset.Now.AddHours(1));
                    // Xóa dữ liệu khỏi cache
                    //MemoryCacheManager.Remove("cacheKey");
                }
                else
                {
                    obj = cachedValue;
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
                var cachedValue = MemoryCacheManager.Get<Config>("GetConfig");
                if (cachedValue == null || isUpdate)
                {
                    obj = db.Configs.FirstOrDefault();
                    // Lưu dữ liệu vào cache
                    MemoryCacheManager.Set("GetConfig", obj, DateTimeOffset.Now.AddHours(1));
                    // Xóa dữ liệu khỏi cache
                    //MemoryCacheManager.Remove("cacheKey");
                }
                else
                {
                    obj = cachedValue;
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