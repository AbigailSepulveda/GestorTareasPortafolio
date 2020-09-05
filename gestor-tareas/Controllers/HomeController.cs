using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using gestor_tareas.Models;
using System.Web.Mvc;


namespace gestor_tareas.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public ActionResult Verify(Usuario user ) {
            View();
        }
    }
}
