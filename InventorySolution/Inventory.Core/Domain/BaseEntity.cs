using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core
{
    [DataContract]
    [Serializable]
    public abstract class BaseEntity
    {
        /// <summary>
        /// The id is needed in various places, however, keeping the Id field in the BaseEntity 
        /// causes some problems with Linq2SQL so we use this instead.
        /// </summary>
        /// <returns></returns>
        public abstract short GetId();

        private static bool IsTransient(BaseEntity obj)
        {
            return obj != null && Equals(obj.GetId(), default(long));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(BaseEntity other)
        {
            if (other == null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(GetId(), other.GetId()))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                       otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public static bool operator ==(BaseEntity x, BaseEntity y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(BaseEntity x, BaseEntity y)
        {
            return !(x == y);
        }

        public virtual EntitySet<T> ValidateEntitySet<T>(EntitySet<T> entitySet) where T : class
        {
            if (entitySet != null)
            {
                try
                {
                    var count = entitySet.Count;
                }
                catch (ObjectDisposedException)
                {
                    return null;
                }
            }
            return entitySet;
        }
    }
}
