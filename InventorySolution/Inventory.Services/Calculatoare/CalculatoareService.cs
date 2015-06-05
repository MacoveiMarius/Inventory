using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{
    public class CalculatoareService : ICalculatoare
    {
        private readonly CalculatoareAccessor _calculatoareAccessor;

        public CalculatoareService(StringBuilder strDbConnectionString)
        {
            _calculatoareAccessor = new CalculatoareAccessor(strDbConnectionString);
        }

        public List<Calculator> GetCalculatoare()
        {
            return  _calculatoareAccessor.GetCalculatoare();
        }

        public Calculator GetCalculator(int id)
        {
            return _calculatoareAccessor.GetCalculator(id);
        }

        public ServiceResult AddCalculator(Calculator calculator)
        {
            return _calculatoareAccessor.AddCalculator(calculator);
        }

        public ServiceResult UpdateCalculator(Calculator calculator)
        {
            return _calculatoareAccessor.UpdateCalculator(calculator);
        }
    }
}
