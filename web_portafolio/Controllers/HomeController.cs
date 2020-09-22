using SGM_INSPECCION_DIGITAL.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;

namespace web_portafolio.Controllers {
    [Authorize]
    public class HomeController : Controller {
        public string[] getData() {
            var identity = (ClaimsIdentity)User.Identity;
            string[] data = null;
            if (identity != null) {
                IEnumerable<Claim> claims = identity.Claims;
                data = new string[]{
                identity.FindFirst(ClaimTypes.Name).Value,
                identity.FindFirst(ClaimTypes.Role).Value,
                identity.FindFirst(ClaimTypes.Email).Value
            };
            }
            return data;
        }

        public ActionResult Index() {
            if (User.Identity.IsAuthenticated) {
                var data = getData();
                return View(new HomeViewModel {
                    Name = data[0],
                    Type = data[1],
                    Email = data[2]
                });
            } else {
                return RedirectToAction("Login", "Auth");
            }
        }
    }
}
