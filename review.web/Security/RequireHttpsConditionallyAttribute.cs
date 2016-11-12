using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace review.web {
    public class RequireHttpsConditionallyAttribute : RequireHttpsAttribute {
        public override void OnAuthorization(AuthorizationContext filterContext) {
            if (ConfigurationManager.AppSettings["RequireHttps"] == "false") {
                // when connection to the application is local, don't do any HTTPS stuff
                return;
            }
            base.OnAuthorization(filterContext);
        }
    }
}