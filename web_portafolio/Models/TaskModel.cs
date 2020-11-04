using System;

namespace web_portafolio.Models {
    public class TaskModel {
        public long id { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public long creatorUserId { get; set; }
        public User creatorUser { get; set; }
        public String dateStart { get; set; }
        public String dateEnd { get; set; }
        public String taskStatusId { get; set; }
        public TaskStatus taskStatus { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public long fatherTaksId { get; set; }
        public long processId { get; set; }
        public ProcessModel process { get; set; }
        public String path { get; set; }
    }
}