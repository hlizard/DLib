using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLib
{
    public static class EnumUtil
    {
        //StatusEnum MyStatus = EnumUtil.ParseEnum<StatusEnum>("Active");
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        //StatusEnum MyStatus = "Active".ToEnum<StatusEnum>();
        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        //StatusEnum MyStatus = "Active".ToEnum(StatusEnum.None);
        public static T ToEnum<T>(this string value, T defaultValue) where T : struct
        {
            if (value == null || value.Trim() == "")
            {
                return defaultValue;
            }

            T result;
            return Enum.TryParse<T>(value, true, out result) ? result : defaultValue;
        }


        public static string GetCode<T>(this T value) where T : struct
        {
            if (typeof(T).BaseType.Name != "Enum")
            {
                throw new NotImplementedException(typeof(T).Name);
            }
            string code = (Convert.ToInt32(value)).ToString();
            return code;
        }
    }
}
