using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace review.web {
    public static class Reference {

        private static string GetFromConfigOrDefault(string key, string defaultValue) {
            return String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[key]) ? defaultValue : ConfigurationManager.AppSettings[key];
        }

        public static string CompanyName {
            get { return GetFromConfigOrDefault("Reference:CompanyName", "{ Company Name }"); }
        }

        public static string ApplicationName {
            get { return GetFromConfigOrDefault("Reference:ApplicationName", "{ Application Name }"); }
        }

        public static string ClientName {
            get { return GetFromConfigOrDefault("Reference:ClientName", "{ Client Name }"); }
        }

        public static string MailSenderName {
            get { return GetFromConfigOrDefault("Reference:MailSenderName", "{ Mail Sender Name }"); }
        }

        public static string MailSenderAddress {
            get { return GetFromConfigOrDefault("Reference:MailSenderAddress", "{ Mail Sender Address }"); }
        }

        public static string MailSourceDomain {
            get { return GetFromConfigOrDefault("Reference:MailSourceDomain", "{ Mail Source Domain }"); }
        }

        public static string StandardReviewAddress {
            get { return GetFromConfigOrDefault("Reference:StandardReviewAddress", "{ Mail Autoprocess Address }"); }
        }

    }
}