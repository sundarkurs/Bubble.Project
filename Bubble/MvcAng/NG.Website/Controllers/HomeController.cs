using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;

namespace NG.Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            var cl = (User as ClaimsPrincipal).Claims;
            return View();

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public void Logout()
        {
            var ctx = Request.GetOwinContext();
            ctx.Authentication.SignOut("Cookies");
            ctx.Authentication.SignOut("oidc");
        }
    }
}