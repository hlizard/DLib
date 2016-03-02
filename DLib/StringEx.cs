using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLib
{
    public static class StringEx
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return str == null || str.Trim() == string.Empty;
        }
    }
}
