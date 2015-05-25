using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Inventory.Services;
using InventorySolution.Models;

namespace InventorySolution.Controllers
{
    public class SurseController : Controller
    {
        //
        // GET: /Surse/

        public ActionResult Index()
        {
            SurseViewModel model = new SurseViewModel()
            {
                Surse = SVC.Surse.GetSurse()
                    .Select(s => new SursaModel
                    {
                        Id = s.Id,
                        Nume = s.Nume
                    }).ToList()
            };

            if (model.Surse.Count == 0)
            {
                model.Message = new MessageModel
                {
                    Message = new HtmlString(string.Format("Nu exista surse.\n Adaugati {0}",
                        "aici".ToLink(new UrlHelper(Request.RequestContext).Action("Create", "Surse")))),
                        Icon = MessageIcon.WarningIcon,
                    Type = MessageType.Warning
                };
            }
            return View(model);
        }

        //
        // GET: /Surse/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Surse/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Surse/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Surse/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Surse/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Surse/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Surse/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
