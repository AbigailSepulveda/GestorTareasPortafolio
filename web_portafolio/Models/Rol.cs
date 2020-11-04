using System.Collections.Generic;

namespace web_portafolio.Models {
    public class Rol {
        public long id { get; set; }
        public string name { get; set; }
        public int state { get; set; }
        public List<Module> modules { get; set; }
    }
}