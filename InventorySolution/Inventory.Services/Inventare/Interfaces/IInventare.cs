using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{

    public interface IInventare
    {
        List<Inventar> GetInventare(bool loadFullData = false);

        Inventar GetInventar(int id);

        ServiceResult AddInventar(Inventar inventar);

        ServiceResult AddInventarWithNewCalculator(Inventar inventar, Calculator calculator);
    }
}
