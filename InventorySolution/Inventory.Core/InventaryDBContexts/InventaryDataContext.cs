using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core
{
    public class InventaryDataContext : DataContext, IDisposable
    {
        protected SqlConnection _connection = null;

        public const int UNIQUE_INDEX_VIOLATION = 2627;

        public const int FOREIGN_KEY_VIOLATION = 547;
        public const int CANNOT_INSERT_DUPLICATE_KEY_ROW = 2601;

        public InventaryDataContext(SqlConnection connection)
            : base(connection)
        {
            _connection = connection;
        }
    }
}
