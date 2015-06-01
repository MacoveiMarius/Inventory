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

        public List<Gestiune> GetGestiuni()
        {
            return  _gestiuniAccessor.GetGestiuni();
        }

        public Gestiune GetGestiune(int id)
        {
            return _gestiuniAccessor.GetGestiune(id);
        }
    }
}
