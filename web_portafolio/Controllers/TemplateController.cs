using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using web_portafolio.Models;
using web_portafolio.Models.Response;

namespace web_portafolio.Controllers {
    public class TemplateController : BaseController {
        // GET: Tasks
        public ActionResult Create() {
            if (User.Identity.IsAuthenticated) {
                return View(getHomeViewModel());
            } else {
                return RedirectToAction("Login", "Auth");
            }
        }
        // GET: Tasks
        public ActionResult Index() {
            if (User.Identity.IsAuthenticated) {
                return View(getHomeViewModel());
            } else {
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpGet]
        public async Task<JsonResult> getTasksByUserId() {
            List<ListTaskModel> tasks = new List<ListTaskModel>();
            try {
                var identity = getHomeViewModel();
                using (var client = getClient(identity.Token)) {
                    var getTask = client.GetAsync(endPointTask + "/getTasksByUser?id= " + identity.Id);
                    getTask.Wait();

                    HttpResponseMessage response = getTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<List<TaskModel>>>(responseAsString.Result);

                        if (resultRemote.success) {
                            var remoteTasks = resultRemote.data;
                            tasks = remoteTasks.Select(x => new ListTaskModel() {
                                id = x.id,
                                name = x.name,
                                description = x.description,
                                date = ((DateTime)x.dateEnd).ToString(formatDate).Replace("-", "/"),
                                state = x.taskStatusId == "0" ? "Pendiente" : x.taskStatusId == "1" ? "Trabajando" : x.taskStatusId == "2" ? "Realizado" : x.taskStatusId == "3" ? "Rechazado" : ""
                            }).ToList();
                        }
                    }
                }
                return Json(new { isReady = true, list = tasks, msg = "" }, JsonRequestBehavior.AllowGet);
            } catch (Exception e) {
                return Json(new { isReady = false, msg = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> createProcess(Template template) {
            try {
                var identity = getHomeViewModel();
                template.userId = long.Parse(identity.Id);

                foreach (TemplateTask templateTask in template.tasks) {
                    templateTask.userId = long.Parse(identity.Id);
                }

                var json = JsonConvert.SerializeObject(template);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = getClient(identity.Token)) {
                    var postTask = client.PostAsync(endPointTemplate + "/insert", data);
                    postTask.Wait();
                    HttpResponseMessage response = postTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<object>>(responseAsString.Result);

                        if (resultRemote.success) {
                            return Json(new { isReady = true, msg = resultRemote.message }, JsonRequestBehavior.AllowGet);
                        } else {
                            return Json(new { isReady = false, msg = resultRemote.message }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                return Json(new { isReady = true, msg = "" }, JsonRequestBehavior.AllowGet);
            } catch (Exception e) {
                return Json(new { isReady = false, msg = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}