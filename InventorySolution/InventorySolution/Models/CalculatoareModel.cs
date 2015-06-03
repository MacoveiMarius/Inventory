using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InventorySolution.Models
{
    public class CalculatorModel
    {
        [Display(Name = "Nr. crt.")]
        public int CalculatorId { get; set; }

        [Display(Name = "Procesor")]
        public string Procesor { get; set; }

        [Display(Name = "Frecventa")]
        public string Frecventa { get; set; }

        [Display(Name = "HDD")]
        public string Hdd { get; set; }

        [Display(Name = "RAM")]
        public string Ram { get; set; }

        [Display(Name = "CD-ROM")]
        public string CdRom { get; set; }

        [Display(Name = "Floppy")]
        public string Floppy { get; set; }

        [Display(Name = "Zipp")]
        public string Zipp { get; set; }

        [Display(Name = "Monitor")]
        public string Monitor { get; set; }

        [Display(Name = "Tastatura")]
        public string Tastatura { get; set; }

        [Display(Name = "Accesorii")]
        public string Accesorii { get; set; }

        [Display(Name = "Mentiuni")]
        public string Mentiuni { get; set; }

        [Display(Name = "Mouse")]
        public string Mouse { get; set; }

        [Display(Name = "Placa de baza")]
        public string PlacaBaza { get; set; }

        [Display(Name = "Placa video")]
        public string PlacaVideo { get; set; }

        [Display(Name = "CD W")]
        public string CdW { get; set; }

        [Display(Name = "CD WR")]
        public string CdWR { get; set; }

        [Display(Name = "DVD")]
        public string Dvd { get; set; }

        [Display(Name = "Placa de retea")]
        public string PlacaRetea { get; set; }

        [Display(Name = "Tv tuner")]
        public string TvTuner { get; set; }

        [Display(Name = "Modem")]
        public string Modem { get; set; }

        public MessageModel Message { get; set; }
    }
}