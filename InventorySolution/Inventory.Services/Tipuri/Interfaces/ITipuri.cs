using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{

    public interface ITipuri
    {
        List<Tip> GetTipuri(bool loadFullData = false);

        Tip GetTip(int id);

        ServiceResult AddTip(Tip tip);

        ServiceResult UpdateTip(Tip updateTip);

        void DeleteTip(int id);
    }
}
