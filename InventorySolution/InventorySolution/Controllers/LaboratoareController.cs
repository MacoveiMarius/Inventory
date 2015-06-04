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
    public class LaboratoareController : Controller
    {
        private ILaboratoare LaboratoareService
        {
            get { return SVC.Laboratoare; }
        }

        //
        // GET: /Laboratoare/

        public ActionResult Index()
        {
            LaboratoareViewModel model = new LaboratoareViewModel()
            {
                Laboratoare = LaboratoareService.GetLaboratoare()
                    .Select(s => new LaboratorModel
                    {
                        LaboratorId = s.Id,
                        Nume = s.Nume
                    }).ToList()
            };

            if (model.Laboratoare.Count == 0)
            {
                model.Message = new MessageModel
                {
                    Message = new HtmlString(string.Format("Nu exista laboratoare.\n Adaugati {0}",
                        "aici".ToLink(new UrlHelper(Request.RequestContext).Action("Create", "Laboratoare"),
                            new Dictionary<string, string>()
                            {
                                {"class", "add-custom-field"}
                            }))),
                    Icon = MessageIcon.WarningIcon,
                    Type = MessageType.Warning
                };
            }
            return View(model);
        }

        //
        // GET: /Laboratoare/Create

        public ActionResult Create(int? id)
        {
            Laborator lab = null;
            if (id.HasValue)
            {
                lab = LaboratoareService.GetLaborator(id.Value);
            }
            lab = lab ?? new Laborator();
            return PartialView("EditorTemplates/LaboratorEditor", new LaboratorModel()
            {
                LaboratorId = lab.Id,
                Nume = lab.Nume
            });
        }

        [HttpPost]
        public ActionResult AddOrUpdateLaborator(int? id)   //id-ul este pk din form
        {
            ServiceResult serviceResult = new ServiceResult();
            var msg = String.Empty;
            try
            {
                int labId;
                int.TryParse(Request.Form["pk"], out labId);
                string value = Request.Form["value"];
                var laborator = new Laborator
                {
                    Id = labId,
                    Nume = value
                };
                if (labId > 0)
                {
                    //update laboratorul
                    serviceResult = LaboratoareService.UpdateLaborator(laborator);
                    msg = string.Format("Laboratorul #{0}(Nume = {1}) acutalizata cu succes", laborator.Id, laborator.Nume);
                }
                else
                {
                    //adauga laboratorul
                    serviceResult = LaboratoareService.AddLaborator(laborator);
                    msg = string.Format("Laboratorul #{0}(Nume = {1}) adaugata cu succes", laborator.Id, laborator.Nume);
                }
            }
            catch
            {
                msg = string.Format("Am intampitan o eroarea");
            }

            if (serviceResult.OperationResult < OperationResult.Success)
            {
                msg = string.Format("Am intampitan o eroarea");
            }
            return Json(new { Id = serviceResult.EntityId, Message = msg }, JsonRequestBehavior.DenyGet);
        }


        //
        // GET: /Laboratoare/Delete/5
 
        public ActionResult Delete(int id)
        {
            LaboratoareService.DeleteLaborator(id);
            return RedirectToAction("Index");
        }
    }
}
