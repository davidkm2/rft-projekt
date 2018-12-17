using Metin2RFT.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Metin2RFT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: /Admin/Account

        [HttpGet]
        public ActionResult Account()
        {
            using (var db = new MetinEntities())
            {
                var accs = db.Accounts.AsQueryable();
                var ret = new List<AccountModel>();
                foreach (var acc in accs)
                {
                    ret.Add(new AccountModel
                    {
                        Id = acc.Id,
                        Username = acc.Username,
                        IsBanned = Roles.IsUserInRole(acc.Username, "Banned")
                    });
                }
                return this.View(ret);
            }
        }

        // POST: /Admin/Account

        [HttpPost]
        public ActionResult Account(string search)
        {
            using (var db = new MetinEntities())
            {
                var accs = string.IsNullOrEmpty(search) ? db.Accounts.AsQueryable() : db.Accounts.Where(x => x.Username.ToLower().Contains(search.ToLower()));
                var ret = new List<AccountModel>();
                foreach (var acc in accs)
                {
                    if(acc.Username.Contains(search))
                    ret.Add(new AccountModel
                    {
                        Id = acc.Id,
                        Username = acc.Username,
                        IsBanned = Roles.IsUserInRole(acc.Username, "Banned")
                    });
                }
                return View(ret);
            }
        }

        // GET: /Admin/Account/id

        [HttpGet]
        public ActionResult AccountDetails(int id, bool? ban)
        {
            if (ban.HasValue)
            {
                this.ViewBag.Success = ban.Value ? "A fiók le lett tiltva." : "A fiók engedélyezve lett.";
            }
            using (var db = new MetinEntities())
            {
                var acc = db.Accounts.SingleOrDefault(x => x.Id == id);
                var ret = new AccountModel
                {
                    Id = acc.Id,
                    Username = acc.Username,
                    Email = acc.Email,
                    Balance = acc.Balance,
                    IsBanned = Roles.IsUserInRole(acc.Username, "Banned"),
                    Players = acc.Players.ToList()
                };
                return View(ret);
            }
        }

        // POST: /Admin/Ban

        [HttpPost]
        public ActionResult Ban(int accId)
        {
            using (var db = new MetinEntities())
            {
                var acc = db.Accounts.SingleOrDefault(x => x.Id == accId);
                Roles.RemoveUserFromRole(acc.Username, "User");
                Roles.AddUserToRole(acc.Username, "Banned");
            }
            return RedirectToAction("AccountDetails", new { id = accId, ban = true });
        }

        // POST: /Admin/Unban

        [HttpPost]
        public ActionResult Unban(int accId)
        {
            using (var db = new MetinEntities())
            {
                var acc = db.Accounts.SingleOrDefault(x => x.Id == accId);
                Roles.RemoveUserFromRole(acc.Username, "Banned");
                Roles.AddUserToRole(acc.Username, "User");
            }
            return RedirectToAction("AccountDetails", new { id = accId, ban = false });
        }

        // GET: /Admin/RenamePlayer

        [HttpGet]
        public ActionResult RenamePlayer(int id)
        {
            using (var db = new MetinEntities())
            {
                var player = db.Players.SingleOrDefault(x => x.Id == id);
                var ret = new RenamePlayerModel { Player = player };
                return View(ret);
            }
        }

        // POST: /Admin/RenamePlayer

        [HttpPost]
        public ActionResult RenamePlayer(RenamePlayerModel model)
        {
            using (var db = new MetinEntities())
            {
                var player = db.Players.SingleOrDefault(x => x.Id == model.Player.Id);
                player.Name = model.NewName;
                db.SaveChanges();
                return RedirectToAction("AccountDetails", new { id = player.Account.Id });
            }
        }
    }
}