using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Domain
{
    public enum OperationResult : int
    {
        /// <summary> Successful Operation</summary>
        Success = 1,

        /// <summary> Operation Failed. General Error</summary>
        Error = -1,

        /// <summary> Operation Failed. Duplicate Item Constraint </summary>
        ErrorDuplicateItem = -2,

        /// <summary> Operation Failed. Item Not Found </summary>
        ErrorItemNotFound = -3,

        /// <summary> Operation Failed. Foreign Key Constraint Violation </summary>
        ErrorForeignKeyViolation = -4,
    }
}
