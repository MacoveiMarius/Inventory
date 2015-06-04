﻿using System;
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
    
    public class LaboratoareDb : InventaryDataContext
    {
        private LaboratoareDb(SqlConnection conn)
            : base(conn)
        {
        }

        public static LaboratoareDb Create(string connectionString)
        {
            return new LaboratoareDb(new SqlConnection(connectionString));
        }

        #region TABLES
        public Table<Laborator> Laboratoare
        {
            get { return GetTable<Laborator>(); }
        }

        public Table<Inventar> Inventare
        {
            get { return GetTable<Inventar>(); }
        }
        #endregion TABLES
}
}
