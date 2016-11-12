using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace review.web {
    public static class UriExtensions {

        public static IDictionary<string,string> GetQueryItems(this Uri source) {
            var result = new Dictionary<string, string>();
            if (String.IsNullOrWhiteSpace(source.Query)) return result;
            var content = source.Query.Substring(1).Split('&');
            foreach (var item in content) {
                var pieces = item.Split('=');
                result.Add(pieces[0], pieces[1]);
            }
            return result;
        }

    }
}