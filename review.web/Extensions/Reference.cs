using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace review.web {
    public static class Reference {

        public static string CompanyName {
            get {
                return
                    String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Reference:CompanyName"]) ?
                        "Rhythm" :
                        ConfigurationManager.AppSettings["Reference:CompanyName"];
            }
        }

        public static string ApplicationName {
            get {
                return
                    String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Reference:ApplicationName"]) ?
                        "Rhythm Email Preview" :
                        ConfigurationManager.AppSettings["Reference:ApplicationName"];
            }
        }

    }
}