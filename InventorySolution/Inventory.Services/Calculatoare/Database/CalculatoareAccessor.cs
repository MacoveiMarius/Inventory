using System;
using System.Collections.Generic;
using System.Data.Linq;
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

        private void PreLoadData(CalculatoareDb context, bool loadFullData = true)
        {
            if (loadFullData)
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<Inventar>(i => i);
                options.AssociateWith<Inventar>(i => i.InventareEntity.OrderBy(ppp => ppp.Id));
                context.LoadOptions = options;
            }
            else
            {
                context.ObjectTrackingEnabled = false;
            }
        }

        public List<Calculator> GetCalculatoare()
        {
            using (var context = CalculatoareDb.Create(_strDbConnectionString.ToString()))
            {
                var result = (from t in context.Calculatoare
                             select t).ToList<Calculator>();
                return result;
            }
        }
    }
}
