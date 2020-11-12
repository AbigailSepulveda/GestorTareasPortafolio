namespace web_portafolio.Models.ListModel {
    public class ListProcessModel {
        public long id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int n_tasks { get; set; }
        public int task_ready { get; set; }
        public int task_peding { get; set; }
        public int progress { get; set; }
        public string endDate { get; set; }
    }
}