using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web_portafolio.Models.ListModel {
    public class ListReportProcess {
        public int processId { get; set; }
        public String processName { get; set; }
        public List<TaskModel> taskList { get; set; }
        public int tasks { get; set; }
        public int pending { get; set; }
        public int working { get; set; }
        public int done { get; set; }
    }
}