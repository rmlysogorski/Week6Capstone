using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week6Capstone.Models;

namespace Week6Capstone.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginUser(UserLogin thisUser)
        {
            int UserId = DAO.FindUserInDb(thisUser);
            if (UserId != -1)
            {
                Session["UserId"] = UserId;
                return RedirectToAction("../Task/Index");
            }
            else
            {
                TempData["LoginError"] = "Email or password incorrect.";
                return RedirectToAction("../Home/Index");
            }

        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult RegisterNewUser(User newUser)
        {
            bool success;
            if (ModelState.IsValid)
            {
                success = DAO.AddUserToDb(newUser);
                if (success)
                {
                    TempData["UserAdded"] = "You have been successfully registered.";
                }
                else
                {
                    TempData["UserAdded"] = "There is already a user with that e-mail.";
                }
            }
            else
            {
                TempData["UserAdded"] = "Something went wrong.";
            }

            return RedirectToAction("../Home/Index");
        }
    }
}