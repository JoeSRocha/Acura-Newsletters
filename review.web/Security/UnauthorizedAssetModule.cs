using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Security;

namespace review.web.Extensions {

    /// <summary>
    /// This module supports inherent url authorization, to get around the way that email client won't try to log in. It works by reading a shared secret from web.config, and hashing it together with the url path. When an incoming unauthenticated request is 
    /// </summary>
    public class UnauthorizedAssetModule : IHttpModule {

        public void Dispose() { }

        public void Init(HttpApplication app) {
            app.PreRequestHandlerExecute += PreRequestHandlerExecute;
        }

        private void PreRequestHandlerExecute(object sender, EventArgs e) {
            var app = sender as HttpApplication;

            //if (!app.Context.Request.IsAuthenticated)
        }
    }
}