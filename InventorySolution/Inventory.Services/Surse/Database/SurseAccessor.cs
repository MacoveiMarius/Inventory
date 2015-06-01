using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Core.Domain;
using Inventory.Core;

namespace Inventory.Services
{

    public class SurseAccessor : ISurse
    {
        private readonly StringBuilder _strDbConnectionString;

        public SurseAccessor(StringBuilder strDbConnectionString)
        {
            _strDbConnectionString = strDbConnectionString;
        }

        public List<Sursa> GetSurse(bool loadFullData = false)
        {
            using (var context = SurseDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context, loadFullData);
                var result = (from t in context.Surse
                    where t.Id > 0      // sursa cu id-ul '0' este NULL
                    select t).ToList<Sursa>();
                return result;
            }
        }

        public Sursa GetSursa(int id)
        {
            return GetSursa(id, false);
        }

        private Sursa GetSursa(int id, bool loadFullData = false)
        {
            using (var context = SurseDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context, loadFullData);
                var result = (from t in context.Surse
                              where t.Id == id
                              select t).SingleOrDefault();
                return result;
            }
        }

        public void DeleteSursa(int id)
        {
            ServiceResult result = new ServiceResult();
            using (var context = SurseDb.Create(_strDbConnectionString.ToString()))
            {
                var sursa = (from t in context.Surse
                              where t.Id == id
                              select t).SingleOrDefault();
                if (sursa == null)
                {
                    return;
                }

                var inventare = from t in context.Inventare
                    where t.SursaId == id
                    select t;

                if (inventare != null && inventare.Count() > 0)
                {
                    context.Inventare.DeleteAllOnSubmit(inventare);
                    context.SubmitChanges();
                }
                context.Surse.DeleteOnSubmit(sursa);
                context.SubmitChanges();
            }
        }

        public ServiceResult AddSursa(Sursa sursa)
        {
            ServiceResult result = new ServiceResult((int)OperationResult.Success);
            using (var context = SurseDb.Create(_strDbConnectionString.ToString()))
            {
                try
                {
                    context.Surse.InsertOnSubmit(sursa);
                    context.SubmitChanges();

                    result.EntityId =  sursa.Id;
                }
                catch (SqlException sqlExc)
                {
                    result.Result = sqlExc.Number;
                }
            }
            return result;
        }

        public ServiceResult UpdateSursa(Sursa updateSursa)
        {
            ServiceResult result = new ServiceResult((int)OperationResult.Success);
            using (var context = SurseDb.Create(_strDbConnectionString.ToString()))
            {
                try
                {
                    var targetNote = (from n in context.Surse
                                      where n.Id == updateSursa.Id
                                      select n).FirstOrDefault();
                    if (targetNote == null)
                    {
                        result.Result = (int)OperationResult.ErrorItemNotFound;
                    }
                    else
                    {
                        BaseEntity.ShallowCopy(updateSursa, targetNote);
                        context.SubmitChanges();
                    }
                }
                catch (SqlException sqlExc)
                {
                    if (sqlExc.Number == BaseEntity.UNIQUE_INDEX_VIOLATION ||
                        sqlExc.Number == BaseEntity.CANNOT_INSERT_DUPLICATE_KEY_ROW)
                    {
                        result.Result = (int)OperationResult.ErrorDuplicateItem;
                    }
                    else if (sqlExc.Number == BaseEntity.FOREIGN_KEY_VIOLATION)
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

        private void PreLoadData(SurseDb context, bool loadFullData = true)
        {
            if (loadFullData)
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<Sursa>(i => i.InventareEntity);
                options.AssociateWith<Sursa>(i => i.InventareEntity.OrderBy(ppp => ppp.Id));
                context.LoadOptions = options;
            }
            else
            {
                context.ObjectTrackingEnabled = false;
            }
        }
    }
}
