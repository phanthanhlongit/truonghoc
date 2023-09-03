using KidsSchool.Models.Commons.Libs;
using KidsSchool.Models.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using static KidsSchool.Models.Commons.Libs.XMail;

namespace KidsSchool.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }

        //[OutputCache(Duration = 86400)]
        public ActionResult Header()
        {
            return PartialView("_HeaderPartial");
        }

        //[OutputCache(Duration = 86400)]
        public ActionResult Footer()
        {
            return PartialView("_FooterPartial");
        }

        [OutputCache(Duration = 86400)]
        public ActionResult NotFound()
        {
            return PartialView();
        }

        public ActionResult TemplateMailContact()
        {
            return PartialView();
        }

        public ActionResult SendContactAjax(string name,string chilname, string chilage, string email, string phone, string address, int type, string content)
        {
            var info = new
            {
                success = false,
                msg = ""
            };
            try
            {
                var obj = new ContactGHelp();
                obj.Phone = phone;
                obj.CusName = name;
                obj.ChilName = chilname;
                obj.ChilAge = chilage;
                obj.Mail = email;
                obj.Address = address;
                obj.Content = content;
                obj.GroupId = type;
                obj.DateCreate= obj.Upd_Date = DateTime.Now;
                obj.IsDelete = false;
                db.ContactGHelps.Add(obj);
                db.SaveChanges();
                info = new
                {
                    success = true,
                    msg = ""
                };

                #region send mail
                var sw = new StringWriter();
                var viewResult = ViewEngines.Engines.FindView(ControllerContext, "TemplateMailContact", null);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, new ViewDataDictionary(obj), new TempDataDictionary(), sw);

                // Render view email và lấy nội dung HTML
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);

                string emailBody = sw.ToString();
                XMail.senmail(WebConfigurationManager.AppSettings["ToSentEmail"].ToString(), "Khách hàng: "+name +" liên hệ" , emailBody);
                #endregion
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


    }
}