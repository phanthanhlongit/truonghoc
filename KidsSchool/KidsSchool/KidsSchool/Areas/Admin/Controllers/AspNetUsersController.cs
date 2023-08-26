using KidsSchool.Controllers;
using KidsSchool.Models;
using KidsSchool.Models.DB;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace KidsSchool.Areas.Admin.Controllers
{
    [Authorize(Roles = "Quản trị viên")]
    public class AspNetUsersController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin/AspNetUsers
        public ActionResult Index()
        {
            var aspNetUsers = db.AspNetUsers.Include(a => a.District);
            return View(aspNetUsers.ToList());
        }
        // GET: Admin/AspNetUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // GET: Admin/AspNetUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AspNetUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = UserManager.Create(user, model.PasswordHash);
                if (result.Succeeded)
                {
                    string sterole = "5fc2e440-d378-44f3-992e-4443191ab4ca";
                    
                    Success("Thêm thành công tài khoản: " + model.Email, true);
                    return RedirectToAction("Index");
                }
                else
                {
                    // Add errors to the model state.
                    ModelState.AddModelError("", result.Errors.First());
                }
            }
            return View(model);
        }
        // GET: Admin/AspNetUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: Admin/AspNetUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(AspNetUser model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            //Xóa tài khoản cũ
        //            db.AspNetUsers.Remove(model);
        //            db.SaveChanges();
        //            //Thêm tài khoản mới
        //            var user = new ApplicationUser
        //            {
        //                UserName = model.Email,
        //                Email = model.Email,
        //            };
        //            var result = UserManager.Create(user, model.PasswordHash);
        //            if (result.Succeeded)
        //            {
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                // Add errors to the model state.
        //                ModelState.AddModelError("", result.Errors.First());
        //            }

        //        }

        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        //}

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AspNetUser model)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var result = await userManager.RemovePasswordAsync(model.Id);
            if (result.Succeeded)
            {
                result = await userManager.AddPasswordAsync(model.Id, model.PasswordHash);
                if (result.Succeeded)
                {
                    Success("Thay đổi thành công tài khoản: " + model.Email, true);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", result.Errors.FirstOrDefault());
                }
            }
            else
            {
                ModelState.AddModelError("", result.Errors.FirstOrDefault());
            }
            return View(model);
        }

        // GET: Admin/AspNetUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }

        // POST: Admin/AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AspNetUser aspNetUser = db.AspNetUsers.Find(id);
            db.AspNetUsers.Remove(aspNetUser);
            db.SaveChanges();
            return RedirectToAction("Index");
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
