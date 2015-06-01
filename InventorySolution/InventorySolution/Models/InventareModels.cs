using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace InventorySolution.Models
{

    public class InventarModel
    {
        [Required]
        [Display(Name = "Nr. ctr.")]
        public int InventarId { get; set; }

        [Required]
        [Display(Name = "Denumire")]
        public string Denumire { get; set; }

        [Required]
        [Display(Name = "Gestiune")]
        public Gestiune Gestiune { get; set; }

        [Required]
        [Display(Name = "Laborator")]
        public Laborator Laborator { get; set; }

        [Display(Name = "Perioada functionalitate")]
        public string AnPFun { get; set; }

        [Display(Name = "Proces verbal")]
        public string PVerbal { get; set; }

        [Display(Name = "Pret")]
        public double? Pret { get; set; }

        [Display(Name = "Cantitate")]
        public short? Cantitate { get; set; }

        [Display(Name = "Valoare")]
        public double? Valoare { get; set; }

        [Display(Name = "Nr. inventar")]
        public string NrInventar { get; set; }

        [Display(Name = "Serie")]
        public string Serie { get; set; }

        [Required]
        [Display(Name = "Tip")]
        public Tip Tip { get; set; }

        [Display(Name = "Natura")]
        public string Natura { get; set; }

        [Required]
        [Display(Name = "Sursa")]
        public Sursa Sursa { get; set; }

        [Display(Name = "Mentiuni")]
        public string Mentiuni { get; set; }
        
        public MessageModel Message { get; set; }
    }

    public class InventareModel
    {
        public List<Inventar> Inventare { get; set; }

        public InventareModel()
        {
            Inventare = new List<Inventar>();
        }

        public MessageModel Message { get; set; }
    }

    public class NewInventar
    {
        public Inventar Inventar { get; set; }

        public  int SelectedGestiuneId { get; set; }
        public List<Gestiune> Gestiuni { get; set; }
 
        public int SelectedLaboratorId { get; set; }
        public List<Laborator> Laboratoare { get; set; } 

        public int SelectedTipId { get; set; }
        public List<Tip> Tipuri { get; set; } 

        public int SelectedSursaId { get; set; }
        public List<Sursa> Surse { get; set; } 

        public MessageModel Message { get; set; }
    } 
}
