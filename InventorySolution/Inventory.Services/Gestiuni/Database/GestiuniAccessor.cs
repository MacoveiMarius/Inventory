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

    public class GestiuniAccessor : IGestiuni
    {
        private readonly StringBuilder _strDbConnectionString;

        public GestiuniAccessor(StringBuilder strDbConnectionString)
        {
            _strDbConnectionString = strDbConnectionString;
        }

        public List<Gestiune> GetGestiuni(bool loadFullData = false)
        {
            using (var context = GestiuniDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context, loadFullData);
                var result = (from t in context.Gestiuni
                              select t).ToList<Gestiune>();
                return result;
            }
        }

        private void PreLoadData(GestiuniDb context, bool loadFullData = true)
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

        public Gestiune GetGestiune(int id)
        {
            return GetGestiune(id, false);
        }

        public Gestiune GetGestiune(int id, bool loadFullData = false)
        {
            using (var context = GestiuniDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context,loadFullData);
                var result = (from t in context.Gestiuni
                              where t.Id == id
                              select t).SingleOrDefault();
                return result;
            }
        }

        public ServiceResult UpdateGestiune(Gestiune updateGestiune)
        {
            ServiceResult result = new ServiceResult((int)OperationResult.Success);
            using (var context = GestiuniDb.Create(_strDbConnectionString.ToString()))
            {
                try
                {
                    var targetGestiune = (from g in context.Gestiuni 
                                          where g.Id == updateGestiune.Id 
                                          select g).FirstOrDefault();

                    if (targetGestiune == null)
                    {
                        result.Result = (int)OperationResult.ErrorItemNotFound;
                    }
                    else
                    {
                        BaseEntity.ShallowCopy(updateGestiune,targetGestiune);
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

        public ServiceResult AddGestiune(Gestiune gestiune)
        {
            ServiceResult result = new ServiceResult((int)OperationResult.Success);
            using (var context = GestiuniDb.Create(_strDbConnectionString.ToString()))
            {
                try
                {
                    context.Gestiuni.InsertOnSubmit(gestiune);
                    context.SubmitChanges();

                    result.EntityId = gestiune.Id;
                }
                catch(SqlException sqlExc)
                {
                    result.Result = sqlExc.Number;
                }
            }
            return result;
        }

        public void DeleteGestiune(int id)
        {
            ServiceResult result = new ServiceResult();
            using (var context = GestiuniDb.Create(_strDbConnectionString.ToString()))
            {
                var sursa = (from t in context.Gestiuni
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
                context.Gestiuni.DeleteOnSubmit(sursa);
                context.SubmitChanges();
            }
        }
    }
}
