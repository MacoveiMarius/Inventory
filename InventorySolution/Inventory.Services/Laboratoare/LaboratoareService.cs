using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{
    public class LaboratoareService : ILaboratoare
    {
        private readonly LaboratoareAccessor _laboratoareAccessor;

        public LaboratoareService (StringBuilder strDbConnectionString)
        {
            _laboratoareAccessor = new LaboratoareAccessor(strDbConnectionString);
        }

        public List<Laborator> GetLaboratoare()
        {
            return _laboratoareAccessor.GetLaboratoare();
        }
    }
}
