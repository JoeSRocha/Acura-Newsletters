using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace review.web.Controllers {
    [Authorize]
    public class ReviewController : Controller {

        public ActionResult Index(string id) {
            if (String.IsNullOrWhiteSpace(id)) return HttpNotFound();
            var model = ReviewContent.Get().FirstOrDefault(c => String.Equals(c.path, id));
            if (model == null) return HttpNotFound();
            else return View(model);
        }

    }
}