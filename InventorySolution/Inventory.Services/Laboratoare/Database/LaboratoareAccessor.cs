using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Core.Domain;
using Inventory.Core;
using System.Data.SqlClient;
using System.Data.Linq;

namespace Inventory.Services
{

    public class LaboratoareAccessor : ILaboratoare
    {
        private readonly StringBuilder _strDbConnectionString;

        public LaboratoareAccessor(StringBuilder strDbConnectionString)
        {
            _strDbConnectionString = strDbConnectionString;
        }

        public List<Laborator> GetLaboratoare(bool loadFullData = false)
        {
            using (var context = LaboratoareDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context, loadFullData);
                var result = (from t in context.Laboratoare
                              select t).ToList<Laborator>();
                return result;
            }
        }

        private void PreLoadData(LaboratoareDb context, bool loadFullData=true)
        {
            if (loadFullData)
            {
                DataLoadOptions option = new DataLoadOptions();
                option.LoadWith<Laborator>(i=>i.InventareEntity);
                option.AssociateWith<Laborator>(i=>i.InventareEntity.OrderBy(ppp => ppp.Id));
                 context.LoadOptions = option;
            }
            else
            {
                context.ObjectTrackingEnabled = false;
            }
        }

        public Laborator GetLaborator(int id)
        {
            return GetLaborator(id, false);
        }

        public Laborator GetLaborator(int id, bool loadFullData)
        {
            using (var context = LaboratoareDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context, loadFullData);
                var result = (from t in context.Laboratoare
                              select t).SingleOrDefault();
                return result;
            }
        }

        public ServiceResult AddLaborator(Laborator lab)
        {
            ServiceResult result = new ServiceResult((int)OperationResult.Success);
            using (var context = LaboratoareDb.Create(_strDbConnectionString.ToString()))
            {
                try
                {
                    context.Laboratoare.InsertOnSubmit(lab);
                    context.SubmitChanges();

                    result.EntityId = lab.Id;
                }
                catch (SqlException Exc)
                {
                    result.Result = Exc.Number;
                }
            }
            return result;
        }

        public ServiceResult UpdateLaborator(Laborator updatelab)
        {
            ServiceResult result = new ServiceResult((int)OperationResult.Success);
            using (var context = LaboratoareDb.Create(_strDbConnectionString.ToString()))
            {
                try
                {
                    var targetLab = (from n in context.Laboratoare
                                 where n.Id == updatelab.Id
                                 select n).FirstOrDefault();
                    if (targetLab == null)
                    {
                        result.Result = (int)OperationResult.ErrorItemNotFound;
                    }
                    else
                    {
                        BaseEntity.ShallowCopy(updatelab,targetLab);
                        context.SubmitChanges();
                    }
                }
                catch (SqlException Exc)
                {
                    if (Exc.Number == BaseEntity.UNIQUE_INDEX_VIOLATION
                        || Exc.Number == BaseEntity.CANNOT_INSERT_DUPLICATE_KEY_ROW)
                    {
                        result.Result = (int)OperationResult.ErrorDuplicateItem;
                    }
                    else if (Exc.Number == BaseEntity.FOREIGN_KEY_VIOLATION)
                    {
                        result.Result = (int)OperationResult.ErrorForeignKeyViolation;
                    }
                    else
                    {
                        result.Result = (int)OperationResult.Error;
                    }
                }
            }
            return result;
        }

        public void DeleteLaborator(int id)
        {
            using (var context = LaboratoareDb.Create(_strDbConnectionString.ToString()))
            {
                var result = (from t in context.Laboratoare
                              select t).SingleOrDefault();
                if (result == null)
                    return;

                var inventare = from t in context.Inventare
                                where t.LaboratorId == id
                                select t;

                if (inventare != null && inventare.Count() > 0)
                {
                    context.Inventare.DeleteAllOnSubmit(inventare);
                    context.SubmitChanges();
                }
                context.Laboratoare.DeleteOnSubmit(result);
                context.SubmitChanges();
            }
        }
    }
}
