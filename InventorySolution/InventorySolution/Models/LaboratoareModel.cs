using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventorySolution.Models
{
    public class LaboratorModel
    {
        [DisplayName("Nr. crt.")]
        public int LaboratorId { get; set; }

        [Required]
        [DisplayName("Nume")]
        public string Nume { get; set; }

        public MessageModel Message { get; set; }
    }
}