using System.Collections.Generic;

namespace api_.Models {
    public class Rol {
        public long id { get; set; }
        public string name { get; set; }
        public int state { get; set; }
        public List<Module> modules { get; set; }
    }
}