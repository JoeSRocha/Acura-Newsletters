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
    /// This module supports inherent url authorization, to get around the way that email client 
    /// won't try to log in. It works by reading a shared secret from web.config, and hashing it 
    /// together with the url path. The resulting string is added to the url as a query parameter. 
    /// When an incoming unauthenticated request is recieved, the module 
    /// </summary>
    public class UrlAuthenticationModule : IHttpModule {

        private const string queryKey = "auth";
        private const string algorithmName = "MD5";

        private HashAlgorithm hashAlgorithm;

        public UrlAuthenticationModule() {
            hashAlgorithm = HashAlgorithm.Create(algorithmName);
        }

        public void Dispose() {
            hashAlgorithm.Dispose();
        }

        public void Init(HttpApplication app) {
            app.AuthenticateRequest += AuthenticateRequest;
        }

        private void AuthenticateRequest(object sender, EventArgs e) {
            var app = sender as HttpApplication;

            // if already authenticated, we don't need to do anything, so bail out.
            if (app.Context.Request.IsAuthenticated) return; 

            // if the UrlAuth key isn't configured, then UrlAuth isn't available, so bail out.
            if (String.IsNullOrWhiteSpace(Reference.UrlAuthenticationKey)) return; 

            if (internal_ValidateAutoAuthorizingUrl(app.Context.Request.Url)) {
                var identity = new GenericIdentity("AutoAuthorizedUrl");
                HttpContext.Current.User = new GenericPrincipal(identity, null);
            }
        }

        // private instance utility methods

        /// <summary>
        /// Separate internal validation to leverage the module's instance-local hash algorithm.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private bool internal_ValidateAutoAuthorizingUrl(Uri source) {
            var validKey = GetAuthorizationKey(this.hashAlgorithm, source);
            return String.Equals(validKey, source.GetQueryItems()[queryKey]);
        }

        // private static utility methods

        private static string GetAuthorizationKey(HashAlgorithm hashAlgorithm, Uri source) {
            var hashInput = source.AbsolutePath + "|" + Reference.UrlAuthenticationKey;
            byte[] hash = hashAlgorithm.ComputeHash(System.Text.Encoding.ASCII.GetBytes(hashInput));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++) { sb.Append(hash[i].ToString("X2")); }
            return sb.ToString();
        }

        private static Uri CreateAutoAuthorizingUrl(HashAlgorithm hashAlgorithm, Uri source) {
            var authItem = queryKey + "=" + GetAuthorizationKey(hashAlgorithm, source);
            UriBuilder result = new UriBuilder(source);
            result.Query =
                result.Query.Length > 1 ?
                result.Query.Substring(1) + "&" + authItem :
                authItem;
            return result.Uri;
        }

        // public static methods

        public static bool ValidateAutoAuthorizingUrl(Uri source) {
            using (var staticHashAlgorithm = HashAlgorithm.Create(algorithmName)) {
                var validKey = GetAuthorizationKey(staticHashAlgorithm, source);
                return String.Equals(validKey, source.GetQueryItems()[queryKey]);
            }
        }

        public static Uri GetAutoAuthorizingUrl(Uri source) {
            using (var staticHashAlgorithm = HashAlgorithm.Create(algorithmName)) {
                return CreateAutoAuthorizingUrl(staticHashAlgorithm, source);
            }
        }

        public static string GetAutoAuthorizingUrl(string source) {
            return GetAutoAuthorizingUrl(new Uri(source)).ToString();
        }

        public static IEnumerable<Uri> GetAutoAuthorizingUrls(params Uri[] sources) {
            using (var staticHashAlgorithm = HashAlgorithm.Create(algorithmName)) {
                foreach (var source in sources) {
                    yield return CreateAutoAuthorizingUrl(staticHashAlgorithm, source);
                }
            }
        }

        public static IEnumerable<string> GetAutoAuthorizingUrls(params string[] sources) {
            return GetAutoAuthorizingUrls(sources.Select(u => new Uri(u)).ToArray()).Select(r => r.ToString());
        }

    }
}