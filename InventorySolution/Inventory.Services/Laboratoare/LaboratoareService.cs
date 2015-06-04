using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{
    public class LaboratoareService : ILaboratoare
    {
        private readonly LaboratoareAccessor _laboratoareAccessor;

        public LaboratoareService (StringBuilder strDbConnectionString)
        {
            _laboratoareAccessor = new LaboratoareAccessor(strDbConnectionString);
        }

        public List<Laborator> GetLaboratoare(bool loadFullData = false)
        {
            return _laboratoareAccessor.GetLaboratoare(loadFullData);
        }

        public Laborator GetLaborator(int id)
        {
            return _laboratoareAccessor.GetLaborator(id);
        }

        public ServiceResult AddLaborator(Laborator lab)
        {
            return _laboratoareAccessor.AddLaborator(lab);
        }

        public ServiceResult UpdateLaborator(Laborator updatelab)
        {
            return _laboratoareAccessor.UpdateLaborator(updatelab);
        }

        public void DeleteLaborator(int id)
        {
            _laboratoareAccessor.DeleteLaborator(id);
        }
    }
}
