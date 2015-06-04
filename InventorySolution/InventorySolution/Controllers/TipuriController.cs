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
    public class TipuriController : Controller
    {
        private ITipuri TipuriService
        {
            get { return SVC.Tipuri; }
        }

        //
        // GET: /Tipuri/

        public ActionResult Index()
        {
            TipuriViewModel model = new TipuriViewModel()
            {
                Tipuri = TipuriService.GetTipuri().Select(t => new TipModel
                {
                    TipId = t.Id,
                    Nume = t.Nume
                }).ToList()
            };

            if (model.Tipuri.Count == 0)
            {
                model.Message = new MessageModel
                {
                    Message = new HtmlString(string.Format("Nu exista tipuri.\n Adaugati {0} aici"
                        .ToLink(new UrlHelper(Request.RequestContext)
                        .Action("Create", "Tipuri"),
                        new Dictionary<string, string>(){
                            {"class", "add-custom-field"}
                        }))),
                    Icon = MessageIcon.WarningIcon,
                    Type = MessageType.Warning
                };
            }
            return View(model);
        }

        //
        // GET: /Tipuri/Create

        public ActionResult Create(int? id)
        {
            Tip tip = null;
            if (id.HasValue)
                tip = TipuriService.GetTip(id.Value);
            tip = tip ?? new Tip();
            return PartialView("EditorTemplates/TipEditor", new TipModel()
            {
                TipId = tip.Id,
                Nume = tip.Nume,
            });
        }

        [HttpPost]
        public ActionResult AddOrUpdateTip(int? id)   //id-ul este pk din form
        {
            ServiceResult serviceResult = new ServiceResult();
            var msg = String.Empty;
            try
            {
                int tipId;
                int.TryParse(Request.Form["pk"], out tipId);
                string value = Request.Form["value"];
                var tip = new Tip
                {
                    Id = tipId,
                    Nume = value
                };
                if (tipId > 0)
                {
                    //update tip
                    serviceResult = TipuriService.UpdateTip(tip);
                    msg = string.Format("Tipul #{0}(Nume = {1}) acutalizata cu succes", tip.Id, tip.Nume);
                }
                else
                {
                    //adauga tip
                    serviceResult = TipuriService.AddTip(tip);
                    msg = string.Format("Tipul #{0}(Nume = {1}) adaugata cu succes", tip.Id, tip.Nume);
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
        // GET: /Tipuri/Delete/5
 
        public ActionResult Delete(int id)
        {
            TipuriService.DeleteTip(id);
            return RedirectToAction("Index");
        }

    }
}
