using System;

namespace web_portafolio.Models {
    public class ProcessModel {
        public String id { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public long task_status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime finished { get; set; }
    }
}