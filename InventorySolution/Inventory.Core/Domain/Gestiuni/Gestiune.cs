﻿using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Inventory.Core.Domain
{
    [Table(Name = "dbo.Gestiune")]
    [DataContract]
    [KnownType(typeof(Gestiune))]
    public class Gestiune : BaseEntity
    {
        private EntitySet<Inventar> _inventare;

        [Association(Storage = "_inventare", ThisKey = "Id", OtherKey = "GestiuneId",
            IsForeignKey = true)]
        public EntitySet<Inventar> InventareEntity
        {
            get
            {
                _inventare = ValidateEntitySet(_inventare) ?? new EntitySet<Inventar>(OnAdd, OnRemove);
                return _inventare;
            }
            set
            {
                _inventare = ValidateEntitySet<Inventar>(value) ?? new EntitySet<Inventar>(OnAdd, OnRemove);
                _inventare.Assign(value);
            }
        } 

         public Gestiune()
        {
            _inventare = new EntitySet<Inventar>(OnAdd, OnRemove);
        }

        private void OnAdd(Inventar entity)
        {
            entity.GestiuneEntity = this;
        }

        private void OnRemove(Inventar entity)
        {
            entity.GestiuneEntity = null;
        }

        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(IsDbGenerated = true, IsPrimaryKey = true, Name = "Gestiune")]
        public short Id { get; set; } 
        
        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Nume")]
        public string Nume { get; set; }
            
        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Prenume")]
        public string Prenume { get; set; }
        
        /// <summary>
        /// Gets or sets the setting identifier
        /// </summary>
        [DataMember]
        [Column(CanBeNull = false, Name = "Catedra")]
        public string Catedra { get; set; }

        [DisplayName("Nume")]
        public string FullName
        {
            get { return string.Format("{0} {1}", Nume, Prenume); }
        }
        
        public override short GetId()
        {
            return Id;
        }
    }
}
