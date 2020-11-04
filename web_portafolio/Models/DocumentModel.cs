using System;

namespace web_portafolio.Models {
    public class DocumentModel {
        public decimal id { get; set; }
        public decimal task_id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string path { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}