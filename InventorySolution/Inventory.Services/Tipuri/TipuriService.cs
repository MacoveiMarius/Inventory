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

        public List<Tip> GetTipuri(bool loadFullData = false)
        {
            return _tipuriAccessor.GetTipuri();
        }

        public Tip GetTip(int id)
        {
            return _tipuriAccessor.GetTip(id);
        }

        public ServiceResult AddTip(Tip tip)
        {
            return _tipuriAccessor.AddTip(tip);
        }

        public ServiceResult UpdateTip(Tip tip)
        {
            return _tipuriAccessor.UpdateTip(tip);
        }

        public void DeleteTip(int id)
        {
            _tipuriAccessor.DeleteTip(id);
        }
    }
}
