using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventorySolution.Controllers
{
    using Inventory.Services;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            var calculatoare = SVC.Calculatoare.GetCalculatoare();
            var gestiuni = SVC.Gestiuni.GetGestiuni();
            var laboratoare = SVC.Laboratoare.GetLaboratoare();
            var surse = SVC.Surse.GetSurse();
            var tipuri = SVC.Tipuri.GetTipuri();
            var inventare = SVC.Inventare.GetInventare();

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
