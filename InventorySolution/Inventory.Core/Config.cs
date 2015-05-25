using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core
{
    public class Config
    {
        public static string InventaryConnection
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["Inventory"];
                return connectionString != null ? connectionString.ConnectionString : null;
            }
        }
    }
}
