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
    public class TipuriDb : InventaryDataContext
    {
        private TipuriDb(SqlConnection conn)
            : base(conn)
        {
        }

        public static TipuriDb Create(string connectionString)
        {
            return new TipuriDb(new SqlConnection(connectionString));
        }

        #region TABLES

        public Table<Tip> Tipuri
        {
            get { return GetTable<Tip>(); }
        }

        #endregion TABLES
    }
}