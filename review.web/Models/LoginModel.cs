using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace review.web.Models {
    public class LoginModel {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}