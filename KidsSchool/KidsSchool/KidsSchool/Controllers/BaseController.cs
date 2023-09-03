using KidsSchool.Models.Commons.Libs;
using KidsSchool.Models.DB;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace KidsSchool.Controllers
{
    public class BaseController : Controller
    {

        public Entities db = new Entities();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(Alert.TempDataKey)
                ? (List<Alert>)TempData[Alert.TempDataKey]
                : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[Alert.TempDataKey] = alerts;
        }

        public ActionResult Logout()
        {
            try
            {
                // Xóa phiên đăng nhập của người dùng
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            }
            catch { }

            // Thực hiện các hành động khác sau khi đăng xuất
            // Ví dụ: chuyển hướng người dùng đến trang đăng nhập
            return RedirectToAction("Login", "Account");
        }
    }
}