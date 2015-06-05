using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{
    public class GestiuniService : IGestiuni
    {
        private readonly GestiuniAccessor _gestiuniAccessor;

        public GestiuniService(StringBuilder strDbConnectionString)
        {
            _gestiuniAccessor = new GestiuniAccessor(strDbConnectionString);
        }

        public List<Gestiune> GetGestiuni(bool loadFullData = false)
        {
            return  _gestiuniAccessor.GetGestiuni();
        }

        public Gestiune GetGestiune(int id)
        {
            return _gestiuniAccessor.GetGestiune(id);
        }
        public ServiceResult UpdateGestiune(Gestiune updateGestiune)
        {
            return _gestiuniAccessor.UpdateGestiune(updateGestiune);
        }

        public ServiceResult AddGestiune(Gestiune gestiune)
        {
            return _gestiuniAccessor.AddGestiune(gestiune);
        }

        public void DeleteGestiune(int id)
        {
            _gestiuniAccessor.DeleteGestiune(id);
        }
    }
}
