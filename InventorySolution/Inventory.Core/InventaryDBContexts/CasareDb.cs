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
    
    public class CasareDb : InventaryDataContext
    {
        private CasareDb(SqlConnection conn)
            : base(conn)
        {
        }

        public static CasareDb Create(string connectionString)
        {
            return new CasareDb(new SqlConnection(connectionString));
        }

        #region TABLES
        public Table<Casare> Casare
        {
            get { return GetTable<Casare>(); }
        }

        //public Table<Inventar> Inventare
        //{
        //    get { return GetTable<Inventar>(); }
        //}
        #endregion TABLES
    }
}
