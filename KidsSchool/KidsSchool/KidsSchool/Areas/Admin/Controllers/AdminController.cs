using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using KidsSchool.Areas.Admin.Common;
using KidsSchool.Models.DB;
using KidsSchool.Models.Commons.Libs;

namespace KidsSchool.Areas.Admin.Controllers
{
    public abstract class AdminController : Controller
    {
        protected Entities db = new Entities();

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
        //private void AddAlert(string alertStyle, string message, bool dismissable)
        //{
        //    List<Alert> alerts = new List<Alert>(); ;
        //    if (TempData.ContainsKey(Alert.TempDataKey))
        //    {
        //        var x = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(TempData[Alert.TempDataKey] + "");

        //        if (x.Any())
        //        {

        //            foreach (var alert in x)
        //            {
        //                Alert alert_ = new Alert()
        //                {
        //                    AlertStyle = alert.ElementAt(0).ToString().Replace("\"AlertStyle\":", "").Replace("\"", "").Replace(" ", ""),
        //                    Message = alert.ElementAt(1).ToString().Replace("\"Message\":", "").Replace("\"", ""),
        //                    Dismissable = (bool)alert.ElementAt(2)
        //                };
        //                alerts.Add(alert_);
        //            }

        //        }

        //    }

        //    alerts.Add(new Alert
        //    {
        //        AlertStyle = alertStyle,
        //        Message = message,
        //        Dismissable = dismissable
        //    });


        //    var x_ = JsonConvert.SerializeObject(alerts);
        //    TempData[Alert.TempDataKey] = x_;
        //}
    }

}