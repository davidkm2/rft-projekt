using Metin2RFT.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace Metin2RFT.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        // GET: /Account/Login

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, model.RememberMe))
            {
                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", "Helytelen felhasználónév vagy jelszó.");
            return View(model);
        }

        // GET: /Account/LogOff

        [HttpGet]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { model.Email, model.DeleteCode, Balance = 5000 });
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            return View(model);
        }

        // GET: /Account/ChangePassword

        public ActionResult ChangePassword(bool? success)
        {
            if (success.HasValue && success.Value)
            {
                ViewBag.StatusMessage = "Sikeres jelszóváltoztatás.";
            }
            return View();
        }

        // POST: /Account/ChangePassword

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                bool changePasswordSucceeded;
                try
                {
                    changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                {
                    return RedirectToAction("ChangePassword", new { success = true });
                }
                else
                {
                    ModelState.AddModelError("", "A régi jelszó nem megfelelő vagy az új jelszó érvénytelen.");
                }
            }
            return View(model);
        }

        // GET: /Account/Manage

        [HttpGet]
        public ActionResult Manage()
        {
            using (var db = new MetinEntities())
            {
                var acc = db.Accounts.Single(x => x.Id == WebSecurity.CurrentUserId);
                var ret = new AccountManageModel
                {
                    Account = acc,
                    Players = acc.Players.ToList(),
                    Items = acc.Items.ToList()
                };
                return View(ret);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "A megadott felhasználónévvel már regisztráltak.";
                case MembershipCreateStatus.DuplicateEmail:
                    return "A megadott az e-mail címmel már regisztráltak.";
                case MembershipCreateStatus.InvalidPassword:
                    return "A megadott jelszó érvénytelen.";
                case MembershipCreateStatus.InvalidEmail:
                    return "A megadott e-mai cím érvénytelen.";
                case MembershipCreateStatus.InvalidUserName:
                    return "A megadott felhasználónév érvénytelen.";
                default:
                    return "Ismeretlen hiba történt. Kérjük ellenőrizzen mindent adatot, hogy helyes-e és próbálkozzon újra.";
            }
        }
    }
}