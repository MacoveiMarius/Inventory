using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{

    public interface ISurse
    {
        List<Sursa> GetSurse(bool loadFullData = false);

        Sursa GetSursa(int id);

        ServiceResult AddSursa(Sursa sursa);
        
        ServiceResult UpdateSursa(Sursa updateSursa);

        void DeleteSursa(int id);
    }
}
