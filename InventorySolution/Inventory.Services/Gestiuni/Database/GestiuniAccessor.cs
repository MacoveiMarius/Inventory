using Inventory.Core;
using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{

    public class GestiuniAccessor : IGestiuni
    {
        private readonly StringBuilder _strDbConnectionString;

        public GestiuniAccessor(StringBuilder strDbConnectionString)
        {
            _strDbConnectionString = strDbConnectionString;
        }

        public List<Gestiune> GetGestiuni()
        {
            using (var context = GestiuniDb.Create(_strDbConnectionString.ToString()))
            {
                var result = (from t in context.GestiuniTable
                             select t).ToList<Gestiune>();
                return result;
            }
        }
    }
}
