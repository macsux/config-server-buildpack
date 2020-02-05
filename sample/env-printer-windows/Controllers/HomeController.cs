using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace env_printer_windows.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Json(Environment.GetEnvironmentVariables(), JsonRequestBehavior.AllowGet);
        }

    }
}