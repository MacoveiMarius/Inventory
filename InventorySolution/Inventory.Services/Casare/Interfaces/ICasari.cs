using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{
    public interface ICasari
    {
        List<Casare> GetCasari(bool loadFullData = false);

        //Gestiune GetCasare(int id);

        ServiceResult AddCasare(Casare casare);
    }
}
