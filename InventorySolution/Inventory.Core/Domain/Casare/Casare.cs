using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Inventory.Core.Domain
{
    [Table(Name = "dbo.Casare")]
    [DataContract]
    [KnownType(typeof (Casare))]
    public class Casare : BaseEntity
    {

        [DataMember]
        [Column(IsDbGenerated = true, IsPrimaryKey = true, Name = "Nr_crt")]
        public int Id { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Denumire")]
        public string Denumire { get; set; }

        [DataMember]
        [Column(CanBeNull = true, Name = "Nume")]
        public string Nume { get; set; }

        [DataMember]
        [Column(CanBeNull = true, Name = "Prenume")]
        public string Prenume { get; set; }

        [DataMember]
        [Column(CanBeNull = true, Name = "Laborator")]
        public string Laborator { get; set; }

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
        public int? Cantitate { get; set; }

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
        [Column(CanBeNull = false, Name = "Mentiuni")]
        public string Mentiuni { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "DCasare")]
        public DateTime DataCasare { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "Cod")]
        public string Cod { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "DurataNorm")]
        public int? DurataNormala { get; set; }

        [DataMember]
        [Column(CanBeNull = false, Name = "DurataReal")]
        public int? DurataReal { get; set; }

        public override int GetId()
        {
            return Id;
        }
    }
}