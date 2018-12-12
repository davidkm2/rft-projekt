﻿using Metin2RFT.Models;
using Metin2RFT.Models.Entities;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Metin2RFT.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        // GET: /Item/Index

        [HttpGet]
        public ActionResult Index()
        {
            using (var db = new MetinEntities())
            {
                var ret = db.Items.ToList();
                return View(ret);
            }
        }

        // POST: Item/Index

        [HttpPost]
        public ActionResult Index(string sort, string search)
        {
            using (var db = new MetinEntities())
            {
                var ret = string.IsNullOrEmpty(search) ? db.Items.AsQueryable() : db.Items.Where(x => x.Name.ToLower().Contains(search.ToLower()));
                switch (sort)
                {
                    default:
                    case "Név":
                        ret = ret.OrderBy(x => x.Name);
                        break;
                    case "Ár növekvő":
                        ret = ret.OrderBy(x => x.Price);
                        break;
                    case "Ár csökkenő":
                        ret = ret.OrderByDescending(x => x.Price);
                        break;
                    case "Kategória":
                        ret = ret.OrderBy(x => x.Category);
                        break;
                }
                return View(ret.ToList());
            }
        }

        // GET: /Item/Details

        [HttpGet]
        public ActionResult Details(int id)
        {
            using (var db = new MetinEntities())
            {
                var ret = db.Items.Single(x => x.Id == id);
                return View(ret);
            }
        }

        // POST: /Item/Details

        [HttpPost]
        public ActionResult Details(Item item)
        {
            using (var db = new MetinEntities())
            {
                item = db.Items.Single(x => x.Id == item.Id);
                var user = db.Accounts.Single(x => x.Id == WebSecurity.CurrentUserId);
                if (user.Balance >= item.Price)
                {
                    user.Items.Add(item);
                    user.Balance -= item.Price;
                    db.SaveChanges();
                    ViewBag.Success = "Sikeres vásárlás!";
                }
                else
                {
                    ModelState.AddModelError("", "Nincs elég egyenlege, hogy megvásárolja ezt a tárgyat. Kérjük töltse fel egyenlegét a profiljában!");
                }
                return View("Details", item);
            }
        }
    }
}