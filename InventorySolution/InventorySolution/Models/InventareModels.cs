using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Metadata.Edm;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using DataAnnotationsExtensions;
using Inventory.Core.Domain.Inventare;

namespace InventorySolution.Models
{

    public class InventarDataModel
    {
        [Required]
        [Display(Name = "Nr. ctr.")]
        public int InventarId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Introduceti denumirea")]
        [Display(Name = "Denumire")]
        public string Denumire { get; set; }

        [Display(Name = "Gestiune")]
        public Gestiune Gestiune { get; set; }

        [Display(Name = "Laborator")]
        public Laborator Laborator { get; set; }

        [Display(Name = "Perioada functionalitate")]
        public string AnPFun { get; set; }

        [Display(Name = "Proces verbal")]
        public string PVerbal { get; set; }

        [Display(Name = "Pret")]
        [Range(0, 10000, ErrorMessage = "Pretul trebuie sa in intervalul 0 - 10000")]
        public double? Pret { get; set; }

        [Integer(ErrorMessage = "Cantitatea trebuie sa fie un intreg")]
        [Display(Name = "Cantitate")]
        public short? Cantitate { get; set; }

        [Display(Name = "Valoare")]
        public double? Valoare { get; set; }

        [Display(Name = "Nr. inventar")]
        public string NrInventar { get; set; }

        [Display(Name = "Serie")]
        public string Serie { get; set; }

        [Display(Name = "Tip")]
        public Tip Tip { get; set; }

        [Display(Name = "Natura")]
        public Natura Natura { get; set; }

        [Display(Name = "Sursa")]
        public Sursa Sursa { get; set; }

        [Display(Name = "Mentiuni")]
        public string Mentiuni { get; set; }
        
        public MessageModel Message { get; set; }

        public InventarDataModel()
        {
            Sursa = new Sursa();
            Gestiune = new Gestiune();
            Laborator = new Laborator();
            Tip = new Tip();
        }
    }

    public class InventareModel
    {
        public List<InventarDataModel> Inventare { get; set; }

        public InventareModel()
        {
            Inventare = new List<InventarDataModel>();
        }

        public MessageModel Message { get; set; }
    }

    public class NewInventarDataModel : InventarDataModel
    {
        [Required]
        public int SelectedGestiuneId { get; set; }
        public List<Gestiune> Gestiuni { get; set; }

        [Required]
        public int SelectedLaboratorId { get; set; }
        public List<Laborator> Laboratoare { get; set; }

        [Required]
        public int SelectedTipId { get; set; }
        public List<Tip> Tipuri { get; set; }

        [Required]
        public int SelectedSursaId { get; set; }
        public List<Sursa> Surse { get; set; } 
    }

    public class NewInventarModel
    {
        public NewInventarDataModel Inventar { get; set; }
        public CalculatorModel Calculator { get; set; }

        public MessageModel Message { get; set; }
    }

    public class InventarModel
    {
        public InventarDataModel Inventar { get; set; }
        public CalculatorModel Calculator { get; set; }

        public MessageModel Message { get; set; }
    }
}
