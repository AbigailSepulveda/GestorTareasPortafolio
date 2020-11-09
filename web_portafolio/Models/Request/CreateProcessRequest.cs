namespace web_portafolio.Models.Request {
    public class CreateProcessRequest {
        public string name { get; set; }
        public string description { get; set; }
        public int[] tasks { get; set; }
        public long user_id { get; set; }
    }
}