using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace thewall9.customer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [Route("app")]
        public ActionResult App()
        {
            return View();
        }
    }
}