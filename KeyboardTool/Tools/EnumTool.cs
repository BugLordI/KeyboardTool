using System;
using System.Collections.Generic;
using System.Text;

namespace KeyboardTool.Tools
{
    public static class EnumTool
    {
        public static T ParseToEnum<T>(this int value) where T : struct, Enum
        {
            Type type = typeof(T);
            if (Enum.TryParse(type, value.ToString(), out object result)
                && Enum.IsDefined(type, value))
            {
                return (T)result;
            }
            else
            {
                return default;
            }
        }
    }
}
