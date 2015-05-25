using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Inventory.Core.Domain
{
    [Table(Name = "dbo.Laborator")]
    [DataContract]
    [KnownType(typeof(Laborator))]
    public class Laborator : BaseEntity
    {
        private EntitySet<Inventar> _inventare;

        [Association(Storage = "_inventare", ThisKey = "Id", OtherKey = "LaboratorId",
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

         public Laborator()
        {
            _inventare = new EntitySet<Inventar>(OnAdd, OnRemove);
        }

        private void OnAdd(Inventar entity)
        {
            entity.LaboratorEntity = this;
        }

        private void OnRemove(Inventar entity)
        {
            entity.LaboratorEntity = null;
        }

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
        [Column(CanBeNull = false, Name = "Laborator")]
        public string Nume { get; set; }
         
        public override short GetId()
        {
            return Id;
        }
    }
}
