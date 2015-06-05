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

        public Sursa GetSursa(int id)
        {
            return _surseAccessor.GetSursa(id);
        }

        public ServiceResult AddSursa(Sursa sursa)
        {
            return _surseAccessor.AddSursa(sursa);
        }

        public ServiceResult UpdateSursa(Sursa updateSursa)
        {
            return _surseAccessor.UpdateSursa(updateSursa);
        }

        public void DeleteSursa(int id)
        {
            _surseAccessor.DeleteSursa(id);
        }


        public Sursa GetSursaByNameInvariant(string name)
        {
            return _surseAccessor.GetSursaByNameInvariant(name);
        }
    }
}
