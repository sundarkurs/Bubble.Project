using NG.Website.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NG.Website.Controllers
{
    [CustomAuthorize("CustomerService", "Customer")]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetUsers()
        {
            var items = new List<string>();
            items.Add("Sundar");
            items.Add("Krishna");
            items.Add("Urs");

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUser(string name)
        {
            return Json(name);
        }
    }
}