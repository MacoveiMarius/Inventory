using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventorySolution.Models
{
    public class SurseViewModel
    {
        public List<SursaModel> Surse { get; set; }

        public SurseViewModel()
        {
            Surse= new List<SursaModel>();
        }

        public MessageModel Message { get; set; }
    }

    public class SursaModel
    {
        [Display(Name = "Nr. Crt.")]
        public int SursaId { get; set; }

        [Required]
        [Display(Name = "Nume")]
        public string Nume { get; set; }

        public MessageModel Message { get; set; }
    }
}