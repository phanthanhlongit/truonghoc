using KidsSchool.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace KidsSchool
{
    public class FriendlyRouteHandler : MvcRouteHandler
    {
        public Entities db = new Entities();
        //List<urlRecord> url = new List<urlRecord>
        //    {
        //        new urlRecord { controller = "Products", action = "Detail", objectId = "1258", url="cream-trang-da" },
        //        new urlRecord { controller = "Products", action = "Detail", objectId = "1259",  url="serum-nam-na-152-"},
        //        new urlRecord { controller = "Blogs", action = "ShowPost", objectId = "6" , url="gia-cong-son-moi-xay-dung-niem-tin-thuong-hieu-son-viet"}
        //    };
        protected override IHttpHandler GetHttpHandler(System.Web.Routing.RequestContext requestContext)
        {
            if (requestContext.RouteData.Values["FriendlyUrl"] != null)
            {
                string friendlyUrl = requestContext.RouteData.Values["FriendlyUrl"].ToString();

                //Here, you would look up the URL Record in your database, then assign the values to Route Data
                //using "where urlRecord.Url == friendlyUrl"         
                try
                {
                    var urlRecord = db.SeoUrlRecords.FirstOrDefault(u => u.url == friendlyUrl);
                    //Now, we can assign the values to routeData
                    if (urlRecord != null)
                    {
                        requestContext.RouteData.Values["controller"] = urlRecord.controller;
                        requestContext.RouteData.Values["action"] = urlRecord.action;
                        requestContext.RouteData.Values["Slug"] = urlRecord.url;
                        if (urlRecord.objectId != null)
                            requestContext.RouteData.Values["id"] = urlRecord.objectId;

                    }
                    else
                    {
                        //Here, I want to redirect to next RouteHandler in route Table
                        requestContext.RouteData.Values["controller"] = friendlyUrl;
                    }
                }
                catch (Exception ex)
                {
                    //throw;
                    //Here too, I want to redirect to next RouteHandler in route Table
                    requestContext.RouteData.Values["controller"] = friendlyUrl;
                }
            }
            return base.GetHttpHandler(requestContext);
        }
    }
    public class urlRecord
    {
        public string controller { get; set; }
        public string action { get; set; }
        public string objectId { get; set; }
        public string url { get; set; }
    }
}