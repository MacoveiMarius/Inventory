using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Core.Domain;
using Inventory.Core;

namespace Inventory.Services
{

    public class LaboratoareAccessor : ILaboratoare
    {
        private readonly StringBuilder _strDbConnectionString;

        public LaboratoareAccessor(StringBuilder strDbConnectionString)
        {
            _strDbConnectionString = strDbConnectionString;
        }

        public List<Laborator> GetLaboratoare()
        {
            using (var context = LaboratoareDb.Create(_strDbConnectionString.ToString()))
            {
                var result = (from t in context.LaboratoareTable
                              select t).ToList<Laborator>();
                return result;
            }
        }
    }
}
