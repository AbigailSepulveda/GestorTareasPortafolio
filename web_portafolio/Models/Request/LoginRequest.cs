using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_portafolio.Models.Request {
    public class LoginRequest {
        public String email { get; set; }
        public String password { get; set; }
    }
}