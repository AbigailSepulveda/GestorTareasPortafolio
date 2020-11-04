using System.Web.Mvc;

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
    }
}
