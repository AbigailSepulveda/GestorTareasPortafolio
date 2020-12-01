using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using web_portafolio.Models;
using web_portafolio.Models.Response;

namespace web_portafolio.Controllers {
    public class TasksController : BaseController {
        // GET: Tasks
        public ActionResult Index() {
            if (User.Identity.IsAuthenticated) {
                return View(getHomeViewModel());
            } else {
                return RedirectToAction("Login", "Auth");
            }
        }

        public async Task<List<User>> getUsersByUnitForSelect(string token, string unitId) {
            List<User> users = new List<User>();
            try {
                using (var client = getClient(token)) {
                    var getTask = client.GetAsync(endPointUser + "/getAllByUnit?unit_id= " + unitId);
                    getTask.Wait();

                    HttpResponseMessage response = getTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<List<User>>>(responseAsString.Result);

                        if (resultRemote.success) {
                            users = resultRemote.data;
                        }
                    }
                }
            } catch (Exception e) {
                ViewBag.msg = "Error: " + e.Message.ToString();
            }

            return users;
        }

        public async Task<List<TaskModel>> getTasksByUser(string token, string id) {
            List<TaskModel> tasks = new List<TaskModel>();
            try {
                using (var client = getClient(token)) {
                    var getTask = client.GetAsync(endPointTask + "/getAllByUnit?unit_id= " + id);
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
            } catch (Exception e) {
                ViewBag.msg = "Error: " + e.Message.ToString();
            }

            return tasks;
        }

        public async Task<List<ProcessModel>> getProcessByUser(string token, string id) {
            List<ProcessModel> tasks = new List<ProcessModel>();
            try {
                using (var client = getClient(token)) {
                    var getProcess = client.GetAsync(endPointProcess + "/getProcessByUnit?unit_id= " + id);
                    getProcess.Wait();

                    HttpResponseMessage response = getProcess.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<List<ProcessModel>>>(responseAsString.Result);

                        if (resultRemote.success) {
                            tasks = resultRemote.data;
                        }
                    }
                }
            } catch (Exception e) {
                ViewBag.msg = "Error: " + e.Message.ToString();
            }

            return tasks;
        }

        public ActionResult Workload() {
            if (User.Identity.IsAuthenticated) {
                return View(getHomeViewModel());
            } else {
                return RedirectToAction("Login", "Auth");
            }
        }

        public ActionResult Create() {
            if (User.Identity.IsAuthenticated) {
                return View(getHomeViewModel());
            } else {
                return RedirectToAction("Login", "Auth");
            }
        }

