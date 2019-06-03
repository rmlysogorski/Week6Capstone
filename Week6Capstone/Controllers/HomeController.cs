using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Week6Capstone.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (TempData["UserAdded"] != null)
            {
                ViewBag.UserAdded = TempData["UserAdded"];
            }

            if (TempData["LoginError"] != null)
            {
                ViewBag.LoginError = TempData["LoginError"];
            }

            return View();
        }

    }
}