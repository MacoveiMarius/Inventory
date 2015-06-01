using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventorySolution.Models
{
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

        public MessageModel Message { get; set; }
    }
}