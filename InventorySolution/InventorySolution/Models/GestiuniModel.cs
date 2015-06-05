using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventorySolution.Models
{
    public class GestiuneViewModel
    {
        public List<GestiuneModel> Gestiune { get; set; }

        public GestiuneViewModel()
        {
            Gestiune = new List<GestiuneModel>();
        }

        public MessageModel Message { get; set; }
    }

    public class GestiuneModel
    {
        [DisplayName("Nr. crt.")]
        public int GestiuneId { get; set; }

        [Required]
        [DisplayName("Nume")]
        public string Nume { get; set; }

        [Required]
        [DisplayName("Prenume")]
        public string Prenume { get; set; }

        [Required]
        [DisplayName("Catedra")]
        public string Catedra { get; set; }

        [DisplayName("Nume")]
        public string FullName
        {
            get { return Nume != null || Prenume != null ? 
                string.Format("{0} {1}", Nume, Prenume) : string.Empty; }
        }

        public string ToValue()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(new { nume = Nume ?? string.Empty, prenume = Prenume ?? string.Empty, catedra = Catedra ?? string.Empty});
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public MessageModel Message { get; set; }
    }
}