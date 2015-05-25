using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Core.Domain;
using Inventory.Core;
using System.Data.Linq;
using System.Runtime.Remoting.Contexts;

namespace Inventory.Services
{

    public class InventareAccessor : IInventare
    {
        private readonly StringBuilder _strDbConnectionString;

        public InventareAccessor(StringBuilder strDbConnectionString)
        {
            _strDbConnectionString = strDbConnectionString;
        }

        public Inventar GetInventarById(int inventarId)
        {
            using (var context = InventareDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context);
                var result = (from i in context.InventareTable
                    //join g in context.GestiuniTable on i.GestiuneId equals g.Id
                    //join l in context.LaboratoareTable on i.LaboratorId equals l.Id
                    //join s in context.SurseTable on i.SursaId equals s.Id
                    //join t in context.TipuriTable on i.TipId equals t.Id
                    where i.Id == inventarId
                    select i).SingleOrDefault();

                return result;
            }
        }

        public List<Inventar> GetInventare(bool loadFullData = false)
        {
            using (var context = InventareDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context, loadFullData);
                var result = (from i in context.InventareTable
                              select i).ToList<Inventar>();
                return result;
            }
        }

        private void PreLoadData(InventareDb context, bool loadFullData = true)
        {
            if (loadFullData)
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<Inventar>(i => i.GestiuneEntity);
                options.LoadWith<Inventar>(i => i.LaboratorEntity);
                options.LoadWith<Inventar>(i => i.SursaEntity);
                options.LoadWith<Inventar>(i => i.TipEntity);
                context.LoadOptions = options;
            }
            else
            {
                context.ObjectTrackingEnabled = false;
            }
        }
    }
}
