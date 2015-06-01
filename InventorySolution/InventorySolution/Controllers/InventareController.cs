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
        #region services

        private IGestiuni GestiuniService
        {
            get { return SVC.Gestiuni; }
        }

        private IInventare InventareService
        {
            get { return SVC.Inventare; }
        }

        private ILaboratoare LaboratoareService
        {
            get { return SVC.Laboratoare; }
        }

        private ISurse SurseService
        {
            get { return SVC.Surse; }
        }

        private ITipuri TipuriService
        {
            get { return SVC.Tipuri; }
        }

        #endregion

        //
        // GET: /Inventare/

        public ActionResult Index()
        {
            InventareModel model = new InventareModel()
            {
                Inventare = SVC.Inventare.GetInventare(true).Select(i =>
                    new InventarModel
                    {
                    }).ToList(),
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
                Denumire = inventar.Denumire,
                //Tip = inventar.TipEntity ?? new Tip(),
                //Gestiune = inventar.GestiuneEntity ?? new Gestiune(),
                //Laborator = inventar.LaboratorEntity ?? new Laborator(),
                //Sursa = inventar.SursaEntity ?? new Sursa(),
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
                Inventar = new InventarModel(),
                Calculator = new CalculatorModel(),
                SelectedGestiuneId = -1,
                SelectedLaboratorId = -1,
                SelectedSursaId = -1,
                SelectedTipId = -1,
            };

            if (gestiuneId.HasValue)
            {
                var gestiune = SVC.Gestiuni.GetGestiune(gestiuneId.Value);
                if (gestiune != null)
                {
                    model.SelectedGestiuneId = gestiune.Id;
                }
            }

            var gestiuni = GestiuniService.GetGestiuni();
            var laboratoare = LaboratoareService.GetLaboratoare();
            var surse = SurseService.GetSurse();
            var tipuri = TipuriService.GetTipuri();
            if (gestiuni == null || gestiuni.Count <= 0
                || laboratoare == null || laboratoare.Count <= 0
                || surse == null || surse.Count <= 0
                || tipuri == null || tipuri.Count <= 0)
            {
                var msg = String.Empty;
                if (gestiuni == null || gestiuni.Count <= 0)
                {
                    msg += string.Format("Nu exista intrari. Vezi {0} <br />",
                        "gestiuni".ToLink(Url.Action("Index", "Gestiuni")));
                }
                if (laboratoare == null || laboratoare.Count <= 0)
                {
                    msg += string.Format("Nu exista intrari. Vezi {0} <br />",
                        "laboratoare".ToLink(Url.Action("Index", "Laboratoare")));
                }
                if (surse == null || surse.Count <= 0)
                {
                    msg += string.Format("Nu exista intrari. Vezi {0} <br />",
                        "Surse".ToLink(Url.Action("Index", "Surse")));
                }
                if (tipuri == null || tipuri.Count <= 0)
                {
                    msg += string.Format("Nu exista intrari. Vezi {0} <br />",
                        "tipuri".ToLink(Url.Action("Index", "Tipuri")));
                }
                model.Message = new MessageModel
                {
                    Message = new HtmlString(msg),
                    Type = MessageType.Warning,
                    Icon = MessageIcon.ErrorIcon
                };
            }
            else
            {
                model.Gestiuni = gestiuni.Select(x => new GestiuneModel
                {
                    GestiuneId = x.Id,
                    Nume = x.Nume,
                    Prenume = x.Prenume
                }).ToList();
                model.Laboratoare = laboratoare.Select(x => new LaboratorModel
                {
                    LaboratorId = x.Id,
                    Nume = x.Nume
                }).ToList();
                model.Surse = surse.Select(x => new SursaModel
                {
                    SursaId = x.Id
                }).ToList();
                model.Tipuri = tipuri.Select(x => new TipModel {}).ToList();
            }
            return View(model);
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