using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Inventory.Core.Domain
{
    [Table(Name = "dbo.Inventar")]
    [DataContract]
    [KnownType(typeof(Inventar))]
    public class Inventar : BaseEntity
    {
        private EntityRef<Sursa> _sursa;
        private EntityRef<Laborator> _laborator;
        private EntityRef<Gestiune> _gestiune;
        private EntityRef<Tip> _tip; 

        public Inventar()
        {
            _sursa = new EntityRef<Sursa>();
        }

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
        
        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(IsDbGenerated = true, IsPrimaryKey = true, Name = "Nr_crt")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Denumire")]
        public string Denumire { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = true, Name = "Gestiune")]
        public int? GestiuneId { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = true, Name = "Lab")]
        public int? LaboratorId { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = true, Name = "AnPFun")]
        public string AnPFun { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = true, Name = "PVerbal")]
        public string PVerbal { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = true, Name = "Pret")]
        public double? Pret { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = true, Name = "Cant")]
        public short? Cantitate { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = true, Name = "Valoare")]
        public double? Valoare { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = true, Name = "Nr_inv")]
        public string NrInventar { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = true, Name = "Serie")]
        public string Serie { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = true, Name = "Tip")]
        public int? TipId { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = true, Name = "Natura")]
        public string Natura { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = true, Name = "Sursa")]
        public int? SursaId { get; set; }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Mentiuni")]
        public string Mentiuni { get; set; }


        public override int GetId()
        {
            return Id;
        }
    }
}
