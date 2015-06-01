using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Reflection;
using Inventory.Core.Domain;

namespace Inventory.Core
{
    public class SurseDb : InventaryDataContext
    {
        private SurseDb(SqlConnection conn)
            : base(conn)
        {
        }

        public static SurseDb Create(string connectionString)
        {
            return new SurseDb(new SqlConnection(connectionString));
        }

        #region TABLES

        public Table<Sursa> Surse
        {
            get { return GetTable<Sursa>(); }
        }

        public Table<Inventar> Inventare
        {
            get { return GetTable<Inventar>(); }
        }

        #endregion TABLES
    }
}