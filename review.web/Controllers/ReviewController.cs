using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace review.web.Controllers {
    [Authorize]
    public class ReviewController : Controller {

        public ActionResult Index() {
            return View();
        }

    }
}