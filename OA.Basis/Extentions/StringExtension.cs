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

        //如果为空，返回默认值
        public static string WithDefaultValueIfEmpty(this string value,string defaultValue)
        {
            return value.IsBlank() ? defaultValue : value;
        }

        public static string GetPlus(string a,string b)
        {
            int aLen = a.Length;
            int bLen = b.Length;
            if(aLen-bLen>=0)
            {
                a=a.PadLeft(aLen+1, '0');
                b=b.PadLeft(aLen+ 1, '0');
            }
            else
            {
                a=a.PadLeft(bLen+1, '0');
                b=b.PadLeft(bLen+1, '0');
            }
            char[] Na = a.ToCharArray();
            char[] Nb = b.ToCharArray();
            char[] Nc = Carry(Na,Nb);

            char[] result =new char[Na.Length]; 

            result = Plus(Na, Nb);
            result = Plus(result, Nc);
            if(result[0]=='0')
            {
                return new String(result).Substring(1, result.Length - 1);
            }
            return new String(result);

        }

        public static char[] Plus(char[] a, char[] b)
        {
            char[] result = new char[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                if ((a[i] == b[i]))
                {
                    result[i] = '0';
                }
                else
                {
                    result[i] = '1';
                }
            }
            return result;
        }
        public static char[] Carry(char[] a, char[] b)
        {
            char[] result = new char[a.Length];
            for (int i = 1; i < a.Length; i++)
            {
                if ((a[i] == '1' && b[i] == '1'))
                {
                    result[i-1] = '1';
                }
                else
                {
                    result[i-1] = '0';
                }
            }
            result[a.Length - 1] = '0';
            return result;
        }
        public static string[] StrToArray(string str)
        {
            string[] Array = new string[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                Array[i] = str.Substring(i, 1);
            }
            return Array;
        }

    }
}