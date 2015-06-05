using InventorySolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Inventory.Core.Domain;
using Inventory.Core.Domain.Inventare;
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
                Inventare = SVC.Inventare.GetInventare(true).Select(i =>
                    new InventarDataModel
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

            InventarDataModel model = new InventarDataModel()
            {
                InventarId = inventar.Id,
                Denumire = inventar.Denumire,
                //Tip = inventar.TipEntity ?? new Tip(),
                //Gestiune = inventar.GestiuneEntity ?? new Gestiune(),
                //Laborator = inventar.LaboratorEntity ?? new Laborator(),
                //Sursa = inventar.SursaEntity ?? new Sursa(),
                Natura = inventar.Natura.ParseEnum<Natura>(),
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
            else
            {
                model.Message = TempData["InventareMessage"] as MessageModel;
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
            if (ModelState.IsValid)
            {
                try
                {
                    MessageModel message = null;
                    Gestiune gestiune = SVC.Gestiuni.GetGestiune(model.Inventar.SelectedGestiuneId);

                    Inventar inventar = new Inventar
                    {
                        Denumire = model.Inventar.Denumire,
                        AnPFun = model.Inventar.AnPFun,
                        GestiuneId = model.Inventar.SelectedGestiuneId,
                        LaboratorId = model.Inventar.SelectedLaboratorId,
                        PVerbal = model.Inventar.PVerbal,
                        Pret = model.Inventar.Pret,
                        Cantitate = model.Inventar.Cantitate ?? 1,
                        NrInventar = model.Inventar.NrInventar,
                        Serie = model.Inventar.Serie,
                        TipId = model.Inventar.SelectedTipId,
                        Natura = model.Inventar.Natura.ToString(),
                        SursaId = model.Inventar.SelectedSursaId,
                        Mentiuni = model.Inventar.Mentiuni
                    };

                    //verifica daca este sursa CALCULATOARE, daca da adauga calculator
                    ServiceResult serviceResult;
                    var sursaCalc = SVC.Surse.GetSursaByNameInvariant(Sursa.SURSA_CALCULATOARE);
                    if (sursaCalc != null && inventar.SursaId == sursaCalc.Id)
                    {
                        Calculator calculator = new Calculator
                        {
                        };
                        serviceResult = SVC.Inventare.AddInventarWithNewCalculator(inventar, calculator);
                        if (serviceResult.Result == (int) OperationResult.Success)
                        {
                            message = new MessageModel
                            {
                                Message =
                                    new HtmlString(string.Format("Am adaugat inventarul #{0}", serviceResult.EntityId)),
                                Icon = MessageIcon.SuccessIcon,
                                Type = MessageType.Success
                            };
                        }
                    }
                    else
                    {
                        serviceResult = SVC.Inventare.AddInventar(inventar);
                        if (serviceResult.Result == (int) OperationResult.Success)
                        {
                            message = new MessageModel
                            {
                                Message =
                                    new HtmlString(string.Format("Am adaugat inventarul #{0}", serviceResult.EntityId)),
                                Icon = MessageIcon.SuccessIcon,
                                Type = MessageType.Success
                            };
                        }
                    }

                    if (serviceResult.OperationResult != OperationResult.Success) // nu s-au reusit inserarile
                    {
                        string msg = String.Empty;
                        switch (serviceResult.OperationResult)
                        {
                            case OperationResult.ErrorDuplicateItem:
                                msg = string.Format("Exista inventarul #{0}", serviceResult.EntityId);
                                break;
                            default:
                                msg = string.Format("Am intampinat o eroare");
                                break;
                        }
                        message = new MessageModel
                        {
                            Message = new HtmlString(msg),
                            Icon = MessageIcon.ErrorIcon,
                            Type = MessageType.Error
                        };
                    }

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