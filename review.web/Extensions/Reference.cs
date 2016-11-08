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
                        "{ Company Name }" :
                        ConfigurationManager.AppSettings["Reference:CompanyName"];
            }
        }

        public static string ApplicationName {
            get {
                return
                    String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Reference:ApplicationName"]) ?
                        "{ Application Name }" :
                        ConfigurationManager.AppSettings["Reference:ApplicationName"];
            }
        }

        public static string ClientName {
            get {
                return
                    String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Reference:ClientName"]) ?
                        "{ Client Name }" :
                        ConfigurationManager.AppSettings["Reference:ClientName"];
            }
        }

    }
}