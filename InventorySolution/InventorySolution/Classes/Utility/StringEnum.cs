using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace InventorySolution.Classes.Utility
{
    /// <summary>
    /// Documentatie: http://stackoverflow.com/questions/424366/c-sharp-string-enums
    /// 
    /// Enumerare cu string-uri
    /// </summary>
    public sealed class StringValue : Attribute
    {
        private string value;

        public StringValue(string value)
        {
            this.value = value;
        }

        public string Value
        {
            get { return value; }
        }
    }

    public static class StringEnum
    {
        /// <summary>
        /// Returneaza valuea atributului de tip string a unei enumerari
        /// </summary>
        /// <param name="value">enumerarea</param>
        /// <returns>valuarea atributului</returns>
        public static string GetStringValue(Enum value)
        {
            Hashtable strValues = new Hashtable();
            string output = null;
            Type type = value.GetType();
            if (strValues.Contains(value))
            {
                output = (strValues[value] as StringValue).Value;
            }
            else
            {
                FieldInfo fi = type.GetField(value.ToString());
                StringValue[] attrs =
                    fi.GetCustomAttributes(typeof (StringValue),
                        false) as StringValue[];
                if (attrs.Length > 0)
                {
                    strValues.Add(value, attrs[0]);
                    output = attrs[0].Value;
                }
            }
            return output;
        }
    }
}