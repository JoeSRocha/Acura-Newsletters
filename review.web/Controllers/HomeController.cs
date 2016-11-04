using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using review.web.Models;

namespace review.web.Controllers {
    public class HomeController : Controller {

        [Authorize]
        public ActionResult Index() {
            return View();
        }

        public ActionResult LogIn(string result) {
            if (this.User.Identity.IsAuthenticated) return RedirectToAction("Index");

            if (result == "failed")
                this.AddMessage(MessageModel.MessageCategories.danger, "Login failed");

            ViewBag.LoginAttempt = new LoginModel();
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel model) {
            if (FormsAuthentication.Authenticate(model.Username, model.Password)) {
                FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                return RedirectToAction("Index");
            } else {
                return RedirectToAction("LogIn", new { result = "failed" });
            }
        }

        public ActionResult LogOut() {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");
        }

    }
}