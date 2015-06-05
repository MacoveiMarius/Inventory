using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Inventory.Core.Domain
{
    [Table(Name = "dbo.Calculatoare")]
    [DataContract]
    [KnownType(typeof (Calculator))]
    public class Calculator : BaseEntity
    {
        private EntityRef<Inventar> _inventar;

        #region Inventar

        [Association(Storage = "_inventar", ThisKey = "Id", OtherKey = "Id",
            IsForeignKey = true)]
        public Inventar InventarEntity
        {
            get { return _inventar.Entity; }
            set
            {
                _inventar.Entity = value;
                if ((value != null))
                {
                    value.InventareEntity.Add(this);
                    Id = value.Id;
                }
                else
                {
                    Id = default(int);
                }
            }
        }

        #endregion

        [DataMember]
        [Column(IsDbGenerated = false, IsPrimaryKey = true, Name = "Nr_crt")]
        public int Id { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Procesor")]
        public string Procesor { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Frecventa")]
        public string Frecventa { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "HDD")]
        public string Hdd { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "RAM")]
        public string Ram { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "CDROM")]
        public string CdRom { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Floppy")] //[Floppy]
        public string Floppy { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "ZIPP")]
        public string Zipp { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "S_monitor")]
        public string Monitor { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "S_tastatura")]
        public string Tastatura { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Accesorii")]
        public string Accesorii { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Mentiuni")]
        public string Mentiuni { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Mouse")]
        public string Mouse { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Placa_baza")]
        public string PlacaBaza { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Placa_video")]
        public string PlacaVideo { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "CD_W")]
        public string CdW { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "CD_WR")]
        public string CdWR { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "DVD")]
        public string Dvd { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Placa_retea")]
        public string PlacaRetea { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Tv_tuner")]
        public string TvTuner { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Modem")]
        public string Modem { get; set; }

        public override int GetId()
        {
            return Id;
        }
    }
}