using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Core.Domain;
using Inventory.Core;

namespace Inventory.Services
{

    public class CalculatoareAccessor : ICalculatoare
    {
        private readonly StringBuilder _strDbConnectionString;

        public CalculatoareAccessor(StringBuilder strDbConnectionString)
        {
            _strDbConnectionString = strDbConnectionString;
        }

        public List<Calculator> GetCalculatoare()
        {
            using (var context = CalculatoareDb.Create(_strDbConnectionString.ToString()))
            {
                var result = (from t in context.CalculatoareTable
                             select t).ToList<Calculator>();
                return result;
            }
        }
    }
}
