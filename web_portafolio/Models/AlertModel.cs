using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_portafolio.Models {
    public class AlertModel {
        public decimal id { get; set; }
        public String message { get; set; }
        public int state { get; set; }
        public TaskModel task { get; set; }
    }
}