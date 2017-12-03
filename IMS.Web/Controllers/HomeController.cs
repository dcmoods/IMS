using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllStockItems()
        {
            return PartialView("_Stock");
        }

        public ActionResult AddStockItem()
        {
            return PartialView("_AddStock");
        }

        public ActionResult EditStockItem()
        {
            return PartialView("_EditStock");
        }

        public ActionResult ItemEntries()
        {
            return PartialView("_ItemEntries");
        }
    }
}
