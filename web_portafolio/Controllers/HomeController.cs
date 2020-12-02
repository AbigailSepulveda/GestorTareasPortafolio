using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using web_portafolio.Models.ListModel;
using web_portafolio.Models.Response;

namespace web_portafolio.Controllers {
    [Authorize]
    public class HomeController : BaseController {

        public ActionResult Index() {
            if (User.Identity.IsAuthenticated) {
                return View(getHomeViewModel());
            } else {
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpGet]
        public async Task<JsonResult> getReportAlerts() {
            ListReportAlerts task = new ListReportAlerts();
            try {
                var identity = getHomeViewModel();
                using (var client = getClient(identity.Token)) {
                    var getTask = client.GetAsync(endPointTask + "/getReportAlerts?id= " + identity.Id);
                    getTask.Wait();

                    HttpResponseMessage response = getTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<ListReportAlerts>>(responseAsString.Result);

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
        public async Task<JsonResult> getReportTask() {
            ListReportTask task = new ListReportTask();
            try {
                var identity = getHomeViewModel();
                using (var client = getClient(identity.Token)) {
                    var getTask = client.GetAsync(endPointTask + "/getReportTask?id= " + identity.Id);
                    getTask.Wait();

                    HttpResponseMessage response = getTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<ListReportTask>>(responseAsString.Result);

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
        public async Task<JsonResult> getReportProcess() {
            List<ListReportProcess> task = new List<ListReportProcess>();
            try {
                var identity = getHomeViewModel();
                using (var client = getClient(identity.Token)) {
                    var getTask = client.GetAsync(endPointTask + "/getReportProcess?id= " + identity.Unit);
                    getTask.Wait();

                    HttpResponseMessage response = getTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<List<ListReportProcess>>>(responseAsString.Result);

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
        public async Task<JsonResult> getReportUnit() {
            List<ListReportUnit> task = new List<ListReportUnit>();
            try {
                var identity = getHomeViewModel();
                using (var client = getClient(identity.Token)) {
                    var getTask = client.GetAsync(endPointTask + "/getReportUnit?id= " + identity.Unit);
                    getTask.Wait();

                    HttpResponseMessage response = getTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var resultRemote = JsonConvert.DeserializeObject<BaseResponse<List<ListReportUnit>>>(responseAsString.Result);

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
    }
}
