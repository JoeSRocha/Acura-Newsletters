using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace review.web.Controllers {
    [RequireHttpsConditionally]
    [Authorize]
    public class ReviewController : Controller {

        public ActionResult Index(string id) {
            if (String.IsNullOrWhiteSpace(id)) return HttpNotFound();
            var model = ReviewContent.Get().FirstOrDefault(c => String.Equals(c.path, id));
            if (model == null) return HttpNotFound();
            else return View(model);
        }

        public ActionResult Send(string id, string to) {
            if (String.IsNullOrWhiteSpace(id)) return HttpNotFound();
            var model = ReviewContent.Get().FirstOrDefault(c => String.Equals(c.path, id));
            if (model == null) return HttpNotFound();
            else {
                try {
                    Email.SendSample(model, to);
                    return Json(new { success = true });
                }
                catch (Exception ex) {
                    return Json(new { success = false });
                }
            }
        }

    }
}