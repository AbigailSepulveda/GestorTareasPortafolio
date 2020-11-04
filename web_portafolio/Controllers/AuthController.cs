using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using web_portafolio.Models;
using web_portafolio.Models.Request;
using web_portafolio.Models.Response;

namespace web_portafolio.Controllers {
    [AllowAnonymous]
    public class AuthController : BaseController {

        [HttpGet]
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginView model) {
            if (!ModelState.IsValid) //Checks if input fields have the correct format
            {
                ModelState.AddModelError("", "Datos invalidos");
                return View(model); //Returns the view with the input values so that the user doesn't have to retype again
            }
            try {
                using (var client = getClient()) {
                    var myContent = JsonConvert.SerializeObject(new LoginRequest() { email = model.User, password = encodeTo64(model.Password) });
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var postTask = client.PostAsync(endPointUser + "signIn", byteContent);
                    postTask.Wait();


                    HttpResponseMessage response = postTask.Result;
                    if (response.IsSuccessStatusCode) {
                        response.EnsureSuccessStatusCode();
                        var responseAsString = response.Content.ReadAsStringAsync();
                        responseAsString.Wait();
                        var data = JsonConvert.DeserializeObject<BaseResponse<User>>(responseAsString.Result);



                        if (data.success) {

                            string models = "";

                            foreach (Module module in data.data.rol.modules) {
                                if (models == "") {
                                    models = module.code;
                                } else {
                                    models = models + ", " + module.code;
                                }
                            }

                            var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name, data.data.name),
                            new Claim(ClaimTypes.Email, data.data.email),
                            new Claim(ClaimTypes.Sid, data.data.id+""),
                            new Claim(ClaimTypes.Role, data.data.rol.id+""),
                            new Claim(ClaimTypes.GroupSid, models),
                            new Claim(ClaimTypes.UserData, data.data.unit.id+""),
                            new Claim(ClaimTypes.Authentication, data.data.token_session),
                            new Claim(ClaimTypes.NameIdentifier, data.data.id + "")
                            }, "ApplicationCookie");

                            var ctx = Request.GetOwinContext();
                            var authManager = ctx.Authentication;

                            authManager.SignIn(new AuthenticationProperties() { IsPersistent = model.RememberMe }, identity);

                            return RedirectToAction("Index", "Home");
                        } else {
                            ModelState.AddModelError("", "Usuario o Contraseña incorrectos");
                        }

                    }
                }
            } catch (Exception e) {
                ViewBag.msg = "Error: " + e.Message.ToString();
            }
            return View(model);
        }

        public ActionResult ChangePass() {
            if (User.Identity.IsAuthenticated) {
                return View(getHomeViewModel());
            } else {
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpPost]
        public ActionResult ChangePass(HomeViewModel model) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError("error", "ambos campos son necesarios");
            } else {
                try {
                    if (model.Password == model.RepeatPassword) {
                        /*bool result = DAL.Login.DLogin.changePass(data[1], model.Password);

                        if (result)
                        {
                            ModelState.AddModelError("correct", "contraseña modificada correctamente");
                        }
                        else
                        {
                            ModelState.AddModelError("error", "no se logró modificar la Contraseña");
                        }*/
                        ModelState.AddModelError("error", "no se logró modificar la Contraseña");
                    } else {
                        ModelState.AddModelError("error", "ambos campos deben ser iguales");
                    }
                } catch (Exception e) {
                    ModelState.AddModelError("error", "error, detalle: " + e.ToString());
                }
            }
            return View(getHomeViewModel());
        }

        public ActionResult Logout() {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }

        public static string encodeTo64(string toEncode) {

            byte[] toEncodeAsBytes

                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);

            string returnValue

                  = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;

        }
    }
}