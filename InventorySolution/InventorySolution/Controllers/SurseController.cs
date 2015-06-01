using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Inventory.Core.Domain;
using Inventory.Services;
using InventorySolution.Models;

namespace InventorySolution.Controllers
{
    public class SurseController : Controller
    {
        private ISurse SurseService
        {
            get { return SVC.Surse; }
        }

        //
        // GET: /Surse/
        public ActionResult Index()
        {
            SurseViewModel model = new SurseViewModel()
            {
                Surse = SurseService.GetSurse()
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
                        "aici".ToLink(new UrlHelper(Request.RequestContext).Action("Create", "Surse"),
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
        // GET: /Surse/Create
        public ActionResult Create(int? id)
        {
            Sursa sursa = null;
            if (id.HasValue)
            {
                sursa = SurseService.GetSursa(id.Value);
            }
            sursa = sursa ?? new Sursa();
            return PartialView("EditorTemplates/SursaEditor", new SursaModel()
            {
                Id = sursa.Id,
                Nume =  sursa.Nume
            });
        }

        [HttpPost]
        public ActionResult AddOrUpdateSursa(int? id)   //id-ul este pk din form
        {
            ServiceResult serviceResult = new ServiceResult();
            var msg = String.Empty;
            try
            {
                int sursaId;
                int.TryParse(Request.Form["pk"], out sursaId);
                string value = Request.Form["value"];
                var sursa = new Sursa
                {
                    Id = sursaId,
                    Nume = value
                };
                if (sursaId > 0)
                {
                    //update sursa
                    serviceResult = SurseService.UpdateSursa(sursa);
                    msg = string.Format("Sursa #{0}(Nume = {1}) acutalizata cu succes", sursa.Id, sursa.Nume);
                }
                else
                {
                    //adauga sursa
                    serviceResult = SurseService.AddSursa(sursa);
                    msg = string.Format("Sursa #{0}(Nume = {1}) adaugata cu succes", sursa.Id, sursa.Nume);
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
            return Json(new {Id = serviceResult.EntityId, Message = msg }, JsonRequestBehavior.DenyGet);
        }

        //
        // GET: /Surse/Delete/5
        public ActionResult Delete(int id)
        {
            SurseService.DeleteSursa(id);
            return RedirectToAction("Index");
        }
    }
}