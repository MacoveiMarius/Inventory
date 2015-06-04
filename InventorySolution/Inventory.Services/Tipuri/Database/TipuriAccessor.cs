using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Core.Domain;
using Inventory.Core;
using System.Data.Linq;
using System.Data.SqlClient;

namespace Inventory.Services
{

    public class TipuriAccessor : ITipuri
    {
        private readonly StringBuilder _strDbConnectionString;

        public TipuriAccessor(StringBuilder strDbConnectionString)
        {
            _strDbConnectionString = strDbConnectionString;
        }

        public List<Tip> GetTipuri(bool loadFullData = false)
        {
            using (var context = TipuriDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context, loadFullData);
                var result = (from t in context.Tipuri
                             select t).ToList<Tip>();
                return result;
            }
        }

        public Tip GetTip(int id)
        {
            return GetTip(id, false);
        }

        private Tip GetTip(int id, bool loadFullData = false)
        {
            using (var context = TipuriDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context, loadFullData);
                var result = (from t in context.Tipuri
                              where t.Id == id
                              select t).SingleOrDefault();
                return result;
            }
        }

        public void DeleteTip(int id)
        {
            ServiceResult result = new ServiceResult();
            using (var context = TipuriDb.Create(_strDbConnectionString.ToString()))
            {
                var sursa = (from t in context.Tipuri
                             where t.Id == id
                             select t).SingleOrDefault();
                if (sursa == null)
                {
                    return;
                }

                var inventare = from t in context.Inventare
                                where t.TipId == id
                                select t;

                if (inventare != null && inventare.Count() > 0)
                {
                    context.Inventare.DeleteAllOnSubmit(inventare);
                    context.SubmitChanges();
                }
                context.Tipuri.DeleteOnSubmit(sursa);
                context.SubmitChanges();
            }
        }

        public ServiceResult AddTip(Tip tip)
        {
            ServiceResult result = new ServiceResult((int)OperationResult.Success);
            using (var context = TipuriDb.Create(_strDbConnectionString.ToString()))
            {
                try
                {
                    context.Tipuri.InsertOnSubmit(tip);
                    context.SubmitChanges();

                    result.EntityId = tip.Id;
                }
                catch (SqlException sqlExc)
                {
                    result.Result = sqlExc.Number;
                }
            }
            return result;
        }

        public ServiceResult UpdateTip(Tip updateTip)
        {
            ServiceResult result = new ServiceResult((int)OperationResult.Success);
            using (var context = TipuriDb.Create(_strDbConnectionString.ToString()))
            {
                try
                {
                    var targetTip = (from n in context.Tipuri
                                     where n.Id == updateTip.Id
                                     select n).FirstOrDefault();
                    if (targetTip == null)
                    {
                        result.Result = (int)OperationResult.ErrorItemNotFound;
                    }
                    else
                    {
                        BaseEntity.ShallowCopy(updateTip, targetTip);
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

        private void PreLoadData(TipuriDb context, bool loadFullData = true)
        {
            if (loadFullData)
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<Tip>(i => i.InventareEntity);
                options.AssociateWith<Tip>(i => i.InventareEntity.OrderBy(ppp => ppp.Id));
                context.LoadOptions = options;
            }
            else
            {
                context.ObjectTrackingEnabled = false;
            }
        }
    }
}
