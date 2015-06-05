using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{
    public class CasariService : ICasari
    {
        private readonly CasariAccessor _casariAccessor;

        public CasariService(StringBuilder strDbConnectionString)
        {
            _casariAccessor = new CasariAccessor(strDbConnectionString);
        }

        public List<Casare> GetCasari(bool loadFullData = false)
        {
            return _casariAccessor.GetCasari();
        }

        public ServiceResult AddCasare(Casare casare)
        {
            return _casariAccessor.AddCasare(casare);
        }
    }
}
