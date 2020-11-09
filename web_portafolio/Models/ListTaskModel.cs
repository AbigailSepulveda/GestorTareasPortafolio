using System;

namespace web_portafolio.Models {
    public class ListTaskModel {
        public long id { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public String date { get; set; }
        public String state { get; set; }
    }
}