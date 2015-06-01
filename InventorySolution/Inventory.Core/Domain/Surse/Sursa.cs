using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Inventory.Core.Domain
{
    [Table(Name = "dbo.Sursa")]
    [DataContract]
    [KnownType(typeof(Sursa))]
    public class Sursa : BaseEntity
    {
        private EntitySet<Inventar> _inventare;

        //[ScriptIgnore]
        [DataMember]
        [Association(Storage = "_inventare", ThisKey = "Id", OtherKey = "SursaId",
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

        public Sursa()
        {
            _inventare = new EntitySet<Inventar>(OnAdd, OnRemove);
        }

        private void OnAdd(Inventar entity)
        {
            entity.SursaEntity = this;
        }

        private void OnRemove(Inventar entity)
        {
            entity.SursaEntity = null;
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
        [Column(CanBeNull = false, Name = "Sursa")]
        public string Nume { get; set; }
          
        public override int GetId()
        {
            return Id;
        }
    }
}
