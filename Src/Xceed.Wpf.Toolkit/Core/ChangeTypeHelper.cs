using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xceed.Wpf.Toolkit.Core
{
    public static class ChangeTypeHelper
    {
        public static object ChangeType(object value, Type conversionType, IFormatProvider provider)
        {
            if (conversionType == null)
            {
                throw new ArgumentNullException("conversionType");
            }

            if (conversionType == typeof(Guid))
            {
                return new Guid(value.ToString());
            }
            else if (conversionType == typeof(Guid?))
            {
                if (value == null) return null;
                return new Guid(value.ToString());
            }            
            else if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return null;
                }

                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType, provider);
        }
    }
}
