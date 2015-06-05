using Inventory.Core;
using Inventory.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.SqlClient;

namespace Inventory.Services
{

    public class CasariAccessor : ICasari
    {
        private readonly StringBuilder _strDbConnectionString;

        public CasariAccessor(StringBuilder strDbConnectionString)
        {
            _strDbConnectionString = strDbConnectionString;
        }

        public List<Casare> GetCasari(bool loadFullData = false)
        {
            using (var context = CasareDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context, loadFullData);
                var result = (from t in context.Casare
                              select t).ToList<Casare>();
                return result;
            }
        }

        private void PreLoadData(CasareDb context, bool loadFullData = true)
        {
            if (loadFullData)
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<Gestiune>(g => g.InventareEntity);
                options.AssociateWith<Gestiune>(g => g.InventareEntity.OrderBy(p => p.GestiuneId));
                context.LoadOptions = options;
            }
            else
                context.ObjectTrackingEnabled = false;
        }


        public ServiceResult AddCasare(Casare casare)
        {
            ServiceResult result = new ServiceResult((int)OperationResult.Success);
            using (var context = CasareDb.Create(_strDbConnectionString.ToString()))
            {
                try
                {
                    context.Casare.InsertOnSubmit(casare);
                    context.SubmitChanges();

                    result.EntityId = casare.Id;
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

    }
}
