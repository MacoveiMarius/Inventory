using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{
    public interface IGestiuni
    {
        List<Gestiune> GetGestiuni(bool loadFullData = false);

        Gestiune GetGestiune(int id);

        ServiceResult UpdateGestiune(Gestiune updateGestiune);

        ServiceResult AddGestiune(Gestiune gestiune);

        void DeleteGestiune(int id);
    }
}
