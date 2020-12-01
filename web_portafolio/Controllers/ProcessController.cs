using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using web_portafolio.Models;
using web_portafolio.Models.ListModel;
using web_portafolio.Models.Request;
using web_portafolio.Models.Response;

namespace web_portafolio.Controllers {
    public class ProcessController : BaseController {
        // GET: Tasks
        public ActionResult Create() {
            if (User.Identity.IsAuthenticated) {
                return View(getHomeViewModel());
            } else {
                return RedirectToAction("Login", "Auth");
            }
        }

        // GET: Tasks
        public ActionResult Backlog() {
            if (User.Identity.IsAuthenticated) {
                return View(getHomeViewModel());
            } else {
                return RedirectToAction("Login", "Auth");
            }
        }

        public ActionResult Alerts() {
            if (User.Identity.IsAuthenticated) {
                return View(getHomeViewModel());
            } else {
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpGet]
        public async Task<JsonResult> getTasksById(decimal id) {
            TaskModel tasks = new TaskModel();
            try {
                var identity = getHomeViewModel();
                using (var client = getClient(identity.Token)) {
                    var getTask = client.GetAsync(endPointTask + "/getByTaskId?id= " + id);
                    getTask.Wait();

                    HttpResponseMessage response = getTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<TaskModel>>(responseAsString.Result);

                        if (resultRemote.success) {
                            tasks = resultRemote.data;
                        }
                    }
                }
                return Json(new { isReady = true, list = tasks, msg = "" }, JsonRequestBehavior.AllowGet);
            } catch (Exception e) {
                return Json(new { isReady = false, msg = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> getTasksByProcessId(decimal id) {
            List<TaskModel> tasks = new List<TaskModel>();
            try {
                var identity = getHomeViewModel();
                using (var client = getClient(identity.Token)) {
                    var getTask = client.GetAsync(endPointTask + "/getTasksByProcessId?id= " + id);
                    getTask.Wait();

                    HttpResponseMessage response = getTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<List<TaskModel>>>(responseAsString.Result);

                        if (resultRemote.success) {
                            tasks = resultRemote.data;
                        }
                    }
                }
                return Json(new { isReady = true, list = tasks, msg = "" }, JsonRequestBehavior.AllowGet);
            } catch (Exception e) {
                return Json(new { isReady = false, msg = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> getProcessByUnit() {
            List<ListProcessModel> tasks = new List<ListProcessModel>();
            try {
                var identity = getHomeViewModel();
                using (var client = getClient(identity.Token)) {
                    var getTask = client.GetAsync(endPointProcess + "/getBacklogByUnit?unit_id= " + identity.Unit);
                    getTask.Wait();

                    HttpResponseMessage response = getTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<List<ListProcessModel>>>(responseAsString.Result);

                        if (resultRemote.success) {
                            tasks = resultRemote.data;
                        }
                    }
                }
                return Json(new { isReady = true, list = tasks, msg = "" }, JsonRequestBehavior.AllowGet);
            } catch (Exception e) {
                return Json(new { isReady = false, msg = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> createProcess(string name, string description, int[] tasks) {
            try {
                var identity = getHomeViewModel();
                CreateProcessRequest request = new CreateProcessRequest();
                request.name = name;
                request.description = description;
                request.user_id = long.Parse(identity.Id);

                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = getClient(identity.Token)) {
                    var postTask = client.PostAsync(endPointProcess + "/createProcess", data);
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

        [HttpPost]
        public async Task<JsonResult> editTask(long id, string estado, decimal asignado) {
            try {
                var identity = getHomeViewModel();
                TaskModel request = new TaskModel();
                request.id = id;
                request.taskStatusId = estado;
                request.assingId = long.Parse(identity.Id);

                var json = JsonConvert.SerializeObject(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = getClient(identity.Token)) {
                    var postTask = client.PostAsync(endPointTask + "/editTask", data);
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