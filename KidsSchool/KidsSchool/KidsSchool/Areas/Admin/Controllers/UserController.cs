using KidsSchool.Controllers;
using KidsSchool.Models.DB;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KidsSchool.Areas.Admin.Controllers
{
    public class UserController : BaseController
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


        // GET: Admin/AspNetUsers/Edit/5
        public ActionResult ChangePassword()
        {
            string userId = HttpContext.User.Identity.GetUserId();
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUser aspNetUser = db.AspNetUsers.Find(userId);
            if (aspNetUser == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUser);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(AspNetUser model)
        {
            string userId = HttpContext.User.Identity.GetUserId();
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var result = await userManager.RemovePasswordAsync(userId);
            if (result.Succeeded)
            {
                result = await userManager.AddPasswordAsync(userId, model.PasswordHash);
                if (result.Succeeded)
                {
                    Success("Thay đổi thành công tài khoản: " + model.Email, true);
                    return RedirectToAction("Index","Home");
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
    }
}
