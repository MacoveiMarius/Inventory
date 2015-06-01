using InventorySolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Core.Domain;
using Inventory.Services;

namespace InventorySolution.Controllers
{
    public class InventareController : Controller
    {
        //
        // GET: /Inventare/

        public ActionResult Index()
        {
            InventareModel model = new InventareModel()
            {
                Inventare = SVC.Inventare.GetInventare(true)
                //.OrderBy(i => i.Denumire).ToList()
                ,
                Message = TempData["InventareMessage"] as MessageModel
            };
            
            return View(model);
        }

        //
        // GET: /Inventare/Details/5

        public ActionResult Details(int id)
        {
            var inventar = SVC.Inventare.GetInventar(id);
            if (inventar == null)
            {
                TempData["InventareMessage"] = new MessageModel
                {
                    Message = new HtmlString(string.Format("Inventarul #{0} nu exista", id)),
                    Type = MessageType.Error,
                    Icon = MessageIcon.ErrorIcon
                };

                return RedirectToAction("Index");
            }

            InventarModel model = new InventarModel()
            {
                InventarId = inventar.Id,
                Tip = inventar.TipEntity ?? new Tip(),
                Denumire = inventar.Denumire,
                Gestiune = inventar.GestiuneEntity ?? new Gestiune(),
                Laborator = inventar.LaboratorEntity ?? new Laborator(),
                Sursa = inventar.SursaEntity ?? new Sursa(),
                Natura = inventar.Natura,

                AnPFun = inventar.AnPFun,
                PVerbal = inventar.PVerbal,
                NrInventar = inventar.NrInventar,
                Serie = inventar.Serie,
                Pret = inventar.Pret,
                Valoare = inventar.Valoare,
                Cantitate = inventar.Cantitate,

                Mentiuni = inventar.Mentiuni,

                Message = TempData["InventareMessage"] as MessageModel
            };
            return View("Inventar", model);
        }

        //
        // GET: /Inventare/Create
        [HttpGet]
        public ActionResult Create(int? gestiuneId)
        {
            var model = new NewInventar
            {
                Message = TempData["InventarMessage"] as MessageModel,
                Inventar = new Inventar(),
                SelectedGestiuneId =  -1,
                Gestiuni = SVC.Gestiuni.GetGestiuni(),
                SelectedLaboratorId =  -1,
                Laboratoare = SVC.Laboratoare.GetLaboratoare(),
                SelectedSursaId = -1,
                Surse = SVC.Surse.GetSurse(),
                SelectedTipId = -1,
                Tipuri = SVC.Tipuri.GetTipuri(),
            };

            if (gestiuneId.HasValue)
            {
                var gestiune = SVC.Gestiuni.GetGestiune(gestiuneId.Value);

            }

            return View();
        }

        //
        // POST: /Inventare/Create

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
        // GET: /Inventare/Edit/5

        public ActionResult Edit(int id)
        {
            var inventar = SVC.Inventare.GetInventar(id);
            if (inventar == null)
            {
                TempData["InventareMessage"] = new MessageModel
                {
                    Message = new HtmlString(string.Format("Inventarul #{0} nu exista", id)),
                    Type = MessageType.Error,
                    Icon = MessageIcon.ErrorIcon
                };

                return RedirectToAction("Index");
            }


            return View();
        }

        //
        // POST: /Inventare/Edit/5

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
        // GET: /Inventare/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Inventare/Delete/5

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