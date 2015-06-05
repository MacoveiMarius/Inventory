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
    
    public class GestiuniDb : InventaryDataContext
    {
        private GestiuniDb(SqlConnection conn)
            : base(conn)
        {
        }

        public static GestiuniDb Create(string connectionString)
        {
            return new GestiuniDb(new SqlConnection(connectionString));
        }

        #region TABLES
        public Table<Gestiune> Gestiuni
        {
            get { return GetTable<Gestiune>(); }
        }
        public Table<Inventar> Inventare
        {
            get { return GetTable<Inventar>(); }
        }
        #endregion TABLES
    }
}
