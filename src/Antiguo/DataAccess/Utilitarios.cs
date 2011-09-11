using System;
using System.Collections.Generic;

namespace DataAccess.Utilitarios
{
    public sealed class Util
    {
        public Nullable<T> DbValueToNullable<T>(object dbValue) where T : struct
        {
            Nullable<T> returnValue = null;

            if ((dbValue != null) && (dbValue != DBNull.Value))
            {
                returnValue = (T)dbValue;
            }

            return returnValue;
        }

        public object NullableToDbValue<T>(object value) where T : struct
        {
            if (value == null)
                return DBNull.Value;
            else
            {
                Nullable<T> nullAble = (T)value;
                return nullAble.Value;
            }
        }

        public object StringToDbValue(string value)
        {
            if (value == string.Empty)
                return DBNull.Value;
            else
                return value;
        }

        public static SortedList<int, string> GetEnumDataSource<T>() where T : struct
        {
            Type myEnumType = typeof(T);
            if (myEnumType.BaseType != typeof(Enum))
            {
                throw new ArgumentException("Type T must inherit from System.Enum.");
            }

            SortedList<int, string> returnCollection = new SortedList<int, string>();
            string[] enumNames = Enum.GetNames(myEnumType);
            for (int i = 0; i < enumNames.Length; i++)
            {
                returnCollection.Add((int)Enum.Parse(myEnumType, enumNames[i]), enumNames[i].Replace('_', ' '));
            }
            return returnCollection;
        }
    }
}
