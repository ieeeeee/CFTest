using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OA.Basis.Helper
{
   public  class SessionHelper
    {
        /// <summary>
        /// 根据Session名获取Session对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetSession(string name)
        {
            return HttpContext.Current.Session[name];
        }

        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="name"></param>
        /// <param name="val">比如用户模型</param>
        public static void SetSession(string name,object val)
        {
            HttpContext.Current.Session.Remove(name);
            HttpContext.Current.Session.Add(name, val);
        }

        /// <summary>
        /// 清空所有的Session
        /// </summary>
        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }

        /// <summary>
        /// 删除一个指定的Session
        /// </summary>
        /// <param name="name"></param>
        public static void RemoveSession(string name)
        {
            HttpContext.Current.Session.Remove(name);
        }

        /// <summary>
        /// 删除所有的Session 
        /// </summary>
        public static void RemoveAllSession()
        {
            HttpContext.Current.Session.RemoveAll();
        }

        /// <summary>
        /// 添加Session,调动有效期为iExpires分钟
        /// </summary>
        /// <param name="strSessionName"> Session对象名称</param>
        /// <param name="strValue">Session值</param>
        public static void Add(string strSessionName,string[] strValue,int iExpires)
        {
            HttpContext.Current.Session[strSessionName] = strValue;
            HttpContext.Current.Session.Timeout = iExpires;
        }

        /// <summary>
        /// 读取某个Session对象的值
        /// </summary>
        /// <param name="strSessionName"></param>
        /// <returns></returns>
        public static string GetSessionByName(string strSessionName)
        {
            if(HttpContext.Current.Session[strSessionName]==null)
            {
                return null; //如果值为null,null.ToString()会报错
            }
            else
            {
                return HttpContext.Current.Session[strSessionName].ToString();
            }
        }

        /// <summary>
        /// 读取某个Session对象值数组
        /// </summary>
        /// <param name="strSessionName"></param>
        /// <returns>Session对象值数组</returns>
        public static string[] GetsSessionByName(string strSessionName)
        {
            if(HttpContext.Current.Session[strSessionName]==null)
            {
                return null;
            }
            else
            {
                return (string[])HttpContext.Current.Session[strSessionName];
            }
        }

        /// <summary>
        /// 删除某个Session对象
        /// </summary>
        /// <param name="strSessionName"></param>
        public static void Del(string strSessionName)
        {
            HttpContext.Current.Session[strSessionName] = null;
        }

    }
}
