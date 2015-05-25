using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{
    public class SurseService : ISurse
    {
        private readonly SurseAccessor _surseAccessor;

        public SurseService(StringBuilder strDbConnectionString)
        {
            _surseAccessor = new SurseAccessor(strDbConnectionString);
        }

        public List<Sursa> GetSurse(bool loadFullData = false)
        {
            return _surseAccessor.GetSurse(loadFullData);
        }
    }
}
