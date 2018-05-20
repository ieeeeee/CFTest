using System;

namespace OA.Basis.Extentions
{
    //字符串扩展类
    public static class StringExtension
    {
        public static bool IsBlank(this string s)
        {
            return s == null || (s.Trim().Length == 0);
        }
        public static bool IsNotBlank(this string s)
        {
            return !s.IsBlank();
        }
        //比较两个字符串是否相等
        public static bool IsEqual(this string source,string compareValue,StringComparison comparison= StringComparison.OrdinalIgnoreCase)
        {
            if(source.IsNotBlank()&&compareValue.IsNotBlank())
            {
                return source.Equals(compareValue, comparison);
            }
            return source.IsBlank() && compareValue.IsBlank();
        }
        //字符串转数字，未转换成功返回0
        public　static int ToIntWithDefaultValue(this string val,int defaultValue=0)
        {
            if (IsBlank(val))
                return 0;
            int k;
            return int.TryParse(val, out k) ? k : defaultValue;
        }
    }
}