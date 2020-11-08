using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_portafolio.Models {
    public class TemplateTask {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime endDate { get; set; }
        public string task_status_code { get; set; }
        public long template_id { get; set; }
        public long userId { get; set; }
    }
}