        public ActionResult Rejection() {
            if (User.Identity.IsAuthenticated) {
                return View(getHomeViewModel());
            } else {
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpPost]
        public async Task<JsonResult> createTask(String nombre, String descripcion,
                                                 String responsableId, String process,
                                                 String taskId, String state,
                                                 String start, String end) {
            try {
                var identity = getHomeViewModel();
                string fileP = "";
                string fileName = "";
                string localPath = "";
                string urlPath = System.Web.HttpContext.Current.Server.MapPath("~");
                bool fileExist = false;
                for (int i = 0; i < Request.Files.Count; i++) {
                    var file = Request.Files[i];
                    fileName = Path.GetFileName(file.FileName);
                    localPath = ConfigurationManager.AppSettings["LOCAL_PATH_TEMP"].ToString();

                    bool exists = Directory.Exists(Server.MapPath(localPath));

                    if (!exists) {
                        Directory.CreateDirectory(Server.MapPath(localPath));
                    }

                    fileP = Path.Combine((urlPath + localPath), fileName);
                    file.SaveAs(fileP);
                    fileExist = true;
                }

                TaskModel taskModel = new TaskModel();
                taskModel.dateEnd = DateTime.Parse(end);
                taskModel.name = nombre;
                taskModel.description = descripcion;
                taskModel.fatherTaksId = int.Parse(taskId);
                taskModel.taskStatusId = state;
                taskModel.processId = long.Parse(process);
                taskModel.assingId = long.Parse(responsableId);
                if (fileExist) {
                    DocumentModel document = new DocumentModel();
                    document.name = fileName;
                    document.url = ConfigurationManager.AppSettings["PUBLIC_PATH_TEMP"].ToString();
                    document.path = localPath;
                    taskModel.document = document;
                }
                taskModel.creatorUserId = int.Parse(identity.Id);

                var json = JsonConvert.SerializeObject(taskModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = getClient(identity.Token)) {
                    var postTask = client.PostAsync(endPointTask + "/createTask", data);
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

        [HttpGet]
        public async Task<JsonResult> getAssignTasksByUser() {
            List<TaskModel> task = new List<TaskModel>();
            try {
                var identity = getHomeViewModel();
                using (var client = getClient(identity.Token)) {
                    var getTask = client.GetAsync(endPointTask + "/getAssignTasksByUser?id= " + identity.Id);
                    getTask.Wait();

                    HttpResponseMessage response = getTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<List<TaskModel>>>(responseAsString.Result);

                        if (resultRemote.success) {
                            var remoteTasks = resultRemote.data;
                            task = remoteTasks;
                        }
                    }
                }
                return Json(new { isReady = true, list = task, msg = "" }, JsonRequestBehavior.AllowGet);
            } catch (Exception e) {
                return Json(new { isReady = false, msg = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> getRejectionTasksByCreator() {
            List<AlertModel> task = new List<AlertModel>();
            try {
                var identity = getHomeViewModel();
                using (var client = getClient(identity.Token)) {
                    var getTask = client.GetAsync(endPointTask + "/getRejectionTasksByCreator?id= " + identity.Id);
                    getTask.Wait();

                    HttpResponseMessage response = getTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<List<AlertModel>>>(responseAsString.Result);

                        if (resultRemote.success) {
                            var remoteTasks = resultRemote.data;
                            task = remoteTasks;
                        }
                    }
                }
                return Json(new { isReady = true, list = task, msg = "" }, JsonRequestBehavior.AllowGet);
            } catch (Exception e) {
                return Json(new { isReady = false, msg = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> getTaskRed() {
            List<TaskModel> task = new List<TaskModel>();
            try {
                var identity = getHomeViewModel();
                using (var client = getClient(identity.Token)) {
                    var getTask = client.GetAsync(endPointTask + "/getTaskRed?unit_id= " + identity.Unit);
                    getTask.Wait();

                    HttpResponseMessage response = getTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<List<TaskModel>>>(responseAsString.Result);

                        if (resultRemote.success) {
                            var remoteTasks = resultRemote.data;
                            task = remoteTasks;
                        }
                    }
                }
                return Json(new { isReady = true, list = task, msg = "" }, JsonRequestBehavior.AllowGet);
            } catch (Exception e) {
                return Json(new { isReady = false, msg = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> getTaskYellow() {
            List<TaskModel> task = new List<TaskModel>();
            try {
                var identity = getHomeViewModel();
                using (var client = getClient(identity.Token)) {
                    var getTask = client.GetAsync(endPointTask + "/getTaskYellow?unit_id= " + identity.Unit);
                    getTask.Wait();

                    HttpResponseMessage response = getTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<List<TaskModel>>>(responseAsString.Result);

                        if (resultRemote.success) {
                            var remoteTasks = resultRemote.data;
                            task = remoteTasks;
                        }
                    }
                }
                return Json(new { isReady = true, list = task, msg = "" }, JsonRequestBehavior.AllowGet);
            } catch (Exception e) {
                return Json(new { isReady = false, msg = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<JsonResult> getTaskGreen() {
            List<TaskModel> task = new List<TaskModel>();
            try {
                var identity = getHomeViewModel();
                using (var client = getClient(identity.Token)) {
                    var getTask = client.GetAsync(endPointTask + "/getTaskGreen?unit_id= " + identity.Unit);
                    getTask.Wait();

                    HttpResponseMessage response = getTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<List<TaskModel>>>(responseAsString.Result);

                        if (resultRemote.success) {
                            var remoteTasks = resultRemote.data;
                            task = remoteTasks;
                        }
                    }
                }
                return Json(new { isReady = true, list = task, msg = "" }, JsonRequestBehavior.AllowGet);
            } catch (Exception e) {
                return Json(new { isReady = false, msg = e.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> assignTask(long id, long asignado) {
            try {
                var identity = getHomeViewModel();

                TaskModel taskModel = new TaskModel();
                taskModel.id = id;
                taskModel.assingId = asignado;

                var json = JsonConvert.SerializeObject(taskModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = getClient(identity.Token)) {
                    var postTask = client.PostAsync(endPointTask + "/assignTask", data);
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
        public async Task<JsonResult> refuseTask(long id, String message) {
            try {
                var identity = getHomeViewModel();

                TaskModel taskModel = new TaskModel();
                taskModel.id = id;
                taskModel.description = message;
                taskModel.creatorUserId = long.Parse(identity.Id + "");

                var json = JsonConvert.SerializeObject(taskModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = getClient(identity.Token)) {
                    var postTask = client.PostAsync(endPointTask + "/refuseTask", data);
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
        public async Task<JsonResult> acceptTask(long id) {
            try {
                var identity = getHomeViewModel();

                TaskModel taskModel = new TaskModel();
                taskModel.id = id;
                taskModel.creatorUserId = long.Parse(identity.Id + "");

                var json = JsonConvert.SerializeObject(taskModel);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = getClient(identity.Token)) {
                    var postTask = client.PostAsync(endPointTask + "/acceptTask", data);
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