using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Core.Domain;
using Inventory.Core;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;

namespace Inventory.Services
{

    public class InventareAccessor : IInventare
    {
        private readonly StringBuilder _strDbConnectionString;

        public InventareAccessor(StringBuilder strDbConnectionString)
        {
            _strDbConnectionString = strDbConnectionString;
        }

        public Inventar GetInventar(int inventarId)
        {
            using (var context = InventareDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context);
                var result = (from i in context.Inventare
                    where i.Id == inventarId
                    select i).SingleOrDefault();

                return result;
            }
        }

        public List<Inventar> GetInventare(bool loadFullData = false)
        {
            using (var context = InventareDb.Create(_strDbConnectionString.ToString()))
            {
                PreLoadData(context, loadFullData);
                var result = (from i in context.Inventare
                              select i).ToList<Inventar>();
                return result;
            }
        }

        public int DeleteInventareBySursa(int sursaId)
        {
            using (var context = InventareDb.Create(_strDbConnectionString.ToString()))
            {
                try
                {
                    int deletedInventareCount = (int) OperationResult.Error;
                    var inventare = from inventar in context.Inventare
                        where inventar.SursaId == sursaId
                        select inventar;

                    if (inventare == null)
                    {
                        return (int) OperationResult.ErrorItemNotFound;
                    }
                    context.Inventare.DeleteAllOnSubmit(inventare);
                    context.SubmitChanges();
                    deletedInventareCount = inventare.Count();

                    return deletedInventareCount;
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Number == InventareDb.UNIQUE_INDEX_VIOLATION)
                    {
                        return (int) OperationResult.ErrorDuplicateItem;
                    }
                    return (int) OperationResult.Error;
                }
            }
        }

        private void PreLoadData(InventareDb context, bool loadFullData = true)
        {
            if (loadFullData)
            {
                DataLoadOptions options = new DataLoadOptions();
                options.LoadWith<Inventar>(i => i.GestiuneEntity);
                options.LoadWith<Inventar>(i => i.LaboratorEntity);
                options.LoadWith<Inventar>(i => i.SursaEntity);
                options.LoadWith<Inventar>(i => i.TipEntity);
                context.LoadOptions = options;
            }
            else
            {
                context.ObjectTrackingEnabled = false;
            }
        }
    }
}
