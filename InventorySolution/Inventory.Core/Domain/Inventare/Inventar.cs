using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Inventory.Core.Domain
{
    [Table(Name = "dbo.Inventar")]
    [DataContract]
    [KnownType(typeof (Inventar))]
    public class Inventar : BaseEntity
    {
        private EntityRef<Sursa> _sursa;
        private EntityRef<Laborator> _laborator;
        private EntityRef<Gestiune> _gestiune;
        private EntityRef<Tip> _tip;
        private EntitySet<Calculator> _calculatoare;

        public Inventar()
        {
            _sursa = new EntityRef<Sursa>();
            _calculatoare = new EntitySet<Calculator>(OnAdd, OnRemove);
        }

        #region Calculatoare

        [DataMember]
        [Association(Storage = "_calculatoare", ThisKey = "Id", OtherKey = "Id",
            IsForeignKey = true)]
        public EntitySet<Calculator> InventareEntity
        {
            get
            {
                _calculatoare = ValidateEntitySet(_calculatoare) ?? new EntitySet<Calculator>(OnAdd, OnRemove);
                return _calculatoare;
            }
            set
            {
                _calculatoare = ValidateEntitySet<Calculator>(value) ?? new EntitySet<Calculator>(OnAdd, OnRemove);
                _calculatoare.Assign(value);
            }
        }

        private void OnAdd(Calculator entity)
        {
            entity.InventarEntity = this;
        }

        private void OnRemove(Calculator entity)
        {
            entity.InventarEntity = null;
        }

        #endregion

        #region Sursa

        [Association(Storage = "_sursa", ThisKey = "SursaId", OtherKey = "Id",
            IsForeignKey = true)]
        public Sursa SursaEntity
        {
            get { return _sursa.Entity; }
            set
            {
                _sursa.Entity = value;
                if ((value != null))
                {
                    value.InventareEntity.Add(this);
                    SursaId = value.Id;
                }
                else
                {
                    SursaId = default(int);
                }
            }
        }

        #endregion

        #region Laborator

        [Association(Storage = "_laborator", ThisKey = "LaboratorId", OtherKey = "Id",
            IsForeignKey = true)]
        public Laborator LaboratorEntity
        {
            get { return _laborator.Entity; }
            set
            {
                _laborator.Entity = value;
                if ((value != null))
                {
                    value.InventareEntity.Add(this);
                    LaboratorId = value.Id;
                }
                else
                {
                    LaboratorId = default(int);
                }
            }
        }

        #endregion

        #region Gestiune

        [Association(Storage = "_gestiune", ThisKey = "GestiuneId", OtherKey = "Id",
            IsForeignKey = true)]
        public Gestiune GestiuneEntity
        {
            get { return _gestiune.Entity; }
            set
            {
                _gestiune.Entity = value;
                if ((value != null))
                {
                    value.InventareEntity.Add(this);
                    GestiuneId = value.Id;
                }
                else
                {
                    GestiuneId = default(int);
                }
            }
        }

        #endregion

        #region Tip

        [Association(Storage = "_tip", ThisKey = "TipId", OtherKey = "Id",
            IsForeignKey = true)]
        public Tip TipEntity
        {
            get { return _tip.Entity; }
            set
            {
                _tip.Entity = value;
                if ((value != null))
                {
                    value.InventareEntity.Add(this);
                    TipId = value.Id;
                }
                else
                {
                    TipId = default(int);
                }
            }
        }

        #endregion

        [DataMember]
        [Column(IsDbGenerated = true, IsPrimaryKey = true, Name = "Nr_crt")]
        public int Id { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Denumire")]
        public string Denumire { get; set; }

        [DataMember]
        [Column(CanBeNull = true, Name = "Gestiune")]
        public int? GestiuneId { get; set; }

        [DataMember]
        [Column(CanBeNull = true, Name = "Lab")]
        public int? LaboratorId { get; set; }

        [DataMember]
        [Column(CanBeNull = true, Name = "AnPFun")]
        public string AnPFun { get; set; }

        [DataMember]
        [Column(CanBeNull = true, Name = "PVerbal")]
        public string PVerbal { get; set; }

        [DataMember]
        [Column(CanBeNull = true, Name = "Pret")]
        public double? Pret { get; set; }

        [DataMember]
        [Column(CanBeNull = true, Name = "Cant")]
        public short? Cantitate { get; set; }

        [DataMember]
        [Column(CanBeNull = true, Name = "Valoare")]
        public double? Valoare { get; set; }

        [DataMember]
        [Column(CanBeNull = true, Name = "Nr_inv")]
        public string NrInventar { get; set; }

        [DataMember]
        [Column(CanBeNull = true, Name = "Serie")]
        public string Serie { get; set; }

        [DataMember]
        [Column(CanBeNull = true, Name = "Tip")]
        public int? TipId { get; set; }

        [DataMember]
        [Column(CanBeNull = true, Name = "Natura")]
        public string Natura { get; set; }

        [DataMember]
        [Column(CanBeNull = true, Name = "Sursa")]
        public int? SursaId { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Mentiuni")]
        public string Mentiuni { get; set; }

        public override int GetId()
        {
            return Id;
        }
    }
}