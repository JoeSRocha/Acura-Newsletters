using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

using review.web.Models;
using System.Text;
using System.Text.RegularExpressions;

namespace review.web {
    public static class Email {

        public static void SendSample(ReviewContentItem source, string to) {
            MailMessage msg = new MailMessage(Reference.MailSender, to);

            msg.Subject = source.subject;

            // replace relative image urls with absolute ones
            StringBuilder content = new StringBuilder(source.GetHtml());
            content.Replace(" src=\"" + source.path, " src=\"" + Reference.MailSourceDomain + "/Content/Review/" + source.path);

            msg.Body = content.ToString();
            msg.IsBodyHtml = true;
            
            using (var client = new SmtpClient()) {
                client.Send(msg);
            }
        }

    }
}