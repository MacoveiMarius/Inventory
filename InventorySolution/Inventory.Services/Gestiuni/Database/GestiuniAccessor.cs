using Inventory.Core;
using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace Inventory.Services
{

    public class GestiuniAccessor : IGestiuni
    {
        private readonly StringBuilder _strDbConnectionString;

        public GestiuniAccessor(StringBuilder strDbConnectionString)
        {
            _strDbConnectionString = strDbConnectionString;
        }

        public List<Gestiune> GetGestiuni(bool loadFullData = false)
        {
            using (var context = GestiuniDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context, loadFullData);
                var result = (from t in context.Gestiuni
                              select t).ToList<Gestiune>();
                return result;
            }
        }

        private void PreLoadData(GestiuniDb context, bool loadFullData = true)
        {
            if (loadFullData)
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<Gestiune>(g => g.InventareEntity);
                options.AssociateWith<Gestiune>(g => g.InventareEntity.OrderBy(p => p.GestiuneId));
                context.LoadOptions = options;
            }
            else
                context.ObjectTrackingEnabled = false;
        }

        public Gestiune GetGestiune(int id)
        {
            return GetGestiune(id, false);
        }

        public Gestiune GetGestiune(int id, bool loadFullData = false)
        {
            using (var context = GestiuniDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context,loadFullData);
                var result = (from t in context.Gestiuni
                              where t.Id == id
                              select t).SingleOrDefault();
                return result;
            }
        }
    }
}
