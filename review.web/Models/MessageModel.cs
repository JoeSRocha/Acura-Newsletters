using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace review.web.Models {
    public class MessageModel {
        public enum MessageCategories { success, info, warning, danger };

        public MessageModel.MessageCategories Category { get; set; }
        public string Text { get; set; }
    }
}