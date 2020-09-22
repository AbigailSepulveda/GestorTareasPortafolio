using System;

namespace web_portafolio.Models.Response {
    public class BaseResponse<T> {

        public bool success { get; set; }
        public String message { get; set; }
        public T data { get; set; }
    }
}