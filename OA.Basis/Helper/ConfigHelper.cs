using System;
using System.Configuration;


namespace OA.Basis.Helper
{
    /// <summary>
    /// web.config 操作类
    /// </summary>
    public sealed class ConfigHelper
    {
        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key)
        {
            string CacheKey = "AppSettings-" + key;
            object objModel = CacherHelper.GetCache(CacheKey);
            if(objModel==null)
            {
                try
                {
                    objModel = ConfigurationManager.AppSettings[key];
                    if(objModel!=null)
                    {
                        CacherHelper.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(180), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return objModel.ToString();
        }

        /// <summary>
        /// 得到AppSettings中的配置Bool信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetConfigBool(string key)
        {
            bool result = false;
            string configVal = GetConfigString(key);
            if (null!=configVal&&string.Empty!=configVal)
            {
                try
                {
                    result = bool.Parse(configVal);
                }
                catch(FormatException)
                {
                    //Ignore format exceptions.
                }
            }
            return result;
        }
        /// <summary>
        /// 得到AppSettings中的配置Decimal信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetConfigDecimal(string key)
        {
            decimal result = 0;
            string configVal = GetConfigString(key);
            if(null!=configVal&&string.Empty!=configVal)
            {
                try
                {
                    result = decimal.Parse(configVal);
                }
                catch(FormatException)
                {

                }
            }
            return result;
        }
        /// <summary>
        /// 得到AppSettings中的配置int信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetConfigInt(string key)
        {
            decimal result = 0;
            string configVal = GetConfigString(key);
            if (null != configVal && string.Empty != configVal)
            {
                try
                {
                    result = int.Parse(configVal);
                }
                catch (FormatException)
                {

                }
            }
            return result;
        }
    }
}