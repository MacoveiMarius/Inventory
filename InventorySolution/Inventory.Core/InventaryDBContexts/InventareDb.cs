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
        public Table<Inventar> InventareTable
        {
            get { return GetTable<Inventar>(); }
        }

        public Table<Sursa> SurseTable
        {
            get { return GetTable<Sursa>(); }
        }

        public Table<Laborator> LaboratoareTable
        {
            get { return GetTable<Laborator>(); }
        }

        public Table<Gestiune> GestiuniTable
        {
            get { return GetTable<Gestiune>(); }
        }

        public Table<Tip> TipuriTable
        {
            get { return GetTable<Tip>(); }
        }
        #endregion TABLES

        #region STORED PROCEDURES
        //[Function(Name = "GetTestById")]
        //public ISingleResult<Test> RetrieveSettingById(int testId,
        //    long subscriptionId)
        //{
        //    IExecuteResult result = ExecuteMethodCall(this, (MethodInfo)(MethodBase.GetCurrentMethod()),
        //        testId);

        //    return result.ReturnValue as ISingleResult<Test>;
        //}

        //[Function(Name = "GetTests")]
        //[ResultType(typeof(Test))]
        //public IMultipleResults RetrieveSettingsByIds()
        //{
        //    var t = (MethodInfo)(MethodBase.GetCurrentMethod());
        //    IExecuteResult result = ExecuteMethodCall(this, t);
        //    return result.ReturnValue as IMultipleResults;
        //}

        #endregion STORED PROCEDURES
    }
}
