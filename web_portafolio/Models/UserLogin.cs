using System;

namespace web_portafolio.Models {
    public class UserLogin {
        public long id { get; set; }
        public String name { get; set; }
        public String email { get; set; }
        public Rol rol { get; set; }
        public Unit unit { get; set; }
        public int state { get; set; }
        public String token_session { get; set; }
        public Enterprise enterprise { get; set; }
    }
}