using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services
{
    public class InventareService : IInventare
    {
        private readonly InventareAccessor _inventareAccessor;

        public InventareService(StringBuilder strDbConnectionString)
        {
            _inventareAccessor = new InventareAccessor(strDbConnectionString);
        }

        public Inventar GetInventar(int inventarId)
        {
            return _inventareAccessor.GetInventar(inventarId);
        }

        public List<Inventar> GetInventare(bool loadFullData = false)
        {
            return _inventareAccessor.GetInventare(loadFullData);
        }

        public int DeleteInventareBySursa(int sursaId)
        {
            return _inventareAccessor.DeleteInventareBySursa(sursaId);
        }


        public ServiceResult AddInventar(Inventar inventar)
        {
            return _inventareAccessor.AddInventar(inventar);
        }

        public ServiceResult AddInventarWithNewCalculator(Inventar inventar, Calculator calculator)
        {
            var serviceResult = AddInventar(inventar);

            //s-a reusit adaugarea unui nou inventar
            if (serviceResult.OperationResult == OperationResult.Success)
            {
                //adauga calculatorul pentru inventar
                calculator.Id = serviceResult.EntityId;

                var src = SVC.Calculatoare.AddCalculator(calculator);
                if (src.OperationResult == OperationResult.Success)
                {
                    return serviceResult;
                }
            }

            return serviceResult;
        }


        public ServiceResult UpdateInventarWithNewCalculator(Inventar inventar, Calculator calculator)
        {
            var serviceResult = UpdateInventar(inventar);

            //s-a reusit adaugarea unui nou inventar
            if (serviceResult.OperationResult == OperationResult.Success)
            {
                //actualizeaza calculatorul pentru inventar
                calculator.Id = serviceResult.EntityId;

                var src = SVC.Calculatoare.UpdateCalculator(calculator);
                if (src.OperationResult == OperationResult.Success)
                {
                    return serviceResult;
                }
            }

            return serviceResult;
        }

        public ServiceResult UpdateInventar(Inventar inventar)
        {
            return _inventareAccessor.UpdateInventar(inventar);
        }


        public void DeleteInventar(int id)
        {
            _inventareAccessor.DeleteInventar(id);
        }

        public ServiceResult Caseaza(int id, Casare casare)
        {
            var serviceResult = SVC.Casari.AddCasare(casare);
            if (serviceResult.OperationResult == OperationResult.Success)
            {
                DeleteInventar(id);
            }

            return serviceResult;
        }
    }
}
