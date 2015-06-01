using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventorySolution.Models
{
    public class TipModel
    {
        [Display(Name = "Nr. Crt.")]
        public int TipId { get; set; }

        [Required]
        [Display(Name = "Nume")]
        public string Nume { get; set; }

        public MessageModel Message { get; set; }
    }
}