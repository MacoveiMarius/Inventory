using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{
    public class InventareService : IInventare
    {
        private readonly InventareAccessor _inventareAccessor;

        public InventareService(StringBuilder strDbConnectionString)
        {
            _inventareAccessor = new InventareAccessor(strDbConnectionString);
        }

        public Inventar GetInventarById(int inventarId)
        {
            return _inventareAccessor.GetInventarById(inventarId);
        }

        public List<Inventar> GetInventare(bool loadFullData = false)
        {
            return _inventareAccessor.GetInventare(loadFullData);
        }
    }
}
