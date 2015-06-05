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
    public class GestiuniController : Controller
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
                var nume = Request.Form["value[nume]"];
                var prenume = Request.Form["value[prenume]"];
                var catedra = Request.Form["value[catedra]"];

                var gestiune = new Gestiune
                {
                    Id = gestiuneId,
                    Nume = nume,
                    Prenume = prenume,
                    Catedra = catedra
                };

                if (gestiuneId > 0)
                {//update
                    service = GestiuniService.UpdateGestiune(gestiune);
                    msg = string.Format("gestiune #{0} actualizata cu succes", gestiuneId);
                }
                else
                {//adaugare
                    service = GestiuniService.AddGestiune(gestiune);
                    msg = string.Format("gestiune #{0} adaugata cu succes", gestiuneId);
                }
                //return RedirectToAction("Index");
            }
            catch
            {
                msg = string.Format("am intampinat o problema");
                //return View();
            }
            if (service.OperationResult < OperationResult.Success)
            {
                msg = string.Format("Am intampitan o eroarea");
            }
            return Json(new { Id = service.EntityId, Message = msg }, JsonRequestBehavior.DenyGet);
        }

        //
        // GET: /Gestiune/Delete/5
 
        public ActionResult Delete(int id)
        {
            GestiuniService.DeleteGestiune(id);
            return RedirectToAction("Index");
        }
    }
}
