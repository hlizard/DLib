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
            return ParseEnum<T>(value);
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
        
        public static T ParseEnum<T>(string value, EnumParseBy parseBy)
        {
            if(parseBy == EnumParseBy.Code)
            {
                int result = int.Parse(value);
                //return (T)Convert.ChangeType(result, typeof(T));
                return (T)Enum.ToObject(typeof(T), result);
            }
            return ParseEnum<T>(value);
        }
        
        public static T ToEnum<T>(this string value, EnumParseBy parseBy)
        {
            return ParseEnum<T>(value, parseBy);
        }
        
        public static T ToEnum<T>(this string value, T defaultValue, EnumParseBy parseBy) where T : struct
        {
            if(parseBy == EnumParseBy.Code)
            {
                if (value == null || value.Trim() == "")
                {
                    return defaultValue;
                }

                int result;
                //return int.TryParse(value, out result) ? (T)Enum.ToObject(typeof(T), result) : defaultValue;
                if(int.TryParse(value, out result))
                {
                    var a = (T)Enum.ToObject(typeof(T), result);
                    return a;
                }
                else
                {
                    return defaultValue;
                }
            }
            return ToEnum<T>(value, defaultValue);
        }
    }

    public class EnumParseBy
    {
        public static readonly EnumParseBy Name = new EnumParseBy(0);
        public static readonly EnumParseBy Code = new EnumParseBy(1);

        private int _infoType;

        private EnumParseBy(int infoType)
        {
            _infoType = infoType;
        }
    }
}
