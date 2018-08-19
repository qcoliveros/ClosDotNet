namespace ClosDotNet.Domain.Base.ORM
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public static class DataReaderExtensions
    {
        public static string GetSafeString(this IDataReader reader, string column)
        {
            if (reader[column] != DBNull.Value)
            {
                return reader[column].ToString();
            } 
            
            return string.Empty;
        }

        public static Boolean GetSafeBoolean(this IDataReader reader, string column)
        {
            return GetSafeBoolean(reader, column, false);
        }

        public static Boolean GetSafeBoolean(this IDataReader reader, string column, bool defaultValue)
        {
            try
            {
                if (reader[column] != DBNull.Value)
                {
                    return bool.Parse(reader[column].ToString());
                }
                
                return defaultValue;                
            }
            catch 
            {
                return defaultValue;
            }
        }

        public static Boolean? GetSafeNullableBoolean(this IDataReader reader, string column)
        {
            Boolean? result = null;
            if (reader[column] != DBNull.Value)
            {
                result = Convert.ToBoolean(reader[column]);
            }

            return result;

        }

        public static int GetSafeInt32(this IDataReader reader, string column)
        {
            if (reader[column] != DBNull.Value)
            {
                return Convert.ToInt32(reader[column]);
            }
            
            return 0;            
        }

        public static Decimal GetSafeDecimal(this IDataReader reader, string column)
        {
            if (reader[column] != DBNull.Value)
            {
                return Convert.ToDecimal(reader[column]);
            }
            
            return 0;
        }

        public static Guid GetSafeGuid(this IDataReader reader, string column)
        {
            if (reader[column] != DBNull.Value)
            {
                return new Guid(reader[column].ToString());
            }
            
            return Guid.Empty;
        }

        public static DateTime GetSafeDateTime(this IDataReader reader, string column)
        {
            if (reader[column] != DBNull.Value)
            {
                return Convert.ToDateTime(reader[column]);
            }
            
            return DateTime.MinValue;
        }

        public static DateTime? GetSafeNullableDateTime(this IDataReader reader, string column)
        {
            DateTime? result = null;
            if (reader[column] != DBNull.Value)
            {
                result = Convert.ToDateTime(reader[column]);
            }
            
            return result;
        }

        public static Char GetSafeChar(this IDataReader reader, string column)
        {
            if (reader[column] != DBNull.Value)
            {
                return Convert.ToChar(reader[column]);
            }
            
            return ' ';
        }

        public static Byte[] GetSafeByteArray(this IDataReader reader, string column)
        {
            Byte[] blob = null;
            if (reader[column] != DBNull.Value)
            {
                try
                {
                    blob = new Byte[(reader.GetBytes(0, 0, null, 0, int.MaxValue))];
                    reader.GetBytes(0, 0, blob, 0, blob.Length);
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Exception: " + e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }

            return blob;
        }

        //This converts an integer column to the given enum (T)
        public static T GetSafeEnum<T>(this IDataReader reader, string column)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException(typeof(T).ToString() + " is not an enum.");
            }

            return (T) Enum.ToObject(typeof(T), reader.GetSafeInt32(column));
        }
    }
}