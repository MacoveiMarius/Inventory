using Inventory.Core.Domain;
using Inventory.Services;
using InventorySolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventorySolution.Controllers
{
    public class CasariController : Controller
    { 

        //
        // GET: /Casare/

        public ActionResult Index()
        {
            CasariViewModel model = new CasariViewModel()
            {
                Casari = SVC.Casari.GetCasari().Select(c => new CasareModel
                {
                    Id = c.Id,
                    Denumire = c.Denumire,
                    Nume = c.Nume,
                    Prenume = c.Prenume,
                    Laborator = c.Laborator,
                    AnPFun = c.AnPFun,
                    PVerbal = c.PVerbal,
                    Pret = c.Pret,
                    Cantitate = c.Cantitate,
                    Valoare = c.Valoare,
                    NrInventar = c.NrInventar,
                    Serie = c.Serie,
                    Mentiuni = c.Mentiuni,
                    DataCasare = c.DataCasare.ToString(),
                    Cod = c.Cod,
                    DurataNormala = c.DurataNormala,
                    DurataReal = c.DurataReal
                }).ToList(),
                Message = TempData["CasariMessage"] as MessageModel
            };
            return View(model);
        }

        //
        // GET: /Casare/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Casare/Create

        public ActionResult Create(int id)
        {
            Inventar inventar = SVC.Inventare.GetInventar(id);
            if (inventar != null)
            {
                CasareModel model = new CasareModel
                {
                    Denumire = inventar.Denumire,
                    AnPFun = inventar.AnPFun,
                    NrInventar = inventar.NrInventar,
                    Serie = inventar.Serie,
                    Pret = inventar.Pret,
                    Cantitate = inventar.Cantitate,
                    Valoare = inventar.Valoare,
                    PVerbal=inventar.PVerbal,
                    InventarId = id,
                    DataCasare = DateTime.Now.ToShortDateString(),
                };
                if (inventar.LaboratorId.HasValue)
                {
                    Laborator lab = SVC.Laboratoare.GetLaborator(inventar.LaboratorId.Value);
                    if (lab != null)
                    {
                        model.Laborator = lab.Nume;
                    }
                }
                if (inventar.GestiuneId.HasValue)
                {
                    Gestiune gest = SVC.Gestiuni.GetGestiune(inventar.GestiuneId.Value);
                    if (gest != null)
                    {
                        model.Nume = gest.Nume;
                        model.Prenume = gest.Prenume;
                    }
                }
                return View("Create", model);
            }
            return RedirectToAction("Index", "Inventare");
        } 

        //
        // POST: /Casare/Create

        [HttpPost]
        public ActionResult Create(CasareModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MessageModel message = null;
                    if (ModelState.IsValid)
                    {
                        Casare casare = new Casare
                        {
                            Denumire = model.Denumire,
                            Nume = model.Nume,
                            Prenume = model.Prenume,
                            Laborator = model.Laborator,
                            AnPFun = model.AnPFun,
                            NrInventar = model.NrInventar,
                            Serie = model.Serie,
                            Pret = model.Pret,
                            Cantitate = model.Cantitate,
                            Valoare = model.Valoare,
                            PVerbal = model.PVerbal,
                            DataCasare = Convert.ToDateTime(model.DataCasare),
                            Cod = model.Cod,
                            DurataNormala = model.DurataNormala,
                            DurataReal = model.DurataReal,
                            Mentiuni = model.Mentiuni
                        };


                        ServiceResult serviceResult = SVC.Inventare.Caseaza(model.InventarId, casare);
                        if (serviceResult.Result == (int) OperationResult.Success)
                        {
                            message = new MessageModel
                            {
                                Message =
                                    new HtmlString(string.Format("Am adaugat casarea #{0}", serviceResult.EntityId)),
                                Icon = MessageIcon.SuccessIcon,
                                Type = MessageType.Success
                            };
                        }
                        else
                        {
                            message = new MessageModel
                            {
                                Message =
                                    new HtmlString(string.Format("Am intampnat o eroare")),
                                Icon = MessageIcon.ErrorIcon,
                                Type = MessageType.Error
                            };
                        }
                    }

                    return RedirectToAction("Index");
                }
                catch
                {
                    return RedirectToAction("Create", "Casari", new {id = model.InventarId});
                }
            }
            return View("Create", model);
        }
        
        //
        // GET: /Casare/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Casare/Edit/5

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
        // GET: /Casare/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Casare/Delete/5

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
