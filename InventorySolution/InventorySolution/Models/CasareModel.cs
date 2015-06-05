using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventorySolution.Models
{
    public class CasariViewModel
    {
        public List<CasareModel> Casari { get; set; }
        public CasariViewModel()
        {
            Casari = new List<CasareModel>();
        }

        public MessageModel Message { get; set; }
    }

    public class CasareModel
    {
        public int InventarId { get; set; }
     
        [DisplayName("Nr_crt")]
        public int Id { get; set; }

        [DisplayName("Denumire")]
        public string Denumire { get; set; }

        [DisplayName("Nume")]
        public string Nume { get; set; }

        [DisplayName("Prenume")]
        public string Prenume { get; set; }

        [DisplayName("Laborator")]
        public string Laborator { get; set; }

        [DisplayName("AnPFun")]
        public string AnPFun { get; set; }

        [DisplayName("PVerbal")]
        public string PVerbal { get; set; }

        [DisplayName("Pret")]
        public double? Pret { get; set; }

        [DisplayName("Cant")]
        public int? Cantitate { get; set; }

        [DisplayName("Valoare")]
        public double? Valoare { get; set; }

        [DisplayName("Nr_inv")]
        public string NrInventar { get; set; }

        [DisplayName("Serie")]
        public string Serie { get; set; }

        [DisplayName("Mentiuni")]
        public string Mentiuni { get; set; }

        [DisplayName("DCasare")]
        public string DataCasare { get; set; }

        [DisplayName("Cod")]
        public string Cod { get; set; }

        //[Integer(ErrorMessage = "Durata normala trebuie sa fie un intreg")]
        //[Range(0, 1000, ErrorMessage = "Durata normala trebuie sa in intervalul 0 - 1000")]
        [DisplayName("DurataNorm")]
        public int? DurataNormala { get; set; }

        //[Integer(ErrorMessage = "Durata reala trebuie sa fie un intreg")]
        //[Range(0, 1000, ErrorMessage = "Durata reala trebuie sa in intervalul 0 - 1000")]
        [DisplayName("DurataReal")]
        public int? DurataReal { get; set; }

        public MessageModel Message { get; set; }
    }
}
