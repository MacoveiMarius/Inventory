using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Threading;
using Inventory.Core.Domain;

namespace Inventory.Core
{
    public class InventareDb : InventaryDataContext
    {
        private InventareDb(SqlConnection conn)
            : base(conn)
        {
        }

        public static InventareDb Create(string connectionString)
        {
            return new InventareDb(new SqlConnection(connectionString));
        }

        #region TABLES

        public Table<Inventar> Inventare
        {
            get { return GetTable<Inventar>(); }
        }

        public Table<Sursa> Surse
        {
            get { return GetTable<Sursa>(); }
        }

        public Table<Laborator> Laboratoare
        {
            get { return GetTable<Laborator>(); }
        }

        public Table<Gestiune> Gestiuni
        {
            get { return GetTable<Gestiune>(); }
        }

        public Table<Tip> Tipuri
        {
            get { return GetTable<Tip>(); }
        }

        public Table<Calculator> Calculatoare
        {
            get { return GetTable<Calculator>(); }
        } 
        #endregion TABLES
    }
}