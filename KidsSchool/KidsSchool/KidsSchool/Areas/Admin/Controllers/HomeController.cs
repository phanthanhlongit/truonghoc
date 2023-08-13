using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using KidsSchool.Models.GenarateSitemap;
using System.IO;
using Newtonsoft.Json;

namespace KidsSchool.Areas.Admin.Controllers
{

    [Authorize(Roles = "Quản trị viên,Nhân viên,Quản lý nguyên liệu,Quản lý công thức")]
    public class HomeController : AdminController
    {
        // GET: admin/Default
        public ActionResult Index()
        {
            ViewBag.NewOrders = 0;
            ViewBag.CountOrder = 0;
            ViewBag.CountProduct = 0;
            ViewBag.CountProductExits = 0;
            ViewBag.CountUsers = db.AspNetUsers.Count();
            return View();
        }

        public ActionResult Header()
        {
            //ViewBag.Orders = db.Orders.Where(p => p.StatusId == 1).ToList();

            return PartialView("_Header");
        }

        [OutputCache(Duration = 86400)]
        public ActionResult _sitemap()
        {
            try
            {
                var dateWirteFile = System.IO.File.GetLastWriteTime(Server.MapPath("~/sitemap.xml"));
                if ((DateTime.Now - dateWirteFile).TotalHours < 24)
                {
                    return PartialView();
                }
                var listPost = db.Posts.Where(p =>  (p.createDate == null || p.createDate < DateTime.Now)).ToList();
                var listCate = db.Categories.ToList();
                var listsitemapPost = new List<SitemapItem>();
                var listsitemapCate = new List<SitemapItem>();

                #region sitemap
                var listsitemap = new List<SitemapItem>(){
                new SitemapItem("https://KidsSchool.vn/category_sitemap.xml", lastModified: DateTime.Now, priority: 1.0),
                new SitemapItem("https://KidsSchool.vn/page_sitemap.xml", lastModified: DateTime.Now, priority: 1),
                new SitemapItem("https://KidsSchool.vn/post_sitemap.xml", lastModified: DateTime.Now, priority: 1)
            };
                #endregion


                #region Page
                #endregion

                #region post
                if (listPost.Count > 0)
                {
                    foreach (var item in listPost)
                    {
                        listsitemapPost.Add(new SitemapItem(Request.Url.GetLeftPart(UriPartial.Authority) + Url.RouteUrl("FriendlyRoute", new { FriendlyUrl = item.Slug }), lastModified: DateTime.Now, changeFrequency: SitemapChangeFrequency.Daily, priority: 0.8));
                    }
                }
                #endregion

                #region Cate
                if (listCate.Count > 0)
                {
                    foreach (var item in listCate)
                    {
                        if(!string.IsNullOrEmpty(item.Slug)&&!item.Slug.Equals("trang"))
                        {
                            listsitemapCate.Add(new SitemapItem(Request.Url.GetLeftPart(UriPartial.Authority) + Url.RouteUrl("FriendlyRouteCate", new { FriendlyUrl = item.Slug }), lastModified: DateTime.Now, changeFrequency: SitemapChangeFrequency.Daily, priority: 0.3));
                        }
                    }
                }
                #endregion

                SitemapGenerator sg = new SitemapGenerator();
                var docsitemap = sg.GenerateSiteMapIndex(listsitemap);
                docsitemap.Save(Server.MapPath("~/sitemap.xml"));

                var docPost = sg.GenerateSiteMap(listsitemapPost);
                docPost.Save(Server.MapPath("~/post_sitemap.xml"));

                //var docPage = sg.GenerateSiteMap(listsitemapPage);
                //docPage.Save(Server.MapPath("~/page_sitemap.xml"));

                var docCate = sg.GenerateSiteMap(listsitemapCate);
                docCate.Save(Server.MapPath("~/category_sitemap.xml"));
            }
            catch(Exception ex)
            { }
            return PartialView();

        }
    }
}
