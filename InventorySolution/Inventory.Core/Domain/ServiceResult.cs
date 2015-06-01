using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Domain
{
    [Serializable]
    [DataContract]
    public class ServiceResult
    {
        public ServiceResult()
        {
        }

        public ServiceResult(int result)
            : this()
        {
            Result = result;
        }

        // vezi enumerearea OperationResult 
        [DataMember]
        public int Result { get; set; }

        public OperationResult OperationResult
        {
            get { return (OperationResult) Result; }
            set { Result = (int) value; }
        }

        [DataMember]
        public string OperationCode { get; set; }

        [DataMember]
        public long EntityId { get; set; }
    }
}
