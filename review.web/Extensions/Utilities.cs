using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace review.web {
    public static class Utilities {

        public static bool IsLoginPage(this WebViewPage view) {
            return String.Equals(view.Context.Request.Url.AbsolutePath.ToLower(), FormsAuthentication.LoginUrl.ToLower());
        }

    }
}