using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Core.Domain;
using Inventory.Core;

namespace Inventory.Services
{

    public class TipuriAccessor : ITipuri
    {
        private readonly StringBuilder _strDbConnectionString;

        public TipuriAccessor(StringBuilder strDbConnectionString)
        {
            _strDbConnectionString = strDbConnectionString;
        }

        public List<Tip> GetTipuri()
        {
            using (var context = TipuriDb.Create(_strDbConnectionString.ToString()))
            {
                var result = (from t in context.TipuriTable
                             select t).ToList<Tip>();
                return result;
            }
        }
    }
}
