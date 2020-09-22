using System;

namespace api_.Models {
    public class User {
        public long id { get; set; }
        public String name { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public long rol_id { get; set; }
        public long unit_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public String token_session { get; set; }
        public long enterprise_id { get; set; }
        public Rol rol { get; set; }
        public Enterprise enterprise { get; set; }
        public Unit unit { get; set; }
        public int state { get; set; }
    }
}