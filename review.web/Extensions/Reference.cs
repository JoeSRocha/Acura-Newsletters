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

        public static string MailSenderName {
            get {
                return
                    String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Reference:MailSenderName"]) ?
                        "{ Mail Sender Name }" :
                        ConfigurationManager.AppSettings["Reference:MailSenderName"];
            }
        }

        public static string MailSenderAddress {
            get {
                return
                    String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Reference:MailSenderAddress"]) ?
                        "{ Mail Sender Address }" :
                        ConfigurationManager.AppSettings["Reference:MailSenderAddress"];
            }
        }

        public static string MailSourceDomain {
            get {
                return
                    String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["Reference:MailSourceDomain"]) ?
                        "{ Mail Source Domain }" :
                        ConfigurationManager.AppSettings["Reference:MailSourceDomain"];
            }
        }

    }
}