using InventorySolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                Inventare = SVC.Inventare.GetInventare(true).Select(inventar =>
                    new InventarDataModel
                    {
                        InventarId = inventar.Id,
                        Denumire = inventar.Denumire,
                        Tip = inventar.TipEntity ?? new Tip(),
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

            Tip tip = SVC.Tipuri.GetTip(inventar.TipId.Value) ?? new Tip();
            
            InventarDataModel inventarDetails = new InventarDataModel()
            {
                InventarId = inventar.Id,
                Denumire = inventar.Denumire,
                Tip = inventar.TipEntity ?? new Tip(),
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
            //var sursaCalc = SVC.Surse.GetSursaByNameInvariant(Sursa.SURSA_CALCULATOARE);
            //if (sursaCalc != null && inventar.SursaId == sursaCalc.Id)
            //{
                var calculator = SVC.Calculatoare.GetCalculator(id);
                if (calculator == null)
                {
                    TempData["calculatorMessage"] = new MessageModel
                    {
                        Message = new HtmlString(string.Format("Calculatorul #{0} nu exista", id)),
                        Type = MessageType.Error,
                        Icon = MessageIcon.ErrorIcon
                    };
                    return RedirectToAction("Index");
                }
            //}

            CalculatorModel calculatorDetails = new CalculatorModel
            {
                CalculatorId=calculator.Id,
                Procesor=calculator.Procesor,
                Frecventa=calculator.Frecventa,
                Hdd=calculator.Hdd,
                Ram=calculator.Ram,
                CdRom=calculator.CdRom,
                Floppy=calculator.Floppy,
                Zipp=calculator.Zipp,
                Monitor=calculator.Monitor,
                Tastatura=calculator.Tastatura,
                Accesorii=calculator.Accesorii,
                Mentiuni=calculator.Mentiuni,
                Mouse=calculator.Mouse,
                PlacaBaza=calculator.PlacaBaza,
                PlacaVideo=calculator.PlacaVideo,
                CdW=calculator.CdW,
                CdWR=calculator.CdWR,
                Dvd=calculator.Dvd,
                PlacaRetea=calculator.PlacaRetea,
                TvTuner=calculator.TvTuner,
                Modem=calculator.Modem,
                Message=TempData["calculatorMessage"] as MessageModel
            };

            InventarModel model = new InventarModel
            {
                Inventar = inventarDetails,
                Calculator = calculatorDetails,
                Message = TempData["InventareMessage"] as MessageModel
            };
            return View("Inventar", model);
        }

        //
        // GET: /Inventare/Create
        [HttpGet]
        public ActionResult Create(int? gestiuneId)
        {
            var gestiuni = SVC.Gestiuni.GetGestiuni();
            var laboratoare = SVC.Laboratoare.GetLaboratoare();
            var surse = SVC.Surse.GetSurse();
            var tipuri = SVC.Tipuri.GetTipuri();
            
            var model = new NewInventarModel
            {
                Message = TempData["InventarMessage"] as MessageModel,
                Inventar = new NewInventarDataModel()
                {
                    SelectedGestiuneId = -1,
                    Gestiuni = gestiuni,
                    SelectedLaboratorId = -1,
                    Laboratoare = laboratoare,
                    SelectedSursaId = -1,
                    Surse = surse,
                    SelectedTipId = -1,
                    Tipuri = tipuri
                },
                Calculator = new CalculatorModel(),
            };

            if (gestiuni == null || gestiuni.Count <= 0
                || laboratoare == null || laboratoare.Count <= 0
                || surse == null || surse.Count <= 0
                || tipuri == null || tipuri.Count <= 0)
            {
                StringBuilder msg = new StringBuilder();
                if (gestiuni == null || gestiuni.Count <= 0)
                {
                    msg.AppendFormat("Nu exista gestiuni.");
                }
                if (laboratoare == null || laboratoare.Count <= 0)
                {
                    msg.AppendFormat("Nu exista laboratoare.");
                }

                if (surse == null || surse.Count <= 0)
                {
                    msg.AppendFormat("Nu exista surse.");
                }

                if (tipuri == null || tipuri.Count <= 0)
                {
                    msg.AppendFormat("Nu exista tipuri.");
                }

                model.Message = new MessageModel
                {
                    Message = new HtmlString(msg.ToString()),
                    Type = MessageType.Warning,
                    Icon = MessageIcon.ErrorIcon
                };
            }

            if (gestiuneId.HasValue)
            {
                var gestiune = SVC.Gestiuni.GetGestiune(gestiuneId.Value);
            }

            return View(model);
        }

        //
        // POST: /Inventare/Create

        [HttpPost]
        public ActionResult Create(NewInventarModel model, string save, string saveAndNew)
        {
            var error = new System.Collections.ArrayList();
            for (int i = 0; i < ModelState.Values.Count; i++)
            {
                if (ModelState.Values.ElementAt(i).Errors.Count > 0)
                {
                    error.Add( ModelState.ElementAt(i));
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add insert logic here
                    MessageModel message = null;

                    TempData["InventareMessage"] = message;

                    if (save != null)
                    {
                        return RedirectToAction("Details", new {id = model.Inventar.InventarId});
                    }
                    else if (saveAndNew != null)
                    {
                        return RedirectToActionPermanent("Create");
                    }
                    else
                    {
                        return RedirectToActionPermanent("Index");
                    }
                }
                catch
                {
                    TempData["InventareMessage"] = new MessageModel
                    {
                        Message = new HtmlString("Am intalnit o eroare"),
                        Type = MessageType.Error,
                        Icon = MessageIcon.ErrorIcon
                    };
                }
            }

            #region repopulare cu date

            var gestiuni = SVC.Gestiuni.GetGestiuni();
            var laboratoare = SVC.Laboratoare.GetLaboratoare();
            var surse = SVC.Surse.GetSurse();
            var tipuri = SVC.Tipuri.GetTipuri();

            if (gestiuni == null || gestiuni.Count <= 0
                || laboratoare == null || laboratoare.Count <= 0
                || surse == null || surse.Count <= 0
                || tipuri == null || tipuri.Count <= 0)
            {
                StringBuilder msg = new StringBuilder();
                if (gestiuni == null || gestiuni.Count <= 0)
                {
                    msg.AppendFormat("Nu exista gestiuni.");
                }
                if (laboratoare == null || laboratoare.Count <= 0)
                {
                    msg.AppendFormat("Nu exista laboratoare.");
                }

                if (surse == null || surse.Count <= 0)
                {
                    msg.AppendFormat("Nu exista surse.");
                }

                if (tipuri == null || tipuri.Count <= 0)
                {
                    msg.AppendFormat("Nu exista tipuri.");
                }

                model.Message = new MessageModel
                {
                    Message = new HtmlString(msg.ToString()),
                    Type = MessageType.Warning,
                    Icon = MessageIcon.ErrorIcon
                };
            }
            else
            {
                model.Inventar.Gestiuni = gestiuni;
                model.Inventar.Laboratoare = laboratoare;
                model.Inventar.Surse = surse;
                model.Inventar.Tipuri = tipuri;
            }

            #endregion

            return View(model);
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