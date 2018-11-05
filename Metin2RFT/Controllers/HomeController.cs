using Metin2RFT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metin2RFT.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TestUser user)
        {
            if (user.Username == "test" && user.Password == "test")
            {
                user.IsLoggedIn = true; ;
                TempData["user"] = user;
                return RedirectToAction("Index");
            }
            TempData["msg"] = "HIBA: Érvénytelen felhasználónév vagy jelszó.";
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var user = TempData["user"] as TestUser;
            if (user == null || !user.IsLoggedIn)
                return RedirectToAction("Login");
            return View();
        }
    }
}