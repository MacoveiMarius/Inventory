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
    public class CalculatoareDb : InventaryDataContext
    {
        private CalculatoareDb(SqlConnection conn)
            : base(conn)
        {
        }

        public static CalculatoareDb Create(string connectionString)
        {
            return new CalculatoareDb(new SqlConnection(connectionString));
        }

        #region TABLES

        public Table<Calculator> Calculatoare
        {
            get { return GetTable<Calculator>(); }
        }

        #endregion TABLES
    }
}