using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventorySolution.Classes.Utility;

namespace InventorySolution
{
    public static class MyExtensions
    {
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from TEnum e in Enum.GetValues(typeof (TEnum))
                select new {Id = e, Name = e.ToString()};
            return new SelectList(values, "Id", "Name", enumObj);
        }

        public static SelectList ToStringSelectList<TEnum>(this TEnum enumObj)
            where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var values = from Enum e in Enum.GetValues(typeof(TEnum))
                         select new { Id = e, Name = StringEnum.GetStringValue(e) };
            return new SelectList(values, "Id", "Name", enumObj);
        }

        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}