using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_portafolio.Models {
    public class Template {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int state { get; set; }
        public List<TemplateTask> tasks { get; set; }
        public long userId { get; set; }
    }
}