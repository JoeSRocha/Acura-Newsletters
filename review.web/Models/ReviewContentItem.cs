using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace review.web.Models {
    public class ReviewContentItem {
        public static readonly string FilePathBase = AppDomain.CurrentDomain.BaseDirectory + @"\Content\Review\";

        public string name { get; set; }
        public string path { get; set; }
        public string subject { get; set; }
        public int width { get; set; }

        public string LocalFilePath {
            get { return FilePathBase + this.path + ".html"; }
        }

        public string LocalAssetPath {
            get { return FilePathBase + this.path + @"\"; }
        }

        public string GetHtml() {
            if (!System.IO.File.Exists(this.LocalFilePath)) throw new Exception("GetHtml() failed; source file not found.");
            else return System.IO.File.ReadAllText(this.LocalFilePath);
        }
    }
}