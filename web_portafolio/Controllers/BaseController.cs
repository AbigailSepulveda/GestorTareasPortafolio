using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;

namespace web_portafolio.Controllers {
    public abstract class BaseController : Controller {

        private readonly String baseURL = "http://192.168.0.165:52834/api/";
        public readonly String endPointUser = "User/";

        // GET: Base
        public ActionResult Index() {
            return View();
        }

        protected HttpClient getClient() {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

    }
}