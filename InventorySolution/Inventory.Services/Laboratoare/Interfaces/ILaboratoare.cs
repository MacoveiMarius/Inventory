using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{

    public interface ILaboratoare
    {
        List<Laborator> GetLaboratoare(bool loadFullData=false);

        Laborator GetLaborator(int id);
        
        ServiceResult AddLaborator(Laborator lab);
        
        ServiceResult UpdateLaborator(Laborator updatelab);
        
        void DeleteLaborator(int id);
    }
}
