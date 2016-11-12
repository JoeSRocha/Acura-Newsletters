using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using review.web.Models;

namespace review.web {
    public static class ReviewContent {

        private const string CacheKey = "ReviewContentData";

        private static string FilePath {
            get { return AppDomain.CurrentDomain.GetData("DataDirectory").ToString() + "/review-content.json"; }
        }

        private static ReviewContentList GetFromFile() {
            if (!System.IO.File.Exists(FilePath)) return new ReviewContentList();
            var json = System.IO.File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<ReviewContentList>(json);
        }
        public static ReviewContentList Get() {
            var cache = HttpRuntime.Cache;
            if (cache[CacheKey] == null) {
                var content = GetFromFile();
                cache.Add(
                    CacheKey,
                    content,
                    new System.Web.Caching.CacheDependency(FilePath),
                    System.Web.Caching.Cache.NoAbsoluteExpiration,
                    System.Web.Caching.Cache.NoSlidingExpiration,
                    System.Web.Caching.CacheItemPriority.Normal,
                    null
                );
            }
            return cache[CacheKey] as ReviewContentList;
        }
    }
}