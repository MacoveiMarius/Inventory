using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{
    public class TipuriService : ITipuri
    {
        private readonly TipuriAccessor _tipuriAccessor;

        public TipuriService(StringBuilder strDbConnectionString)
        {
            _tipuriAccessor = new TipuriAccessor(strDbConnectionString);
        }

        public List<Tip> GetTipuri()
        {
            return _tipuriAccessor.GetTipuri();
        }
    }
}
