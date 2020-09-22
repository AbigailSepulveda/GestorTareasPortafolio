using api_.Models;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using SGM_INSPECCION_DIGITAL.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
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
                    var myContent = JsonConvert.SerializeObject(new LoginRequest() { email = model.User, password = model.Password });
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
                            var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name, data.data.name),
                            new Claim(ClaimTypes.Email, data.data.email),
                            new Claim(ClaimTypes.Sid, data.data.id+""),
                            new Claim(ClaimTypes.Role, data.data.rol.id+"")
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

        public string[] getData() {
            var identity = (ClaimsIdentity)User.Identity;
            string[] data = null;
            if (identity != null) {
                IEnumerable<Claim> claims = identity.Claims;
                data = new string[]{
                identity.FindFirst(ClaimTypes.Name).Value,
                identity.FindFirst(ClaimTypes.SerialNumber).Value,
                identity.FindFirst(ClaimTypes.Role).Value,
                identity.FindFirst(ClaimTypes.Email).Value
            };
            }
            return data;
        }

        public ActionResult ChangePass() {
            if (User.Identity.IsAuthenticated) {
                var data = getData();
                return View(new HomeViewModel {
                    Name = data[0],
                    Rut = data[1],
                    Type = data[2],
                    Email = data[3]
                });
            } else {
                return RedirectToAction("Login", "Auth");
            }
        }

        [HttpPost]
        public ActionResult ChangePass(HomeViewModel model) {
            var data = getData();

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
            return View(new HomeViewModel {
                Name = data[0],
                Rut = data[1],
                Type = data[2],
                Email = data[3]
            });
        }

        public ActionResult Logout() {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }
    }
}