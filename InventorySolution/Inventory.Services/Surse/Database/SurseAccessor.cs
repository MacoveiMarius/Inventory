using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Core.Domain;
using Inventory.Core;

namespace Inventory.Services
{

    public class SurseAccessor : ISurse
    {
        private readonly StringBuilder _strDbConnectionString;

        public SurseAccessor(StringBuilder strDbConnectionString)
        {
            _strDbConnectionString = strDbConnectionString;
        }

        public List<Sursa> GetSurse(bool loadFullData = false)
        {
            using (var context = SurseDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context, loadFullData);
                var result = (from t in context.SurseTable
                    where t.Id > 0      //0  are NULL
                    select t).ToList<Sursa>();
                return result;
            }
        }

        private void PreLoadData(SurseDb context, bool loadFullData = true)
        {
            if (loadFullData)
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<Sursa>(i => i.InventareEntity);
                options.AssociateWith<Sursa>(i => i.InventareEntity.OrderBy(ppp => ppp.Id));
                context.LoadOptions = options;
            }
            else
            {
                context.ObjectTrackingEnabled = false;
            }
        }
    }
}
