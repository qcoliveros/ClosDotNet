namespace ClosDotNet.Domain.Util
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class AccessorUtil
    {
        public static void copyValue(object source, object target)
        {
            copyValue(source, target, null);
        }

        public static void copyValue(object source, object target, IList<string> excludeList)
        {
            Type sourceType = source.GetType();
            PropertyInfo[] sourceProperties = sourceType.GetProperties();

            foreach (PropertyInfo propertyInfo in sourceProperties)
            {
                if (excludeList != null && excludeList.Contains(propertyInfo.Name))
                {
                    continue;
                }

                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(target, propertyInfo.GetValue(source, null), null);
                }
            }
        }
    }
}