using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
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
        /// SQL Server Code for Unique_Index/Primary_key Violation/Constriant Violation
        /// </summary>
        public const int UNIQUE_INDEX_VIOLATION = 2627;

        public const int FOREIGN_KEY_VIOLATION = 547;
        public const int CANNOT_INSERT_DUPLICATE_KEY_ROW = 2601;

        public abstract int GetId();

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
        public static void ShallowCopy<T>(T source, T destination) where T : new()
        {
            PropertyInfo[] destinationProperties = destination.GetType().GetProperties();
            foreach (PropertyInfo destinationPI in destinationProperties)
            {
                if (destinationPI.CanWrite)
                {
                    PropertyInfo sourcePI = source.GetType().GetProperty(destinationPI.Name);

                    if (!sourcePI.Name.Contains("Entity") && sourcePI.GetIndexParameters().Length <= 0)
                    //don't set the entity relationship members or indexers
                    {
                        destinationPI.SetValue(destination,
                            sourcePI.GetValue(source, null),
                            null);
                    }
                }
            }
        }
    }
}
