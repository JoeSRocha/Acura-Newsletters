using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using review.web.Models;

namespace review.web {
    public static class Messaging {

        public static void AddMessage(this Controller controller, MessageModel message) {
            if (controller.ViewBag.Messages == null)
                controller.ViewBag.Messages = new List<MessageModel>();
            controller.ViewBag.Messages.Add(message);
        }

        public static void AddMessage(this Controller controller, MessageModel.MessageCategories category, string text) {
            controller.AddMessage(new MessageModel() { Category = category, Text = text });
        }

        public static bool HasMessages(this WebViewPage view) {
            return view.ViewBag.Messages == null || ((List<MessageModel>)view.ViewBag.Messages).Count > 0;
        }

        public static List<MessageModel> Messages(this WebViewPage view) {
            if (view.ViewBag.Messages == null)
                view.ViewBag.Messages = new List<MessageModel>();
            return view.ViewBag.Messages as List<MessageModel>;
        }
    }
}