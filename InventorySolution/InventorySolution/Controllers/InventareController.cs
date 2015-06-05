using InventorySolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Inventory.Core.Domain;
using Inventory.Services;
using Inventory.Core.Domain.Inventare;

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
                        Natura = inventar.Natura.ParseEnum<Natura>(),
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
            Calculator calculator = null;
            CalculatorModel calculatorDetails = null;
            var sursaCalc = SVC.Surse.GetSursaByNameInvariant(Sursa.SURSA_CALCULATOARE);
            if (sursaCalc != null && inventar.SursaId == sursaCalc.Id)
            {
                calculator = SVC.Calculatoare.GetCalculator(id);
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
                else
                {

                    calculatorDetails = new CalculatorModel
                    {
                        CalculatorId = calculator.Id,
                        Procesor = calculator.Procesor,
                        Frecventa = calculator.Frecventa,
                        Hdd = calculator.Hdd,
                        Ram = calculator.Ram,
                        CdRom = calculator.CdRom,
                        Floppy = calculator.Floppy,
                        Zipp = calculator.Zipp,
                        Monitor = calculator.Monitor,
                        Tastatura = calculator.Tastatura,
                        Accesorii = calculator.Accesorii,
                        Mentiuni = calculator.Mentiuni,
                        Mouse = calculator.Mouse,
                        PlacaBaza = calculator.PlacaBaza,
                        PlacaVideo = calculator.PlacaVideo,
                        CdW = calculator.CdW,
                        CdWR = calculator.CdWR,
                        Dvd = calculator.Dvd,
                        PlacaRetea = calculator.PlacaRetea,
                        TvTuner = calculator.TvTuner,
                        Modem = calculator.Modem,
                        Message = TempData["calculatorMessage"] as MessageModel
                    };
                }
            }

            InventarModel model = new InventarModel
            {
                Inventar = inventarDetails,
                Calculator = calculatorDetails ?? new CalculatorModel(),
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
                        if (serviceResult.Result == (int)OperationResult.Success)
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
                        if (serviceResult.Result == (int)OperationResult.Success)
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
                        return RedirectToAction("Details", new { id = model.Inventar.InventarId });
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

            model.Inventar = new NewInventarDataModel
            {
                InventarId = inventar.Id,
                Denumire = inventar.Denumire,
                AnPFun = inventar.AnPFun,
                SelectedGestiuneId = (int)inventar.GestiuneId,
                Gestiuni = gestiuni,
                SelectedLaboratorId = (int)inventar.LaboratorId,
                Laboratoare = laboratoare,
                PVerbal = inventar.PVerbal,
                Pret = inventar.Pret,
                Cantitate = inventar.Cantitate,
                NrInventar = inventar.NrInventar,
                Serie = inventar.Serie,
                SelectedTipId = (int)inventar.TipId,
                Tipuri = tipuri,
                Natura = inventar.Natura.ParseEnum<Natura>(),
                SelectedSursaId = (int)inventar.SursaId,
                Surse = surse,
                Mentiuni = inventar.Mentiuni
            };

            var sursaCalc = SVC.Surse.GetSursaByNameInvariant(Sursa.SURSA_CALCULATOARE);
            if (sursaCalc != null && sursaCalc.Id == model.Inventar.SelectedSursaId)
            {
                var calculator = SVC.Calculatoare.GetCalculator(inventar.Id);
                if (calculator != null)
                {
                    model.Calculator = new CalculatorModel
                    {
                        CalculatorId = calculator.Id,
                        Procesor = calculator.Procesor,
                        Frecventa = calculator.Frecventa,
                        Hdd = calculator.Hdd,
                        Ram = calculator.Ram,
                        CdRom = calculator.CdRom,
                        Floppy = calculator.Floppy,
                        Zipp = calculator.Zipp,
                        Monitor = calculator.Monitor,
                        Tastatura = calculator.Tastatura,
                        Accesorii = calculator.Accesorii,
                        Mentiuni = calculator.Mentiuni,
                        Mouse = calculator.Mouse,
                        PlacaBaza = calculator.PlacaBaza,
                        PlacaVideo = calculator.PlacaVideo,
                        CdW = calculator.CdW,
                        CdWR = calculator.CdWR,
                        Dvd = calculator.Dvd,
                        PlacaRetea = calculator.PlacaRetea,
                        TvTuner = calculator.TvTuner,
                        Modem = calculator.Modem,
                    };
                }
                else
                {
                    TempData["InventareMessage"] = new MessageModel
                    {
                        Message = new HtmlString(string.Format("Nu exista calculatorul pentru inventarul #{0}", id)),
                        Type = MessageType.Error,
                        Icon = MessageIcon.ErrorIcon
                    };

                    return RedirectToAction("Index");
                }
            }
            else
            {
                model.Calculator = new CalculatorModel();
            }

            return View(model);
        }

        //
        // POST: /Inventare/Edit/5

        [HttpPost]
        public ActionResult Edit(NewInventarModel model, string save)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MessageModel message = null;
                    Gestiune gestiune = SVC.Gestiuni.GetGestiune(model.Inventar.SelectedGestiuneId);

                    Inventar inventar = new Inventar
                    {
                        Id = model.Inventar.InventarId,
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
                            Id = model.Calculator.CalculatorId,
                            Procesor = model.Calculator.Procesor,
                            Frecventa = model.Calculator.Frecventa,
                            Hdd = model.Calculator.Hdd,
                            Ram = model.Calculator.Ram,
                            CdRom = model.Calculator.CdRom,
                            Floppy = model.Calculator.Floppy,
                            Zipp = model.Calculator.Zipp,
                            Monitor = model.Calculator.Monitor,
                            Tastatura = model.Calculator.Tastatura,
                            Accesorii = model.Calculator.Accesorii,
                            Mentiuni = model.Calculator.Mentiuni,
                            Mouse = model.Calculator.Mouse,
                            PlacaBaza = model.Calculator.PlacaBaza,
                            PlacaVideo = model.Calculator.PlacaVideo,
                            CdW = model.Calculator.CdW,
                            CdWR = model.Calculator.CdWR,
                            Dvd = model.Calculator.Dvd,
                            PlacaRetea = model.Calculator.PlacaRetea,
                            TvTuner = model.Calculator.TvTuner,
                            Modem = model.Calculator.Modem,
                        };
                        serviceResult = SVC.Inventare.UpdateInventarWithNewCalculator(inventar, calculator);
                        if (serviceResult.Result == (int)OperationResult.Success)
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
                        serviceResult = SVC.Inventare.UpdateInventar(inventar);
                        if (serviceResult.Result == (int)OperationResult.Success)
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
                        return RedirectToAction("Details", new { id = model.Inventar.InventarId });
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