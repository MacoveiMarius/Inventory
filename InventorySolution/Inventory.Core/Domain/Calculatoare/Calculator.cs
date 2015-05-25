using System;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Inventory.Core.Domain
{
    [Table(Name = "dbo.Calculatoare")]
    [DataContract]
    [KnownType(typeof(Calculator))]
    public class Calculator : BaseEntity
    {
        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(IsDbGenerated = true, IsPrimaryKey = true, Name = "Nr_crt")]
        public short Id { get; set; } 
        
        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Procesor")]
        public string Procesor { get; set; }
            
        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Frecventa")]
        public string Frecventa { get; set; }
        
        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "HDD")]
        public string Hdd { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "RAM")]
        public string Ram { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "CDROM")]
        public string CdRom { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Floppy")] //[Floppy]
        public string Floppy { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "ZIPP")]
        public string Zipp { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "S_monitor")]
        public string Monitor { get; set; }
        
        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "S_tastatura")]
        public string Tastatura { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Accesorii")]
        public string Accesorii { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Mentiuni")]
        public string Mentiuni { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Mouse")]
        public string Mouse { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Placa_baza")]
        public string PlacaBaza { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Placa_video")]
        public string PlacaVideo { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "CD_W")]
        public string CdW { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "CD_WR")]
        public string CdWR { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "DVD")]
        public string Dvd { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Placa_retea")]
        public string PlacaRetea { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Tv_tuner")]
        public string TvTuner { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Modem")]
        public string Modem { get; set; }

        public override short GetId()
        {
            return Id;
        }
    }
}
