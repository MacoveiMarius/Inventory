using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Inventory.Core.Domain
{
    [Table(Name = "dbo.Tip")]
    [DataContract]
    [KnownType(typeof (Tip))]
    public class Tip : BaseEntity
    {
        private EntitySet<Inventar> _inventare;

        #region Inventare

        [Association(Storage = "_inventare", ThisKey = "Id", OtherKey = "TipId",
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

        public Tip()
        {
            _inventare = new EntitySet<Inventar>(OnAdd, OnRemove);
        }

        private void OnAdd(Inventar entity)
        {
            entity.TipEntity = this;
        }

        private void OnRemove(Inventar entity)
        {
            entity.TipEntity = null;
        }

        #endregion

        [DataMember]
        [Column(IsDbGenerated = true, IsPrimaryKey = true, Name = "Nr_crt")]
        public int Id { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Tip")]
        public string Nume { get; set; }

        public override int GetId()
        {
            return Id;
        }
    }
}