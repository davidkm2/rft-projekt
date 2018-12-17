using Metin2RFT.Models;
using System.Linq;
using System.Web.Mvc;

namespace Metin2RFT.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Index

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Download

        [HttpGet]
        public ActionResult Download()
        {
            return View();
        }

        // GET: /Rankings

        [HttpGet]
        public ActionResult Rankings()
        {
            using (var db = new MetinEntities())
            {
                var ret = db.Players.OrderByDescending(x => x.Level).ToList();
                return View(ret);
            }
        }

        // POST: /Rankings

        [HttpPost]
        public ActionResult Rankings(string rankby, string empire)
        {
            using (var db = new MetinEntities())
            {
                int.TryParse(empire, out int emp);
                var ret = emp == 0 ? db.Players.AsQueryable() : db.Players.Where(x => x.Empire == emp);
                switch (rankby)
                {
                    default:
                    case "Szint":
                        ret = ret.OrderByDescending(x => x.Level);
                        break;
                    case "Arany":
                        ret = ret.OrderByDescending(x => x.Gold);
                        break;
                }
                return View(ret.ToList());
            }
        }
    }
}