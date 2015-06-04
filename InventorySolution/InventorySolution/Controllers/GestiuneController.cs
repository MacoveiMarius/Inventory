using InventorySolution.Models;
using Inventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Core.Domain;

namespace InventorySolution.Controllers
{
    public class GestiuneController : Controller
    {
        private IGestiuni GestiuniService
        {
            get
            {
                return SVC.Gestiuni;
            }
        }

        //
        // GET: /Gestiune/

        public ActionResult Index()
        {
            GestiuneViewModel model = new GestiuneViewModel()
            {
                Gestiune = GestiuniService.GetGestiuni()
                .Select(g => new GestiuneModel
                {
                    GestiuneId = g.Id,
                    Nume = g.Nume,
                    Prenume = g.Prenume,
                    Catedra = g.Catedra
                }).ToList()
            };

            return View(model);
        }
        //
        // GET: /Gestiune/Create

        public ActionResult Create(int? id)
        {
            Gestiune gestiune = null;
            if (id.HasValue)
                gestiune = GestiuniService.GetGestiune(id.Value);

            gestiune = gestiune ?? new Gestiune();
            return PartialView("EditorTemplates/GestiuneEditor", new GestiuneModel()
                {
                    GestiuneId = gestiune.Id,
                    Nume = gestiune.Nume,
                    Prenume = gestiune.Prenume,
                    Catedra = gestiune.Catedra
                });
        }

        //
        // POST: /Gestiune/Create

        [HttpPost]
        public ActionResult AddOrUpdateGestiune(int? id)
        {
            ServiceResult service = new ServiceResult();
            var msg = String.Empty;
            try
            {
                // TODO: Add insert logic here
                int gestiuneId;
                int.TryParse(Request.Form["pk"], out gestiuneId);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Gestiune/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Gestiune/Delete/5

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
